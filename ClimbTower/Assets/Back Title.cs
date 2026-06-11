using UnityEngine;
using UnityEngine.SceneManagement;

public class BackTitle : MonoBehaviour
{
    public void SwitchScene()
    {
        SceneManager.LoadScene("TitleScene", LoadSceneMode.Single);
    }
}
