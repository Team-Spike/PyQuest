using UnityEngine;
using UnityEngine.UI;  // Required for working with UI components

public class PlayerCollision : MonoBehaviour
{
    public Button targetButton;
    public Button secondButton;  // New reference to another button
    public GameObject gameOverUI;  // Reference to Game Over UI GameObject
    private Image gameOverUIImage; // To store the Image component of the UI

    private void Start()
    {
        if (gameOverUI != null)
        {
            gameOverUIImage = gameOverUI.GetComponent<Image>();
        }

        if (secondButton == null)
        {
            Debug.LogError("Second Button is not assigned in the Inspector!");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            ChangeButtonTransparency(targetButton);  // Change target button transparency
            ChangeButtonTransparency(secondButton);  // Change second button transparency

            // Existing code for handling the image and Game Over UI
            GameObject imageObject = GameObject.Find("Image");  // Replace with actual object name
            if (imageObject != null)
            {
                Image uiImage = imageObject.GetComponent<Image>();
                if (uiImage != null)
                {
                    Color newColor = uiImage.color;
                    newColor.a = 1f;  // Set alpha to 1 to make it fully opaque
                    uiImage.color = newColor;
                }
                else
                {
                    Debug.LogError("Image component not found on the UI object!");
                }
            }
            else
            {
                Debug.LogError("UI Image object not found! Ensure the object name is correct.");
            }

            GameOver(); // Trigger Game Over logic
        }
    }

    void GameOver()
    {
        Time.timeScale = 0f;
        Debug.Log("Game Over!");

        if (gameOverUIImage != null)
        {
            Color tempColor = gameOverUIImage.color;
            tempColor.a = 1f;  // Set alpha to 1 for full opacity
            gameOverUIImage.color = tempColor;
        }

        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);  // Show the Game Over UI
        }
        else
        {
            Debug.LogError("Game Over UI object is not assigned in the Inspector!");
        }
    }

    void ChangeButtonTransparency(Button button)
    {
        if (button == null)
        {
            Debug.LogError("Target Button is not assigned in the Inspector!");
            return;
        }

        Image buttonImage = button.GetComponent<Image>();
        if (buttonImage != null)
        {
            Color buttonColor = buttonImage.color;
            buttonColor.a = 1;  // Set transparency to fully opaque
            buttonImage.color = buttonColor;  // Apply the new color
        }
        else
        {
            Debug.LogError("Button Image component not found!");
        }

        Text buttonText = button.GetComponentInChildren<Text>();
        if (buttonText != null)
        {
            Color textColor = buttonText.color;
            textColor.a = 1;  // Set transparency to fully opaque
            buttonText.color = textColor;  // Apply the new color
        }
        else
        {
            Debug.LogError("Button Text component not found!");
        }
    }
}
