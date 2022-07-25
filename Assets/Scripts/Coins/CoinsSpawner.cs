using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class CoinsSpawner : MonoBehaviour
{
    [SerializeField] private Coin _coin;    
    [SerializeField] private Scene _scene;

    public delegate void ActionWithCoin(Coin coin,CoinSpawnDot coinSpawnDot);
    public event ActionWithCoin CreatedCoin;

    private Coin _createdCoin;

    public List<CoinSpawnDot> CoinSpawnDots { get; private set; }

    private void OnEnable()
    {        
        CoinSpawnDots = new List<CoinSpawnDot>(GetComponentsInChildren<CoinSpawnDot>());
        _scene.CoinsCountIsLow+=SpawnCoin;

        foreach(CoinSpawnDot dot in CoinSpawnDots)
        {
            dot.PlayerWentAway += AddPoint;
        }
    }

    private void OnDisable()
    {
        CreatedCoin = null;
    }

    private void SpawnCoin()
    {
        int currentSpawnDot = Random.Range(0, CoinSpawnDots.Count);
        CreateCoin(currentSpawnDot);
        CoinSpawnDots.RemoveAt(currentSpawnDot);
    }

    private void CreateCoin(int position)
    {
        _createdCoin = Instantiate(_coin, CoinSpawnDots[position].transform);
        CreatedCoin?.Invoke(_createdCoin,CoinSpawnDots[position]);               
    }

    private void AddPoint(CoinSpawnDot deletedCoinDot)
    {
        CoinSpawnDots.Add(deletedCoinDot);
    }
}