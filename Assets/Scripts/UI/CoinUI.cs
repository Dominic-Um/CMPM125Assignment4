using UnityEngine;
using TMPro;

public class CoinUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinText;

    private void OnDisable()
    {
        CoinManager.Instance.OnCoinCollect -= OnCoinCollected;
    }

    private void Start()
    {
        CoinManager.Instance.OnCoinCollect += OnCoinCollected;
        coinText.text = CoinManager.Instance.CoinCount.ToString();
    }

    private void OnCoinCollected()
    {
        coinText.text = CoinManager.Instance.CoinCount.ToString();
    }
}
