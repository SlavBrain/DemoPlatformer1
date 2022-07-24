using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class CoinsSpawner : MonoBehaviour
{
    [SerializeField] private Coin coin;
    [SerializeField] public List<CoinDot> CoinSpawnDots { get; private set; }

    [SerializeField] private Scene _scene;

    public delegate void ActionWithCoin(Coin coin);
    public event ActionWithCoin ÑreatedCoin;
    private Coin _createdCoin;
    

    void OnEnable()
    {        
        CoinSpawnDots = new List<CoinDot>(GetComponentsInChildren<CoinDot>());
        _scene.CoinsCountIsLow.AddListener(SpawnCoin);
    }

    private void SpawnCoin()
    {
        int currentSpawnDot = Random.Range(0, CoinSpawnDots.Count);
        CreateCoin(currentSpawnDot);
        CoinSpawnDots.RemoveAt(currentSpawnDot);
    }

    private void CreateCoin(int position)
    {
        _createdCoin = Instantiate(coin, CoinSpawnDots[position].transform);
        _createdCoin.OnPlayerTouch+=AddPoint;
        ÑreatedCoin?.Invoke(_createdCoin);
    }
    private void AddPoint(CoinDot deletedCoinDot)
    {
        CoinSpawnDots.Add(deletedCoinDot);
    }
}