using UnityEngine;

public class MoyaControll : MonoBehaviour
{
    public float moyaSpd = 0.5f;
    void Start()
    {
        Application.targetFrameRate = 60;
    }

    void Update()
    {
        if(transform.position.y < 10.0f)
        {
            transform.Translate(Vector3.up * moyaSpd * Time.deltaTime);
        }
    }
}
