using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TripleTriad.Assets.Scripts.Core.SOs;
using TripleTriad.Infrastructure;
using UnityEngine;

namespace TripleTriad.Persistence
{
    // Persistent manager - serialize all datas we want to save   
    // then delegate the saving to the infrastructure layer 
    public class JsonPersistenceManager : Singleton<JsonPersistenceManager>, IPersistenceManager 
    {
        [SerializeField] string _saveFileName;
        [SerializeField] RuntimeSOSet _savedAndLoadedDatas;

        public Action OnGameLoaded { get; set; }
        
        private JsonPersistenceRepository _jsonPersistentRepository;

        public async Task Awake()
        {
            string filePath = Path.Combine(Application.persistentDataPath, _saveFileName);
            _jsonPersistentRepository = new JsonPersistenceRepository(filePath);
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
            await _jsonPersistentRepository.SaveData(jsons);
        }

        public async Task Load()
        {
            Dictionary<int, string> loadedDatas = await _jsonPersistentRepository.LoadData();
            // Fill SOs
            foreach (ScriptableObject so in _savedAndLoadedDatas.Items)
            {
                int id = so.GetInstanceID();
                if (loadedDatas.TryGetValue(id, out string soData))
                    JsonUtility.FromJsonOverwrite(soData, so);
            }
            // On attend la fin du chargement de la partie
            OnGameLoaded?.Invoke();
        }
    }
}