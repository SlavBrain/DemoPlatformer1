using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class CoinSpawnDot: MonoBehaviour
{
    public delegate void ActionWithCoinSpawnDot(CoinSpawnDot coinSpawnDot);
    public event ActionWithCoinSpawnDot PlayerWentAway;

    private CircleCollider2D _circleCollider2D;

    private bool _isAvailableAfterTakingCoin=true;
    private CoinsSpawner _coinsSpawner;

    private void OnEnable()
    {
        _coinsSpawner = GetComponentInParent<CoinsSpawner>();
        _coinsSpawner.ÑreatedCoin += SubscribeToNewCoin;

        _circleCollider2D = GetComponent<CircleCollider2D>();
        _circleCollider2D.isTrigger = true;
        _circleCollider2D.radius = 1f;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player) && _isAvailableAfterTakingCoin == false)
        {
            DoAvailablwe();
            PlayerWentAway?.Invoke(this);
        }            
    }

    private void DoAvailablwe()
    {
        _isAvailableAfterTakingCoin = true;
    }

    private void DoUnavalible(CoinSpawnDot coinSpawnDot)
    {
        if (this == coinSpawnDot)
        {
            _isAvailableAfterTakingCoin = false;
        }        
    }

    private void SubscribeToNewCoin(Coin coin, CoinSpawnDot coinSpawnDot)
    {
        if (this == coinSpawnDot)
        {
            coin = GetComponentInChildren<Coin>();
            coin.OnPlayerTouched += DoUnavalible;
        }        
    }
}
