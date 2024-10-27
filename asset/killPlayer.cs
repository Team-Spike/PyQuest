using UnityEngine;
using UnityEngine.SceneManagement;  // For managing scene loading

public class ReloadSceneOnCollision : MonoBehaviour
{
    public string targetTag = "Obstacle";  // Set the tag of the object you want to trigger the scene reload

    void OnCollisionEnter2D(Collision2D collision)  // For 2D collisions
    {
        // Check if the object we collided with has the correct tag
        if (collision.gameObject.CompareTag(targetTag))
        {
            // Reload the current scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    // Alternatively, use this method if you're using a trigger collider
    void OnTriggerEnter2D(Collider2D collision)  // For trigger collisions
    {
        if (collision.gameObject.CompareTag(targetTag))
        {
            // Reload the current scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
