using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TripleTriad.Battle
{
    public class EndResultView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _resultTxt;
        [SerializeField] private Button _nextButton;
    
        public Action OnNextClicked;

        void Awake()
        {
            gameObject.SetActive(false);
        } 

        public void SetResult(bool isWinner, Action onNext)
        {
            gameObject.SetActive(true);
            string message = isWinner 
                ? "Felicitation! Continuez la campagne" 
                : "Perdu! Retenter votre chance";

            _resultTxt.SetText(message);    

            _nextButton.onClick.AddListener(onNext.Invoke);
        }
    }
}
