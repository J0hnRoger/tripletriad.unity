using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TestFinishLevelView : MonoBehaviour
{
    [SerializeField] private MapView _mapView;
    [SerializeField] private TMP_Text _finishText;

    private void Awake()
    {
        _finishText.gameObject.SetActive(false);
    }

    public void OnEnable()
    {
        _mapView.OnLevelFinished += ShowFinishText;
    }

    public void OnDisable()
    {
        _mapView.OnLevelFinished -= ShowFinishText;
    }

    private void ShowFinishText(MapLevelData obj)
    {
        _finishText.gameObject.SetActive(true);
    }
}
