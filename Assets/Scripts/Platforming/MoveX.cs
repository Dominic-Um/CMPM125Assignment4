using UnityEngine;

public class MoveX : MonoBehaviour
{
    public float speed = 2f;
    public float distance = 5f;

    private Vector3 startPosition;
    private bool movingLeft = true;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        if (movingLeft)
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
            if (transform.position.x <= startPosition.x - distance)
            {
                movingLeft = false;
            }
        }
        else
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
            if (transform.position.x >= startPosition.x)
            {
                movingLeft = true;
            }
        }
    }
}
