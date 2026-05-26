using Unity.Cinemachine;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public AudioClip coinSFX;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerHealth> () != null)
        {
            CoinManager.Instance.AddCoin();
            SoundManager.instance.PlayAudio(coinSFX, this.transform, 1);
            Destroy(gameObject);
        }
    }
}
