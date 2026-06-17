using UnityEngine;

public class CoinMaterial : MonoBehaviour
{
    private void Start()
    {
        //仕様はここで完結
        if (TryGetComponent<Collider2D>(out var col))
        {
            col.isTrigger = true;
        }
    }

    //プレイヤーが触れた瞬間に実行
    private void OnTriggerEnter2D(Collider2D other)
    {
        //"Player"タグで判定
        if (other.CompareTag("Player"))
        {
            // コインのオブジェクト自体をゲーム内から完全に削除する
            Destroy(gameObject);
        }
    }
}
