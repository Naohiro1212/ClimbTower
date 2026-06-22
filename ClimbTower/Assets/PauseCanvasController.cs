using UnityEngine;
using UnityEngine.InputSystem;

public class PauseCanvasController : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject clearPanel;
    [SerializeField] private GameStartTimer gameStartTimer;
    [SerializeField] private GameNormalTimer gameNormalTimer;

    private void Start()
    {
        if (pausePanel == null)
        {
            Debug.LogError("PausePanel が設定されていません");
            return;
        }

        if (clearPanel == null)
        {
            Debug.LogError("ClearPanel が設定されていません");
        }
        pausePanel.SetActive(false);
    }

    private void Update()
    {
        if (Keyboard.current == null)
        {
            return;
        }

        if (gameStartTimer.IsStarted() == false)
        {
            return;
        }

        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            bool isPauseOpen = !pausePanel.activeSelf;
            pausePanel.SetActive(isPauseOpen);

            if (gameNormalTimer != null)
            {
                gameNormalTimer.SetStop(isPauseOpen);
            }
        }
    }
}
