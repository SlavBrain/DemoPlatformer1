using UnityEngine;
using UnityEngine.Events;

public class Coin : MonoBehaviour
{
    public delegate void ActionWithCoinDot(CoinDot coinDot);
    public ActionWithCoinDot OnPlayerTouch;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Player>(out Player player))
        {
            OnPlayerTouch.Invoke(this.GetComponentInParent<CoinDot>());
            Destroy(gameObject);
        }
    }
}
