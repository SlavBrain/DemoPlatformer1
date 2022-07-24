using UnityEngine;
using UnityEngine.Events;

public class Scene : MonoBehaviour
{
    [SerializeField] private int _maxCoinsInScene=2;
    [SerializeField] private bool _isCreatingAllCoinsOnStart = false;

    public UnityEvent CoinsCountIsLow;
    private CoinsSpawner _coinSpawner;
    private int _currentCoinsInScene = 0;    

    void Start()
    {
        _coinSpawner = GetComponentInChildren<CoinsSpawner>();

        if (_coinSpawner != null)
        {
            _coinSpawner.ÑreatedCoin += AddCurrentCoinsValue;

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

    private void Update()
    {
        if (_currentCoinsInScene < _maxCoinsInScene)
        {
            CoinsCountIsLow?.Invoke();
        }            
    }

    private void AddCurrentCoinsValue(Coin currentCoin)
    {
        _currentCoinsInScene++;
        currentCoin.OnPlayerTouch+=RemoveCurrentCoinsValue;
    }

    private void RemoveCurrentCoinsValue(CoinDot coinDot)
    {
        _currentCoinsInScene--;
    }
    
    private void RequiredMaxCoin()
    {
        while (_coinSpawner.CoinSpawnDots.Count != 0)
        {
            CoinsCountIsLow?.Invoke();
        }
    }
}
