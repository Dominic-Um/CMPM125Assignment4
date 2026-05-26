using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speed = 8f;
    [SerializeField] int damage = 1;
    private int direction = 1;

    public void SetDirection(int dir)
    {
        direction = dir;
    }

    void Update()
    {
        transform.Translate(Vector2.right * direction * speed * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D col)
{
    Debug.Log("Bullet hit: " + col.gameObject.name);
    if (col.gameObject.CompareTag("Enemy")) return;

    PlayerHealth player = col.gameObject.GetComponent<PlayerHealth>();
    if (player != null)
    {
        player.TakeDamage(damage);
        Destroy(gameObject);
    }
}
}