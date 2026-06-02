using UnityEngine;

public class MoyaControll : MonoBehaviour
{
    public float moyaSpd = 0.5f;
    public float posMaxY = 20.0f;
    void Start()
    {
        Application.targetFrameRate = 60;
    }

    void Update()
    {
        if(transform.position.y < posMaxY)
        {
            transform.Translate(Vector3.up * moyaSpd * Time.deltaTime);
        }
    }
}
