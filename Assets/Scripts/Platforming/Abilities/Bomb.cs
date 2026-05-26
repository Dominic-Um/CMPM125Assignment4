using UnityEngine;
using SupanthaPaul;

public class Bomb : MonoBehaviour
{
    [SerializeField] private GameObject explosionVisual;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float explosionTime = 2f;
    [SerializeField] private float explosionRadius;
    public AudioClip explosionSFX;

    private bool hasExploded;

    private void Start()
    {
        Invoke(nameof(Explode), explosionTime);
    }

    private void Explode()
    {
        SoundManager.instance.PlayAudio(explosionSFX, this.transform, 1);

        if (hasExploded) return;
        hasExploded = true;

        CancelInvoke(nameof(Explode));

        GameObject visual = Instantiate(explosionVisual, transform.position, Quaternion.identity);
        visual.GetComponent<BombExplosionEffect>().Init(explosionRadius);

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, explosionRadius, layerMask);
        foreach (Collider2D hit in hits)
        {
            if (hit.GetComponent<IDamageable>() != null)
            {
                hit.GetComponent<IDamageable>().TakeDamage(1000);
            }
        }

        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>() != null) return;

        Debug.Log(collision.gameObject.name);
        Explode();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
