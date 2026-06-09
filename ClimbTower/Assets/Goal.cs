using UnityEngine;

public class Goal : MonoBehaviour
{
    private bool isClear = false;
    public bool IsClear()
    {
        return isClear;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isClear = true;
        }
    }
}
