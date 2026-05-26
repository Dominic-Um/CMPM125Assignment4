using UnityEngine;

public class DetectiveMarker : MonoBehaviour
{
    private bool isActive;
    public bool IsActive => isActive;

    public void Activate(Vector3 worldPosition)
    {
        transform.position = worldPosition;
        gameObject.SetActive(true);
        isActive = true;
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
        isActive = false;
    }
}
