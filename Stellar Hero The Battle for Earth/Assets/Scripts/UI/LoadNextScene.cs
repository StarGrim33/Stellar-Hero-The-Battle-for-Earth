using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextScene : MonoBehaviour
{
    public void LoadScene()
    {
        var currentScene = SceneManager.GetActiveScene();
        int nextSceneNndex = currentScene.buildIndex + 1;
        SceneManager.LoadScene(nextSceneNndex);
    }
}
