using System;
using TMPro;
using TripleTriad.Core.EventArchitecture.Events;
using TripleTriad.Core.EventArchitecture.Listeners;
using UnityEngine;
using UnityEngine.UI;

namespace TripleTriad.Battle
{
    public class ScoreBarView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _playerScoreTxt;
        [SerializeField] private TMP_Text _ennemyScoreTxt;

        [SerializeField] private Image _playerScoreBar;
        [SerializeField] private Image _ennemyScoreBar;

        void Awake()
        {
            _playerScoreBar.fillAmount = 0;
            _ennemyScoreBar.fillAmount = 0;
        }

        public void SetPlayerScore(int newScore)
        {
            _playerScoreTxt.SetText(newScore.ToString()); 
            _playerScoreBar.fillAmount = newScore / 9f;
        }
        
        public void SetOpponentScore(int newScore)
        {
            _ennemyScoreTxt.SetText(newScore.ToString());
            _ennemyScoreBar.fillAmount = newScore / 9f;
        }
    }
}
