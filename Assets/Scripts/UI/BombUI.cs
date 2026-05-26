using UnityEngine;
using TMPro;

public class BombUI : MonoBehaviour
{
    [SerializeField] private BombAbility bombAbility;
    [SerializeField] private TextMeshProUGUI bombNumberText;

    private void OnEnable()
    {
        bombAbility.OnBombUsed += OnBombUsed;
    }

    private void OnDisable()
    {
        bombAbility.OnBombUsed -= OnBombUsed;
    }

    private void Start()
    {
        bombNumberText.text = bombAbility.BombRemaining().ToString();
    }

    private void OnBombUsed()
    {
        bombNumberText.text = bombAbility.BombRemaining().ToString();
    }

}
