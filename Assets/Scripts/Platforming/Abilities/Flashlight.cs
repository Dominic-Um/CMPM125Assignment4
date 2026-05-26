using UnityEngine;

public class Flashlight : MonoBehaviour
{
    [SerializeField] private InputReader inputReader;
    [SerializeField] private SpriteMask mask;
    [SerializeField] private int maxUses = 5;
    [SerializeField] private float duration = 5f;
    private bool flashlightActive = false;
    private int usesCount = 1;

    private void OnEnable()
    {
        inputReader.Ability2Event += EnableFlashlight;
    }

    private void OnDisable()
    {
        inputReader.Ability2Event -= EnableFlashlight;
    }

    public void EnableFlashlight()
    {

        if (usesCount > maxUses || flashlightActive)
        {
            return;
        }

        usesCount++;
        mask.enabled = true;
        flashlightActive = true;
        Invoke(nameof(ResetFlashlight), duration);

    }

    private void ResetFlashlight()
    {
        mask.enabled = false;
        flashlightActive = false;
    }

}
