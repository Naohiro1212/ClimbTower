using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameStartTimer : MonoBehaviour
{
    private float countDown = 3.0f;
    public TextMeshProUGUI timerText;

    // あとでPlayerControllerとCameraControllerを制御するための参照を追加する予定
    public PlayerController playerController;
    // public CameraController cameraController;

    private bool started = false;

    private void Start()
    {
        playerController.canMove = false;
        //cameraController.forceScroll = false;
    }

    private void Update()
    {
        if (started) return;

        countDown -= Time.deltaTime;

        if (countDown > 0)
        {
            timerText.text = Mathf.Ceil(countDown).ToString();
        }
        else
        {
            timerText.text = "START!";
            StartGame();
        }
    }

    private void StartGame()
    {
        started = true;

        playerController.canMove = true;
        //scrollController.forceScroll = true;

        // 少し後に文字を消す
        Invoke(nameof(HideText), 1f);
    }

    private void HideText()
    {
        timerText.text = "";
    }

    public bool IsStarted()
    {
        return started;
    }
}
