using System;
using System.Linq;
using System.Threading.Tasks;
using TripleTriad.Core.EventArchitecture.Events;
using TripleTriad.Engine;
using TripleTriad.Events;
using TripleTriad.Persistence;
using TripleTriad.WorldScene;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Control the game Flow  
/// </summary>
public class GameManager : Manager<GameManager>
{
    [Header("Player Data")] 
    [SerializeField] private DeckData _runtimePlayerDeck;
    [SerializeField] private PlayerData _runtimePlayer;
    
    [Header("Progression Data")]
    [SerializeField] private LevelsRuntimeSet  _starterWorldMap;
    [SerializeField] private LevelsRuntimeSet _runtimeWorldMap;
    [SerializeField] private MapLevelData _runtimeLevel;
    [SerializeField] private CardData _runtimeWonCard;
    
    [Header("Raise events")]
    [SerializeField] private SceneEvent _onLoadSceneEvent;
    [SerializeField] private MapLevelDataEvent _onLevelLoadedEvent;
    
    // Dependencies 
    private IPersistenceManager _persistenceManager;

    private MapView _currentQuestView;
    private PlayerData _currentOpponent;

    [SerializeField] private DeckData _starterDeck;

    private Transform _loadingScreen;
    private Transform _gameOverScreen;

    public Player CurrentPlayer { get; internal set; }

    public void Awake()
    {
        _persistenceManager = GetComponent<IPersistenceManager>();
        _gameOverScreen = GetComponentsInChildren<Transform>()
            .FirstOrDefault(go => go.name == "GameOverScreen");
        
        // Waiting for Loading data from external dependencies only if exists 
        if (_persistenceManager == null)
            StartGame();
        else
            _persistenceManager.OnGameLoaded += StartGame;
    }

    private async void SaveGameState()
    {
        await _persistenceManager.Save();
        int questId = (_currentQuestView != null) 
            ? _currentQuestView.GetInstanceID() : -1;
    }

    private async Task InitGameState()
    {
        if (_runtimePlayerDeck.AllCards.Count == 0)
           _runtimePlayerDeck.InitFrom(_starterDeck);
        if (_runtimeWorldMap.Items.Count == 0)
            _runtimeWorldMap.InitFrom(_starterWorldMap);
    }

    private void StartGame()
    {
        // first access  
        InitGameState().Wait();

        _gameOverScreen.gameObject.SetActive(false); 
        var backToMenuBtn = _gameOverScreen.GetComponentInChildren<Button>(); 
        backToMenuBtn.onClick.AddListener(() =>
        {
            _gameOverScreen.gameObject.SetActive(false);
             Load(SceneName.HubScene, true);
        });
        
        Load(SceneName.MenuScene, true); 
    }
    
    public void StartNewGame()
    {
        _runtimePlayerDeck.InitFrom(_starterDeck); 
        _runtimeWorldMap.InitFrom(_starterWorldMap);
        
        SaveGameState();

        Load(SceneName.HubScene, true);
    }

    public void Load(SceneName scene, bool isAdditive = false, Action onSceneLoaded = null)
    {
       _onLoadSceneEvent.RaiseEvent(new SceneInstance() {
          IsAdditive = isAdditive, OnLoaded = onSceneLoaded, SceneName = scene
       }); 
    }

    public void OnGameFinished(bool hasWin)
    {
        if (hasWin)
        {
            _runtimeLevel.CompleteCurrentBattle();
            if (_runtimeLevel.IsComplete())
            {
                _runtimeWorldMap.Visits(_runtimeLevel.LevelName);
                var wonCard = _runtimeLevel.CurrentNode.Opponent.Deck.GetStrongestCard();
                _runtimeWonCard.Set(_runtimeLevel.CurrentNode.Opponent.Deck.GetStrongestCard());
                _runtimePlayerDeck.AddCard(wonCard); 
                Load(SceneName.LootScene, true);
            } 
            else  
                Load(SceneName.QuestScene, true);
        }

        if (!hasWin)
            _gameOverScreen.gameObject.SetActive(true);

        SaveGameState();
    }
}
