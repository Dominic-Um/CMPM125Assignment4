using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RevealAreasUI : MonoBehaviour
{
    [SerializeField] private UncoverDarkAreas uncoverDarkAreas;
    [SerializeField] private Button toggleButton;
    [SerializeField] private TextMeshProUGUI buttonText;

    private bool isDark = true;

    private void OnEnable()
    {
        GameManager.OnGameStateChange += OnGameStateChanged;
    }

    private void OnDisable()
    {
        GameManager.OnGameStateChange -= OnGameStateChanged;
    }

    private void Start()
    {
        toggleButton.onClick.AddListener(ToggleDarkAreas);
    }

    private void OnGameStateChanged(GameState state)
    {
        if (state == GameState.Platforming)
        {
            uncoverDarkAreas.HandleDarkAreas(true);
            toggleButton.gameObject.SetActive(false);
        }
    }

    private void ToggleDarkAreas()
    {
        isDark = !isDark;
        uncoverDarkAreas.HandleDarkAreas(isDark);

        buttonText.text = isDark ? "Reveal Areas" : "Hide Areas";
    }
}
