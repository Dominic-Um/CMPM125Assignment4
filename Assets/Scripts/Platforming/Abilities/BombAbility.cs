using UnityEngine;
using System;

public class BombAbility : MonoBehaviour
{
    [SerializeField] private InputReader inputReader;
    [SerializeField] private GameObject bomb;
    [SerializeField] private float throwForce;
    [SerializeField] private Transform throwPoint;
    [SerializeField] private float cooldownTime = 2f;
    [SerializeField] private int maxThrows = 5;
    private int throwsCount;
    private bool canThrow;

    public int BombRemaining()
    {
        int throws = maxThrows - throwsCount;

        if (throws < 0) return 0;

        return throws;
    }
    private Vector2 moveInput;
    private Vector2 lastDirection = Vector2.right;

    public event Action OnBombUsed;

    private void OnEnable()
    {
        canThrow = true;
        inputReader.MoveEvent += HandleMove;
        inputReader.Ability1Event += HandleThrow;
    }

    private void OnDisable()
    {
        inputReader.MoveEvent -= HandleMove;
        inputReader.Ability1Event -= HandleThrow;
    }

    private void HandleMove(Vector2 direction)
    {
        moveInput = direction;

        if (direction.x != 0)
        {
            lastDirection = new Vector2(Mathf.Sign(direction.x), 0);
        }
    }

    private void HandleThrow()
    {
        if (!canThrow || BombRemaining() <= 0) return;

        ThrowBomb();
    }

    private Vector2 GetThrowDirection()
    {
        if (moveInput != Vector2.zero) return moveInput.normalized;

        return lastDirection;
    }

    private void ThrowBomb()
    {
        throwsCount++;
        OnBombUsed?.Invoke();
        canThrow = false;
        GameObject b = Instantiate(bomb, throwPoint.position, Quaternion.identity);
        Rigidbody2D rb = b.GetComponent<Rigidbody2D>();
        rb.AddForce(GetThrowDirection() * throwForce, ForceMode2D.Impulse);

        Invoke(nameof(Cooldown), cooldownTime);
    }

    private void Cooldown()
    {
        canThrow = true;
    }

    public void AddBomb(int amount = 1)
    {
        throwsCount--;
        if (throwsCount > maxThrows) throwsCount = maxThrows;
        OnBombUsed?.Invoke();
    }
}
