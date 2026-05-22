using UnityEngine;

public class DisguisePickup : MonoBehaviour
{
    public float disguiseDuration = 10f;

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        PlayerDisguise disguise = other.GetComponent<PlayerDisguise>();
        if (disguise != null)
        {
            disguise.ActivateDisguise(disguiseDuration);
            gameObject.SetActive(false); 
        }
    }
}