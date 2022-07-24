using UnityEngine;
using UnityEngine.Events;

public class Scene : MonoBehaviour
{
    [SerializeField] private int _maxCoinsInScene=2;
    [SerializeField] private bool _isCreatingAllCoinsOnStart = false;

    public delegate void ActionWithCoins();
    public event ActionWithCoins CoinsCountIsLow;
    private CoinsSpawner _coinSpawner;

    private int _currentCoinsInScene = 0;    

    private void Start()
    {
        _coinSpawner = GetComponentInChildren<CoinsSpawner>();

        if (_coinSpawner != null)
        {
            _coinSpawner.ÑreatedCoin+=AddCurrentCoinsValue;

            if (_maxCoinsInScene > _coinSpawner.CoinSpawnDots.Count)
                _maxCoinsInScene = _coinSpawner.CoinSpawnDots.Count;

            if (_isCreatingAllCoinsOnStart)
            {
                RequiredMaxCoin();
            }
        }
        else
        {
            Debug.LogError("Not found CoinSpawn in child");
        }        
    }

    private void AddCurrentCoinsValue(Coin currentCoin,CoinSpawnDot coinSpawnDot)
    {
        _currentCoinsInScene++;
        currentCoin.OnPlayerTouched+=RemoveCurrentCoinsValue;
    }

    private void RemoveCurrentCoinsValue(CoinSpawnDot coinDot)
    {
        _currentCoinsInScene--;

        if (_currentCoinsInScene < _maxCoinsInScene)
        {
            CoinsCountIsLow?.Invoke();
        }
    }
    
    private void RequiredMaxCoin()
    {
        while (_coinSpawner.CoinSpawnDots.Count != 0)
        {
            CoinsCountIsLow?.Invoke();
        }
    }
}
