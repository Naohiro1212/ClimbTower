using UnityEngine;
using UnityEngine.InputSystem;

public class PauseCanvasController : MonoBehaviour
{
    [SerializeField] private Canvas pauseCanvas;
    [SerializeField] private Canvas clearCanvas;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (pauseCanvas == null)
        {
            Debug.Log("キャンバスが設定されていません");
        }

        if (clearCanvas == null)
        {
            Debug.Log("クリアキャンバスが設定されていません。");
        }

        pauseCanvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Keyboard.current.escapeKey.wasPressedThisFrame && !clearCanvas.enabled)
        {
            pauseCanvas.enabled = !pauseCanvas.enabled;
        }
    }
}
