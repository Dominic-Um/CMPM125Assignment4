using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitDoor : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (!StealableItem.IsStolen)
        {
            Debug.Log("You need to steal the item first!");
            return;
        }

        WinScreen.Instance?.ShowWin();
    }
}