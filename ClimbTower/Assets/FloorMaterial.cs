using UnityEngine;

public class FloorMaterial : MonoBehaviour
{
    [Header("この床に触れた時に適用")]
    [SerializeField] private PhysicsMaterial2D targetMaterial;

    [Header("氷の場合のみ（通常床なら0）")]
    [SerializeField] private float iceSlideForce = 2f;

    [Header("離れた時に戻す通常マテリアル")]
    [SerializeField] private PhysicsMaterial2D defaultMaterial;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();

            if (playerRb != null)
            {
                //1.物理マテリアルを切り替え
                if (playerRb.sharedMaterial != targetMaterial)
                {
                    playerRb.sharedMaterial = targetMaterial;
                }

                //2.滑る処理
                if (iceSlideForce > 0f)
                {
                    float currentMoveX = playerRb.linearVelocity.x;
                    playerRb.AddForce(new Vector2(currentMoveX * iceSlideForce, 0f), ForceMode2D.Force);
                }
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //この床から離れたら、マテリアルを通常状態に戻す
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (playerRb != null && defaultMaterial != null)
            {
                playerRb.sharedMaterial = defaultMaterial;
            }
        }
    }
}

