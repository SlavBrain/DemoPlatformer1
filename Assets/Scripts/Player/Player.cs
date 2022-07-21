using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private int _score;
    [SerializeField] private CoinsSpawner _coinsSpawner;
    private Coin _coin;

    private void OnEnable()
    {
        _coinsSpawner.�reatedCoin += SubscribeToCoin;
    }

    private void TakeCoin(CoinDot TouchedDot)
    {
        _score++;
    }

    private void SubscribeToCoin(Coin currentCoin)
    {
        currentCoin.OnPlayerTouch.AddListener(TakeCoin);
        Debug.Log("���������� �� ������");
    }
}

