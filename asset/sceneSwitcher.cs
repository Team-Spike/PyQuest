using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public string sceneNameToLoad;  // The name of the scene you want to switch to

    // Use this if the colliders are NOT triggers
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision Detected with: " + collision.gameObject.name);  // Debugging line to check collision
        if (collision.gameObject.CompareTag("screenSwitcher"))
        {
            Debug.Log("Switching scene to: " + sceneNameToLoad);  // Debugging line to confirm tag match
            SceneManager.LoadScene(sceneNameToLoad);
        }
    }
}
