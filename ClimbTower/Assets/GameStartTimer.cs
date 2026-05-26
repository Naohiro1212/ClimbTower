using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameStartTimer : MonoBehaviour
{
    private float countDown = 3.0f;
    public TextMeshProUGUI timerText;

    // あとでPlayerMovementとCameraControllerを制御するための参照を追加する予定
    // public PlayerMovement playerMovement;
    // public CameraController cameraController;

    private bool started = false;

    private void Start()
    {
        //playerMovement.canMove = false;
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

        //playerMovement.canMove = true;
        //scrollController.forceScroll = true;

        // 少し後に文字を消す
        Invoke(nameof(HideText), 1f);
    }

    private void HideText()
    {
        timerText.text = "";
    }
}
