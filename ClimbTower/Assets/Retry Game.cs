using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryGame : MonoBehaviour
{
    public void SwitchScene()
    {
        SceneManager.LoadScene("PlayScene", LoadSceneMode.Single);
    }
}
