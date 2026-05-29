using UnityEngine;
using UnityEngine.InputSystem;

public class PauseCanvasController : MonoBehaviour
{
    [SerializeField] private Canvas canvas;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (canvas == null)
        {
            Debug.Log("キャンバスが設定されていません");
        }

        canvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            canvas.enabled = !canvas.enabled;
        }
    }
}
