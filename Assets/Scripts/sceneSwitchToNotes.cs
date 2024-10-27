using UnityEngine;
using UnityEngine.SceneManagement;  // For scene management
using UnityEngine.UI;  // For working with UI elements like buttons

public class ChangeSceneOnClick : MonoBehaviour
{
    public Button yourButton;  // Drag and drop your button in the Inspector

    void Start()
    {
        // Add listener to the button's onClick event
        if (yourButton != null)
        {
            yourButton.onClick.AddListener(ChangeScene);  // Call ChangeScene when button is clicked
        }
        else
        {
            Debug.LogError("Button not assigned in the Inspector!");
        }
    }

    // Method to change to scene 4
    void ChangeScene()
    {
        SceneManager.LoadScene(4);  // Load scene with index 4
    }
}
