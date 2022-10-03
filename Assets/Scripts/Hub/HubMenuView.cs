using TMPro;
using TripleTriad.Core.SOs;
using UnityEngine;

public class HubMenuView : MonoBehaviour
{
    [SerializeField] private TMP_Text _playerNameTxt;
    [SerializeField] private StringReference _playerName;

    public void Awake()
    {
        _playerNameTxt.SetText(_playerName);
    }

    
}
