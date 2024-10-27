using UnityEngine;
using UnityEngine.SceneManagement;  // For managing scene loading
using UnityEngine.UI;  // For UI components like buttons

public class ReloadSceneOnButtonClick : MonoBehaviour
{
    public Button reloadButton;  // Assign your button in the Inspector
    public AudioSource reloadSound;  // Assign the AudioSource with the reload sound in the Inspector
    public float delayBeforeReload = 1f;  // Time to wait before reloading the scene

    void Start()
    {
        // Add a listener to the button click event
        if (reloadButton != null)
        {
            reloadButton.onClick.AddListener(OnReloadButtonClick);  // Call OnReloadButtonClick when the button is clicked
        }
        else
        {
            Debug.LogError("Reload Button not assigned in the Inspector!");
        }
    }

    // This method is called when the reload button is clicked
    void OnReloadButtonClick()
    {
        if (reloadSound != null)
        {
            // Play the reload sound
            reloadSound.Play();

            // Wait for the sound to finish before reloading the scene
            Invoke("ReloadScene", reloadSound.clip.length);  // Delay scene reload based on the sound length
        }
        else
        {
            Debug.LogWarning("Reload sound not assigned. Reloading immediately.");
            ReloadScene();  // If there's no sound, reload the scene immediately
        }
    }

    // Method to reload the scene
    void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
