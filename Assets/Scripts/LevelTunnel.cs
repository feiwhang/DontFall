using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTunnel : MonoBehaviour
{
    [SerializeField] private float levelLoadDelay = 0.25f;

    void OnTriggerEnter2D(Collider2D col)
    {
        StartCoroutine(LoadNextLevel());
    }

    private IEnumerator LoadNextLevel()
    {
        yield return new WaitForSeconds(levelLoadDelay);

        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        var nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
}
