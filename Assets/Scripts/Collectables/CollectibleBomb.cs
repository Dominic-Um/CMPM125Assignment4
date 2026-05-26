using UnityEngine;

public class CollectibleBomb : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<BombAbility>() != null)
        {
            collision.GetComponent<BombAbility>().AddBomb();
            Destroy(gameObject);
        }
    }
}
