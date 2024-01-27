using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        if (SceneManager.GetSceneByName(sceneName).isLoaded)
            return;

        SceneManager.LoadScene(sceneName);
    }

    public void UnloadScene(string sceneName)
    {
        if (!SceneManager.GetSceneByName(sceneName).isLoaded)
            return;

        SceneManager.UnloadSceneAsync(sceneName);
    }

    public void LoadSceneAdditively(string sceneName)
    {
        if (SceneManager.GetSceneByName(sceneName).isLoaded)
            return;

        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}