using UnityEngine;

public class BombExplosionEffect : MonoBehaviour
{
    [SerializeField] private float duration = 0.4f;
    
    private float targetScale;
    private float elapsed = 0f;

    public void Init(float radius)
    {
        targetScale = radius * 2f; 
        transform.localScale = Vector3.one * targetScale;
    }

    private void Update()
    {
        elapsed += Time.deltaTime;

        if (elapsed >= duration)
        {
            Destroy(gameObject);
        }
    }
}
