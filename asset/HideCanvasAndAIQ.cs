using UnityEngine;
using UnityEngine.UI;

public class HideCanvasAndAIQ : MonoBehaviour
{
    public Canvas mainCanvas;  // Make sure this is public for it to appear in the Inspector
    public Button yourButton;  // Reference to the button that triggers the action

    void Start()
    {
        // Add listener to button's click event
        if (yourButton != null)
        {
            yourButton.onClick.AddListener(OnButtonClick);
        }
        else
        {
            Debug.LogError("Button not assigned in the Inspector");
        }
    }

    void OnButtonClick()
    {
        // Hide the canvas
        if (mainCanvas != null)
        {
            mainCanvas.gameObject.SetActive(false);  // Disable the entire canvas
        }
        else
        {
            Debug.LogError("MainCanvas not assigned in the Inspector");
        }

        // Find the object tagged with "Sambot" and disable its renderer
        GameObject sambot = GameObject.FindWithTag("Sambot");
        if (sambot != null)
        {
            Renderer sambotRenderer = sambot.GetComponent<Renderer>();
            if (sambotRenderer != null)
            {
                sambotRenderer.enabled = false;  // Disable the renderer to make Sambot invisible
                Debug.Log("Sambot renderer disabled.");
            }
            else
            {
                Debug.LogError("No Renderer found on the Sambot object.");
            }
        }
        else
        {
            Debug.LogError("No object with the tag 'Sambot' was found.");
        }
    }
}
