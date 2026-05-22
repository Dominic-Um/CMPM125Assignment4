using UnityEngine;

public class StealableItem : MonoBehaviour
{
    public static bool IsStolen = false;

    void Start()
    {
        IsStolen = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        IsStolen = true;
        gameObject.SetActive(false);
        Debug.Log("Item stolen!");
    }
}