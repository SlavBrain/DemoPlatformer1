using UnityEngine;
using UnityEngine.Events;

public class Coin : MonoBehaviour
{
    public UnityEvent<CoinDot> OnPlayerTouch = new UnityEvent<CoinDot>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Player>(out Player player))
        {
            Debug.Log("Есть касание");
            OnPlayerTouch.Invoke(this.GetComponentInParent<CoinDot>());
            OnPlayerTouch.RemoveAllListeners();
            Destroy(gameObject);
        }
    }
}
