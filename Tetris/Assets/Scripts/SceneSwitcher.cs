using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public void StartGame()
    {
        //SceneManager.UnloadSceneAsync("GamePlay");
        SceneManager.LoadScene("GamePlay");
    }

    public void HomeScene()
    {
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }


}