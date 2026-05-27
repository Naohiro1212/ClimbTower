using UnityEngine;
using TMPro;

// 通常のタイマーを管理するクラス
public class GameNormalTimer : MonoBehaviour
{
    private float timer = 0.0f;
    public TextMeshProUGUI timerText;
    GameStartTimer gameStartTimer;

    void Start()
    {
        timer = 0.0f;
        timerText.text = "00:00.00";
        gameStartTimer = FindAnyObjectByType<GameStartTimer>();
        if (gameStartTimer == null)
        {
            Debug.Log("ゲーム開始タイマーが見つかりません");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // ゲーム開始タイマーが存在し、ゲームが開始されている場合にタイマーを更新
        if (gameStartTimer != null && gameStartTimer.IsStarted() == true)
        {
            timer += Time.deltaTime;

            // 00:00.00の形式で表示
            int minutes = Mathf.FloorToInt(timer / 60);
            int seconds = Mathf.FloorToInt(timer % 60);
            int milliseconds = Mathf.FloorToInt((timer * 100) % 100);
            timerText.text = string.Format("{0:00}:{1:00}.{2:00}", minutes, seconds, milliseconds);
        }
    }

    public float GetTime()
    {
        return timer;
    }
}
