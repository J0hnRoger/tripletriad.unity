using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TripleTriad.Assets.Scripts.Core.SOs;
using TripleTriad.Infrastructure;
using TripleTriad.Infrastructure.Models;
using UnityEngine;
using UnityEngine.Networking;

namespace TripleTriad.Persistence
{
    // Persistent manager - serialize all datas we want to save   
    // then delegate the saving to the infrastructure layer 
    public class ApiPersistenceManager : Singleton<ApiPersistenceManager>, IPersistenceManager 
    {
        [SerializeField] string _apiUrl;
        [SerializeField] string _gameName;
        [SerializeField] RuntimeSOSet _savedAndLoadedDatas;

        [SerializeField] private CardsList _cardsLibrary;

        private const string SESSION_ID_KEY = "tripleTriad_sessionId"; 
        public Action OnGameLoaded { get; set; }
        
        private GamingDataService _gamingDataService;

        /// <summary>
        /// On utilise un void pour indiquer à Unity que c'est une méthode async
        /// le void contrairement à la Task nous sert de point d'entrée 
        /// </summary>
        public async void Awake()
        {
            // Recuperation de l'id unique depuis les playerPrefs 
            string sessionId = PlayerPrefs.GetString(SESSION_ID_KEY);
            sessionId = "";
            if (string.IsNullOrEmpty(sessionId))
            {
                sessionId  = Guid.NewGuid().ToString();
                PlayerPrefs.SetString(SESSION_ID_KEY, sessionId);
            }
            _gamingDataService = new GamingDataService(_apiUrl, _gameName, sessionId);
            await Load();
        }

        public async Task Save()
        {
            Dictionary<int, string> jsons = new Dictionary<int, string>();
            foreach (ScriptableObject so in _savedAndLoadedDatas.Items)
            {
                int id = so.GetInstanceID();
                string jsonSo = JsonUtility.ToJson(so);
                jsons.Add(id, jsonSo);
            }

            try
            {
                await _gamingDataService.SaveData(jsons);
            }
            catch (Exception ex)
            {
                Debug.Log(ex.Message);
            }
        }

        public async Task Load()
        {
            try
            {
                List<CardDto> allCards = await _gamingDataService.LoadAllCards();
                InitCardsList(allCards);
                Dictionary<int, string> loadedDatas = await _gamingDataService.LoadData();
                // TODO - if return OK but null - reset the session 
                // Fill SOs
                if (loadedDatas == null)
                    throw new Exception("Joueur existant, mais partie supprimée coté serveur");
                
                foreach (ScriptableObject so in _savedAndLoadedDatas.Items)
                {
                    int id = so.GetInstanceID();
                    if (loadedDatas.TryGetValue(id, out string soData))
                        JsonUtility.FromJsonOverwrite(soData, so);
                }
            }
            catch (Exception ex)
            {
                Debug.Log($"Erreur during loading: {ex.Message}");
            }
            // On attend la fin du chargement de la partie
            OnGameLoaded?.Invoke();
        }

        IEnumerator DownloadImageAsSprite(CardData cardData, string imageUrl)
        {
            UnityWebRequest request = UnityWebRequestTexture.GetTexture(imageUrl);
            yield return request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(request.error);
            }
            Sprite cardSprite = Sprite.Create(((DownloadHandlerTexture)request.downloadHandler).texture, Rect.zero, Vector2.zero);
            cardData.ArtWork = cardSprite;
        }
        
        private void InitCardsList(List<CardDto> allCards)
        {
            _cardsLibrary.Items = new List<CardData>();
            foreach (CardDto card  in allCards)
            {
                var cardData = ScriptableObject.CreateInstance<CardData>();
                
                cardData.Bottom = card.Bottom;
                cardData.Top = card.Top;
                cardData.Left = card.Left;
                cardData.Right = card.Right;
                cardData.Name = card.Name;
                
                // StartCoroutine(DownloadImageAsSprite(cardData, card.ImageUrl));
                
               _cardsLibrary.Items.Add(cardData); 
            }
        }
    }
}