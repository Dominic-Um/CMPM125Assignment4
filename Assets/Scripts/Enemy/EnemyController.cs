using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float patrolDistance = 3f;
    [SerializeField] private int damage = 1;
    [SerializeField] private float damageCooldown = 1f;
    [SerializeField] private Vector2 patrolDirection = Vector2.right;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private LayerMask obstacleLayer;
    [SerializeField] private float raycastDistance = 0.6f;

    private Vector2 startPos;
    private Vector2 currentDirection;
    private float cooldownTimer = 0f;

    private void Start()
    {
        startPos = transform.position;
        currentDirection = patrolDirection.normalized;
    }

    private void Update()
    {
        CheckCollision();
        Patrol();
        if (cooldownTimer > 0f)
            cooldownTimer -= Time.deltaTime;
    }

    private void Patrol()
    { 
        transform.Translate(currentDirection * moveSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, startPos) >= patrolDistance)
        {
            ReverseDirection();
        }
    }

    private void ReverseDirection()
    {
        currentDirection *= -1;
        transform.localScale = new Vector3(
                Mathf.Abs(transform.localScale.x) * Mathf.Sign(currentDirection.x),
                transform.localScale.y,
                transform.localScale.z
            );
    }

    private void CheckCollision()
    {
        if (Physics2D.Raycast(transform.position, currentDirection, raycastDistance, obstacleLayer))
        {
            ReverseDirection();
            return;
        }

        RaycastHit2D playerHit = Physics2D.Raycast(transform.position, currentDirection, raycastDistance, playerLayer);
        if (playerHit && cooldownTimer <= 0f)
        {
            IDamageable damageable = playerHit.collider.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(damage);
                cooldownTimer = damageCooldown;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerHealth>() && cooldownTimer <= 0f)
        {
            IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(damage);
                cooldownTimer = damageCooldown;
            }
        }
    }
}