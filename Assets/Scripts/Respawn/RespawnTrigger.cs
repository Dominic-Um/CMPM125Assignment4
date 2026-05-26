using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnTrigger : MonoBehaviour
{
    public Transform spawnPoint;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            HandleSceneManager.instance.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
