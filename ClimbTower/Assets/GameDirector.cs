using UnityEngine;
using TMPro;

public class GameDirector : MonoBehaviour
{
    [SerializeField] private MoyaControll moyaScript;
    [SerializeField] private CameraController cameraScript;
    float time = 0.0f;

    /// <summary>
    /// 各段階のスピードと、何秒経った後にスピードを上げるか
    /// </summary>
    private const int speedStepsNum = 4;
    private const int speedUpTimingsNum = 3;

    [SerializeField] private float[] speedSteps = new float[speedStepsNum];
    [SerializeField] private float[] speedUpTiming = new float[speedUpTimingsNum];


    void Start()
    {
        // モヤスクリプトが見つからなければデバグ出力
        if (moyaScript == null)
        {
            Debug.Log("モヤが指定されていません");
        }

        // カメラスクリプトが見つからなければデバグ出力
        if (cameraScript == null)
        {
            Debug.Log("カメラが指定されていません");
        }
    }

    void Update()
    {
        this.time += Time.deltaTime;

        if (this.time >= speedUpTiming[2])
        {
            this.moyaScript.moyaSpd = speedSteps[3];
            this.cameraScript.scrollSpd = speedSteps[3];
        }
        else if (this.time >= speedUpTiming[1])
        {
            this.moyaScript.moyaSpd = speedSteps[2];
            this.cameraScript.scrollSpd = speedSteps[2];
        }
        else if(this.time >= speedUpTiming[0])
        {
            this.moyaScript.moyaSpd = speedSteps[1];
            this.cameraScript.scrollSpd = speedSteps[1];
        }
        else
        {
            this.moyaScript.moyaSpd = speedSteps[0];
            this.cameraScript.scrollSpd = speedSteps[0];
        }
    }
}
