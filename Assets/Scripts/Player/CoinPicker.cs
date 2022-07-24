using UnityEngine;

[RequireComponent(typeof(Player))]
public class CoinPicker : MonoBehaviour
{
    [SerializeField] private int _score;
    [SerializeField] private CoinsSpawner _coinsSpawner;

    private Player _player;

    private void OnEnable()
    {
        _coinsSpawner.ÑreatedCoin += SubscribeToCoin;
        _player = GetComponent<Player>();
        _player.Dead.AddListener(ResetScore);
    }

    private void TakeCoin(CoinSpawnDot TouchedDot)
    {
        _score++;
    }

    private void SubscribeToCoin(Coin currentCoin, CoinSpawnDot coinSpawnDot)
    {
        currentCoin.OnPlayerTouched += TakeCoin;
    }

    private void ResetScore()
    {
        _score = 0;
    }
}
