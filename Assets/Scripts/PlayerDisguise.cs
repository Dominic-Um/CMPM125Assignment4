using UnityEngine;

public class PlayerDisguise : MonoBehaviour
{
    public bool IsDisguised { get; private set; } = false;

    private float disguiseTimer = 0f;
    private Renderer rend;
    private Color originalColor;

    void Start()
    {
        rend = GetComponent<Renderer>();
        if (rend != null)
            originalColor = rend.material.color;
    }

    void Update()
    {
        if (IsDisguised)
        {
            disguiseTimer -= Time.deltaTime;
            if (disguiseTimer <= 0f)
                DeactivateDisguise();
        }
    }

    public void ActivateDisguise(float duration)
    {
        IsDisguised = true;
        disguiseTimer = duration;

        if (rend != null)
            rend.material.color = Color.blue;
    }

    void DeactivateDisguise()
    {
        IsDisguised = false;
        disguiseTimer = 0f;

        if (rend != null)
            rend.material.color = originalColor;
    }
}