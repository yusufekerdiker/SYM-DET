using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public string nextLevelName;

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(nextLevelName);
    }
}
