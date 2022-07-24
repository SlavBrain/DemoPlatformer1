using UnityEngine;
using UnityEngine.Events;

public class Coin : MonoBehaviour
{
    public delegate void ActionWithCoinSpawnDot(CoinSpawnDot coinSpawnDot);
    public event ActionWithCoinSpawnDot OnPlayerTouched;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Player>(out Player player))
        {
            OnPlayerTouched?.Invoke(GetComponentInParent<CoinSpawnDot>());            
            Destroy(gameObject);
        }
    }

    private void OnDisable()
    {
        OnPlayerTouched = null;
    }
}
