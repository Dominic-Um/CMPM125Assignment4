using UnityEngine;

public class DetectiveToggle : MonoBehaviour
{
    [SerializeField] private InputReader inputReader;
    [SerializeField] private MarkerSystem markerSystem;
    [SerializeField] private BlockAbility blockAbility;

    private bool markerActive = true;

    private void Awake()
    {
        blockAbility.enabled = false;
        inputReader.SetDetective();
    }

    private void OnEnable()
    {
        inputReader.ToggleEvent += HandleToggle;
    }

    private void OnDisable()
    {
        inputReader.ToggleEvent += HandleToggle;
    }

    private void HandleToggle()
    {
        if (markerActive)
        {
            blockAbility.enabled = true;
            markerSystem.enabled = false;
            markerActive = false;
            Debug.Log("Block mode!");
        }else
        {
            Debug.Log("Marker mode!");
            blockAbility.enabled = false;
            markerSystem.enabled = true;
            markerActive = true;
        }
    }
}
