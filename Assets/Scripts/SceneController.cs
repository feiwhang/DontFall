using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void LoadScene(string scneneName)
    {
        SceneManager.LoadScene(scneneName);
        Debug.Log($"Scene loaded: {scneneName}");
    }
}
