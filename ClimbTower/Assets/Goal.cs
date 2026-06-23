using UnityEngine;

public class Goal : MonoBehaviour
{
    [SerializeField] private bool isClear = false;

    public bool IsClear()
    {
        return isClear;
    }

    // 押し返されずに「すり抜けた瞬間」を検知する場合（Is Triggerがオンのとき）
    void OnTriggerEnter2D(Collider2D collision)
    {
        // ぶつかった相手（collision）のタグが "Player" かどうか
        if (collision.CompareTag("Player"))
        {
            isClear = true;
            Debug.Log("ゴールに触れました！クリアフラグをONにしました。");

            // TODO: ここにクリア演出（UIを出す、シーンを切り替えるなど）を呼び出す処理を書くとバッチリです！
        }
    }
}