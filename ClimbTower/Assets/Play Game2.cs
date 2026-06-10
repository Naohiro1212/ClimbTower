using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayGame2 : MonoBehaviour
{
    public void SwitchScene()
    {
        SceneManager.LoadScene("PlayScene2", LoadSceneMode.Single);
    }
}