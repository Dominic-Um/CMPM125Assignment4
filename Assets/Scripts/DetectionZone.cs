using UnityEngine;

public class DetectionZone : MonoBehaviour
{
    private GuardAI guard;

    void Start()
    {
        guard = GetComponentInParent<GuardAI>();

        if (guard == null)
            return;
    }

    void OnTriggerEnter(Collider other)
    {
        if (guard == null) return;
        if (!other.CompareTag("Player")) return;

        float dist = Vector3.Distance(guard.transform.position, other.transform.position);

        if (dist > 20f)
            return;

        guard.PlayerInZone = true;
        guard.ForceChase();
    }

    void OnTriggerExit(Collider other)
    {
        if (guard == null) return;
        if (!other.CompareTag("Player")) return;

        float dist = Vector3.Distance(guard.transform.position, other.transform.position);

        if (dist > 20f)
            return;

        guard.PlayerInZone = false;
    }
}