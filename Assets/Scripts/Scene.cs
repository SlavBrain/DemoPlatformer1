using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Scene : MonoBehaviour
{
    [SerializeField] private int _maxCoinsInScene=2;
    [SerializeField] private bool _isCreatingAllCoinsOnStart = false;
    [SerializeField] private CoinsSpawner _coinSpawner;

    private int _currentCoinsInScene = 0;
    public UnityEvent CoinsCountIsLow;

    void Start()
    {
        _coinSpawner.ÑreatedCoin += AddCurrentCoinsValue;

        if (_maxCoinsInScene > _coinSpawner.CoinSpawnDots.Count)
            _maxCoinsInScene = _coinSpawner.CoinSpawnDots.Count;

        if (_isCreatingAllCoinsOnStart) 
        {
            while (_coinSpawner.CoinSpawnDots.Count != 0)
            {
                CoinsCountIsLow?.Invoke();
                Debug.Log("neeeed");
            }            
        }
    }

    private void Update()
    {
        if (_currentCoinsInScene < _maxCoinsInScene)
        {
            CoinsCountIsLow?.Invoke();
            Debug.Log("neeeed");
        }            
    }

    private void AddCurrentCoinsValue(Coin currentCoin)
    {
        _currentCoinsInScene++;
        currentCoin.OnPlayerTouch.AddListener(RemoveCurrentCoinsValue);
    }

    private void RemoveCurrentCoinsValue(CoinDot coinDot)
    {
        _currentCoinsInScene--;
    }
}
