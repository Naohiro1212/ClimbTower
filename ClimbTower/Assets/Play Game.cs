using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayGame : MonoBehaviour
{
    public void SwitchScene()
    {
        SceneManager.LoadScene("PlayScene", LoadSceneMode.Single);
    }
}
