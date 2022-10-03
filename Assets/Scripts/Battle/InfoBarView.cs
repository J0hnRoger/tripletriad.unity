using System;
using System.Linq;
using TMPro;
using TripleTriad.Engine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace TripleTriad.Battle
{
    public enum InfoBarMode
    {
        BUTTON,
        MESSAGE
    }

    public class InfoBarView : MonoBehaviour
    {
        private TMP_Text _infoBarTxt;
        private Image _infoBarColor;
        public Action OnInfoBarClick;

        [SerializeField] public Color infoColor;
        private Button _infoBtn;

        private InfoBarMode _mode = InfoBarMode.BUTTON;

        // Start is called before the first frame update
        void Awake()
        {
            _infoBarTxt = GetComponentsInChildren<TMP_Text>().First();
            _infoBarColor = GetComponent<Image>();
            _infoBtn = GetComponent<Button>();
            _infoBtn.onClick.AddListener(OnClick);
        }

        private void OnEnable()
        {
            BattleManager.OnTurnChangedEvent += SetTurnMessage;
        }

        private void OnDisable()
        {
            BattleManager.OnTurnChangedEvent -= SetTurnMessage;
        }
        
        private void OnClick()
        {
            if(_mode == InfoBarMode.BUTTON)
                OnInfoBarClick?.Invoke(); 
        } 

        public void SetTurnMessage(BattleState battleState, PlayerBehaviour playerBehaviour)
        {
            _mode = InfoBarMode.MESSAGE;
            _infoBarColor.color = playerBehaviour.PlayerColor;
            _infoBarTxt.SetText($"Tour de: {playerBehaviour.CurrentPlayer.Name}");
        }

        public void SetActionableMessage(string message)
        {
            _mode = InfoBarMode.BUTTON;
            _infoBarColor.color = infoColor;
            _infoBarTxt.SetText(message);
        }
    }
}