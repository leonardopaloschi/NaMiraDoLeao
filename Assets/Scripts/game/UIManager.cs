using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public TextMeshProUGUI timerText;
    public TextMeshProUGUI coinsText;
    public TextMeshProUGUI finalTimerText;
    public TextMeshProUGUI finalCoinsText;

    private float timer;
    private int coins;

    private bool isGameRunning = true;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    void Update()
    {
        if (!isGameRunning) return;

        timer += Time.deltaTime;
        timerText.text = $"Tempo: {timer:F2}s";
    }

    public void AddCoin()
    {
        coins++;
        coinsText.text = $"Moedas: {coins}";
    }

    public void ShowFinalStats()
    {
        isGameRunning = false;
        finalTimerText.text = $"Você durou: {timer:F2}s";
        finalCoinsText.text = $"Você coletou: {coins} moedas";
    }

    public void ResetStats()
    {
        timer = 0f;
        coins = 0;
        isGameRunning = true;
        timerText.text = "Tempo: 0.00s";
        coinsText.text = "Moedas: 0";
    }
}
