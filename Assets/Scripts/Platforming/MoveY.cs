using UnityEngine;

public class MoveY : MonoBehaviour
{
    public float speed = 2f;
    public float distance = 5f;

    private Vector3 startPosition;
    private bool movingUp = true;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        if (movingUp)
        {
            transform.position += Vector3.up * speed * Time.deltaTime;
            if (transform.position.y >= startPosition.y + distance)
            {
                movingUp = false;
            }
        }
        else
        {
            transform.position += Vector3.down * speed * Time.deltaTime;
            if (transform.position.y <= startPosition.y)
            {
                movingUp = true;
            }
        }
    }
}
