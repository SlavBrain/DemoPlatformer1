using UnityEngine;

public class CoinsSpawner : MonoBehaviour
{
    [SerializeField] private Coin coin;
    [SerializeField] private CoinDot[] _coinSpawnDots;

    void Start()
    {
        _coinSpawnDots = GetComponentsInChildren<CoinDot>();
        SpawnAllCoins();
    }

    private void SpawnAllCoins()
    {
        for(int i = 0; i < _coinSpawnDots.Length; i++)
        {
            SpawnCoin(i);
        }
    }

    private void SpawnCoin(int currentSpawnDot)
    {
        Instantiate(coin, _coinSpawnDots[currentSpawnDot].transform);
    }
}