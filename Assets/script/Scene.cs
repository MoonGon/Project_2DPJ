using UnityEngine;
using UnityEngine.SceneManagement;

public class NextSceneButtonByName : MonoBehaviour
{
    [SerializeField] private string sceneName;
    public void LoadSceneByName()
    {
        if (string.IsNullOrWhiteSpace(sceneName))
        {
            Debug.LogError("Scene name is empty. Please set sceneName in Inspector.");
            return;
        }

        SceneManager.LoadScene(sceneName);
    }
}