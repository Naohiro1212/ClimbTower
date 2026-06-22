using UnityEngine;

public class PlayerDirectionFlip : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private float deadZone = 0.01f; // 微小入力のガタつき防止

    void Reset()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
    }

    public void UpdateFacing(float moveX)
    {
        if (moveX > deadZone)
            sr.flipX = false;   // 右向き
        else if (moveX < -deadZone)
            sr.flipX = true;    // 左向き
    }
}