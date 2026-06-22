using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

// 通常のタイマーを管理するクラス
public class GameNormalTimer : MonoBehaviour
{
    private float timer = 0.0f;
    [SerializeField] private float limit = 60.0f;
    public TextMeshProUGUI normalTimerText;
    private GameStartTimer gameStartTimer;
    private Goal goal;

    // タイマーが停止しているかどうかを示すフラグ（例: タイムアップ後など）
    private bool isStop;
    public bool IsStop() { return isStop; }
    public void SetStop(bool value) { isStop = value; }

    void Start()
    {
        timer = 0.0f;
        normalTimerText.text = "00:00.00";
        gameStartTimer = FindAnyObjectByType<GameStartTimer>();
        if (gameStartTimer == null)
        {
            Debug.Log("ゲーム開始タイマーが見つかりません");
        }
        goal = FindAnyObjectByType<Goal>();
        if (goal == null)
        {
            Debug.Log("ゴールが見つかりません");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // ゲーム開始タイマーが存在し、ゲームが開始されている場合にタイマーを更新
        if (gameStartTimer != null && gameStartTimer.IsStarted() == true)
        {
            if (!isStop)
            {
                timer += Time.deltaTime;
                timer = Mathf.Min(timer, limit);
            }

            // 00:00.00の形式で表示
            int minutes = Mathf.FloorToInt(timer / 60);
            int seconds = Mathf.FloorToInt(timer % 60);
            int milliseconds = Mathf.FloorToInt((timer * 100) % 100);
            normalTimerText.text = string.Format("{0:00}:{1:00}.{2:00}", minutes, seconds, milliseconds);
        }

        if(timer >= limit) 
        {
            isStop = true;
        }

        if (goal.IsClear())
        {
            isStop = true;
        }
    }

    public float GetTime()
    {
        return timer;
    }

    public float GetLimit()
    {
        return limit;
    }
}
