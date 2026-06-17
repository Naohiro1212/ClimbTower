using UnityEngine;

// プレイヤーの生死の状態を管理するクラス
public class PlayerStatus : MonoBehaviour
{
    // プレイヤーの生死の状態を管理するフラグ
    [SerializeField] private bool isAlive;

    void Start()
    {
        isAlive = true;
    }

    public void SetAlive(bool isAlive)
    {
        this.isAlive = isAlive;
    }
}
