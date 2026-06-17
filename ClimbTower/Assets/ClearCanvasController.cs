using UnityEngine;
using UnityEngine.UI;

public class ClearCanvasController : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private Goal goal;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (canvas == null)
        {
            Debug.Log("キャンバスが設定されていません");
        }
        if (goal == null)
        {
            Debug.Log("ゴールが設定されていません");
        }
        canvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (goal.IsClear())
        {
            canvas.enabled = true;
        }
    }
}
