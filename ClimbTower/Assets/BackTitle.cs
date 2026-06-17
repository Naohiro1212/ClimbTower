using UnityEngine;
using UnityEngine.SceneManagement;

public class BackTitle : MonoBehaviour
{
    public void switchScene()
    {
        SceneManager.LoadScene("TitleScene", LoadSceneMode.Single);
    }
}
