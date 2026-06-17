using UnityEngine;

public class SpikeMaterial : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("プレイヤーが接触した、動きを停止");

            Rigidbody2D player = other.GetComponent<Rigidbody2D>();
            if (player != null)
            {
                //1.突き刺さった瞬間の速度を完全に静止
                player.linearVelocity = Vector2.zero;

                //２.プレイヤーを固定
                Vector2 pushDirection = new Vector2(0f, 1f).normalized;
                player.constraints = RigidbodyConstraints2D.FreezeAll;
            }
        }
    }
}

