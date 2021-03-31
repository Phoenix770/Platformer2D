using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Collector : MonoBehaviour
{
    [SerializeField] List<Collectible> _collectibles;
    [SerializeField] UnityEvent _onCollectionComplete;
    [SerializeField] Color _gizmoColor = new Color(0.1f, 0.1f, 0.1f, 1);

    TMP_Text _remainingText;
    int _countCollected;

    void Start()
    {
        _remainingText = GetComponentInChildren<TMP_Text>();
        foreach(var collectible in _collectibles)
        {
            collectible.OnPickedUp += ItemPickedUp;
        }
    }

    public void ItemPickedUp()
    {
        _countCollected++;
        int _countRemaining = _collectibles.Count - _countCollected;
        
        _remainingText?.SetText(_countRemaining.ToString());

        if(_countRemaining == 0)
            Unlock();

        if (_countRemaining > 0)
            return;
    }

    void OnValidate()
    {
        _collectibles = _collectibles.Distinct().ToList();
    }

    void Unlock()
    {
        _onCollectionComplete.Invoke();
    }

    void OnDrawGizmos()
    {
        foreach (var collectible in _collectibles)
        {
            if (UnityEditor.Selection.activeGameObject == gameObject)
                Gizmos.color = Color.yellow;
            else
                Gizmos.color = _gizmoColor;
            Gizmos.DrawLine(transform.position, collectible.transform.position);
        }
    }
}
