using System.Linq;
using TMPro;
using TripleTriad.Core.SOs;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuView : MonoBehaviour
{
    [SerializeField] private StringReference _playerName; 
    
    private Button _newBtn;
    private Button _continueBtn;
    private Transform _newGamePanel;
    
    private Button _startBtn;
    
    void Awake()
    {
        MusicManager.Instance?.PlayMusic(Music.Irelia_Login_Music);

        _newGamePanel = GetComponentsInChildren<Transform>().First(g => g.name == "NewGamePanel");
        _newBtn = GetComponentsInChildren<Button>().First(g => g.name == "NewGameBtn");
        _continueBtn = GetComponentsInChildren<Button>().First(g => g.name == "ContinueBtn");

        _newGamePanel.gameObject.SetActive(false);

        _newBtn.onClick.AddListener(() => ShowNewGamePanel(true));
        _newBtn.onClick.AddListener(StartNewGame);
        
        SetUI(!string.IsNullOrEmpty(_playerName.Variable.Value));
    }
    
    public void HideNewGamePanel()
    {
       ShowNewGamePanel(false); 
    }

    private void StartNewGame()
    {
        SoundManager.Instance?.PlaySound(SoundManager.Sound.ClicButton);
        string playerName = _newGamePanel.GetComponentInChildren<TMP_InputField>().text;
        if (string.IsNullOrWhiteSpace(playerName))
           return;
        _playerName.Variable.SetValue(playerName);       
    }

    private void SetUI(bool gameExists)
    {
        _continueBtn.interactable = gameExists;
        _continueBtn.enabled = gameExists;
    }

    
    private void ShowNewGamePanel(bool visible)
    {
        _newGamePanel.gameObject.SetActive(visible);
        if (visible)
        {
            _startBtn = _newGamePanel.GetComponentsInChildren<Button>().First(g => g.name == "StartBtn");
            _startBtn.onClick.AddListener(StartNewGame);
        }

        _continueBtn.gameObject.SetActive(!visible);
        _newBtn.gameObject.SetActive(!visible);
    }
}
