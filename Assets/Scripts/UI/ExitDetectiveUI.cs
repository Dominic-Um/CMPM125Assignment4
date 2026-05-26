using UnityEngine;
using UnityEngine.UI;

public class ExitDetectiveUI : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private GameObject helpText;

    public void ExitDetectiveMode()
    {
        GameManager.Instance.UpdateGameState(GameState.Platforming);
        button.interactable = false;
        helpText.SetActive(false);
        button.gameObject.SetActive(false);
    }
}
