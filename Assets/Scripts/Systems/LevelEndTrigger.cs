using UnityEngine;

public class LevelEndTrigger : MonoBehaviour
{
    [SerializeField] private string nextScene;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerHealth>() != null)
        {
            HandleSceneManager.instance.LoadScene(nextScene);
        }
    }
}
