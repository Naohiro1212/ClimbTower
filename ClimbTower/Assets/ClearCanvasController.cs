using UnityEngine;
using UnityEngine.UI;

public class ClearCanvasController : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private Slider slider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (canvas == null)
        {
            Debug.Log("キャンバスが設定されていません");
        }
        if (slider == null)
        {
            Debug.Log("スライダーが設定されていません");
        }

        canvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (slider.value == 1.0f)
        {
            canvas.enabled = true;
        }
    }
}
