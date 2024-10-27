using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using System;

public class PlayerCollision2D : MonoBehaviour
{
    public Canvas mainCanvas;  // Make sure this is public for it to appear in the Inspector
    public Button yourButton;  // Reference to the button that triggers the action
    
    // References to the UI Texts for displaying results
    public Text displayText1;
    public Text displayText2;
    public Text displayText3;
    public Text displayText4;
    public Text displayText5;
    

    private bool hasCollidedWithAIQ = false; // Flag to track if player has collided with AIQ


    private string apiKey = "AIzaSyB_BNjL8n0MQW0p2FiZTYYXJYr1dlIjaUU";  // Replace with your API key
    private string apiUrl = "https://generativelanguage.googleapis.com/v1beta/models/gemini-1.5-flash:generateContent?key=";

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
        Debug.Log("Button Clicked, hiding canvas and enabling player movement.");

        // Hide the canvas
        if (mainCanvas != null)
        {
            mainCanvas.gameObject.SetActive(false);  // Disable the entire canvas
            Debug.Log("Canvas hidden.");
        }
        else
        {
            Debug.LogError("MainCanvas not assigned in the Inspector.");
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

        // Enable player movement by enabling the movement script
        GameObject player = GameObject.FindWithTag("Player"); // Assuming the player has the tag "Player"
        if (player != null)
        {
            // Enable the movement script
            PlayerMovement playerMovement = player.GetComponent<PlayerMovement>(); // Assuming "PlayerMovement" is the script controlling movement
            if (playerMovement != null)
            {
                playerMovement.enabled = true; // Enable the movement script to allow the player to move again
                Debug.Log("Player movement enabled.");
            }
            else
            {
                Debug.LogError("No PlayerMovement script found on the player.");
            }

            // Optionally, you can set the Rigidbody2D velocity if needed for movement logic
            Rigidbody2D rb2D = player.GetComponent<Rigidbody2D>();
            if (rb2D != null)
            {
                // Reset velocity to zero to allow fresh movement
                rb2D.velocity = Vector2.zero;
                Debug.Log("Player velocity reset to zero.");
            }
            else
            {
                Debug.LogError("No Rigidbody2D component found on the player.");
            }
        }
        else
        {
            Debug.LogError("No object with the tag 'Player' was found.");
        }
    }


    // This method is called when the player enters a trigger collider
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("AIQ"))
        {
            Debug.Log("Player collided with AIQ object!");
            RunSpecificScript();
        }
    }

    void RunSpecificScript()
    {
        SetOptionsImagesOpaque();
        // set the transparency of question board to 1
        GameObject imageObject = GameObject.Find("QuestionBoard");  // Replace "ImageObjectName" with your UI Image's name

        if (imageObject != null)
        {
            Image uiImage = imageObject.GetComponent<Image>();
            if (uiImage != null)
            {
                Color newColor = uiImage.color;
                newColor.a = 1f;  // Set alpha to 1 to make it fully opaque
                uiImage.color = newColor;
            }
        }

        GameObject sambot = GameObject.FindWithTag("Sambot");

        if (sambot != null && hasCollidedWithAIQ == false)
        {
            Renderer sambotRenderer = sambot.GetComponent<Renderer>();
            if (sambotRenderer != null)
            {
                sambotRenderer.enabled = true;
                Debug.Log("Sambot is now visible.");
            }

            SpriteRenderer sambotSpriteRenderer = sambot.GetComponent<SpriteRenderer>();
            if (sambotSpriteRenderer != null)
            {
                sambotSpriteRenderer.enabled = true;
                Debug.Log("Sambot sprite is now visible.");
            }
        }
        else
        {
            Debug.LogError("No object with the tag 'Sambot' was found.");
        }

        // Stop player movement by disabling the movement script and setting velocity to zero
        GameObject player = GameObject.FindWithTag("Player"); // Assuming the player has the tag "Player"
        if (player != null && hasCollidedWithAIQ == false)
        {
            // Disable the movement script
            PlayerMovement playerMovement = player.GetComponent<PlayerMovement>(); // Assuming "PlayerMovement" is the script controlling movement
            if (playerMovement != null)
            {
                hasCollidedWithAIQ = true; // make has collided true to ensure that it does not run twice
                playerMovement.enabled = false; // Disable the movement script to stop the player from moving
                Debug.Log("Player movement disabled.");
            }
            else
            {
                Debug.LogError("No PlayerMovement script found on the player.");
            }

            // Set Rigidbody2D velocity to zero
            Rigidbody2D rb2D = player.GetComponent<Rigidbody2D>();
            if (rb2D != null)
            {
                rb2D.velocity = Vector2.zero; // Set velocity to (0, 0) to stop movement
                Debug.Log("Player velocity set to zero.");
            }
            else
            {
                Debug.LogError("No Rigidbody2D component found on the player.");
            }
        }
        else
        {
            Debug.LogError("No object with the tag 'Player' was found.");
        }

        StartCoroutine(SendPromptToGeminiAPI("question"));
    }

    IEnumerator SendPromptToGeminiAPI(string prompt)
    {
        string url = apiUrl + apiKey;

        string jsonRequestBody = @"{
            ""contents"": [
                {
                    ""role"": ""user"",
                    ""parts"": [
                        {
                            ""text"": "" You are Samboot, a patient and friendly assistant who specializes in teaching Python to Grade 12 students. Your goal is to ask multiple-choice questions (MCQs) and adjust the difficulty based on the student's performance.
                            When the student sends the keyword 'question', you will present an MCQ with options seperated by ___ on the SAME LINE.
                            You will always have correct answer as the first option.
                            You will only ansk one liner questions.
                            You will only ask questions when the keyword 'question' is sent.
                            DO NOT ask questions related to 'what is the output of a specific Python syntax.'
                            You will never repeat a question.
                            DO NOT ask questions related to 'what is the output of a specific Python syntax.'
                            When you ask question, replace double quotes with single quotes EVERY time.
                            example response :
                            Which of these is a valid Python variable name?___123abc ___abc_123___abc-123 ___abc 123___1
                            the first part is the question, part 2,3,4,5 are the options and part 6 is the correct answer.
                            ""
                        }
                    ]
                },
                
                {
                    ""role"": ""model"",
                    ""parts"": [
                        {
                            ""text"": ""Okay""
                        }
                    ]
                },

                {
                    ""role"": ""user"",
                    ""parts"": [
                        {
                            ""text"": ""question""
                        }
                    ]
                },

            ]
        }";

        using (UnityWebRequest request = new UnityWebRequest(url, "POST"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonRequestBody);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error: " + request.error);
            }
            else
            {
                string responseText = request.downloadHandler.text;
                Debug.Log("Full Response: " + responseText);

                if (responseText.Contains("\"text\""))
                {
                    string[] lines = responseText.Split('\n');
                    foreach (string line in lines)
                    {
                        if (line.Contains("\"text\""))
                        {
                            string currentLine = line;
                            currentLine = currentLine.Replace("\"text\": \"", "").Replace("\n", "").Trim();
                            string[] parts = currentLine.Split(new string[] { "___" }, StringSplitOptions.None);

                            // Show the first 5 elements of the string array
                            DisplayFirstFiveParts(parts);
                        }
                    }
                }
            }
        }
    }

    void DisplayFirstFiveParts(string[] parts)
    {
        // Make sure each part is displayed in a separate Text element
        if (parts.Length > 0 && displayText1 != null) displayText1.text = parts[0].Replace("\n", "").Replace("\"", "").Trim();
        if (parts.Length > 1 && displayText2 != null) displayText2.text = parts[1].Replace("\n", "").Replace("\"", "").Trim();
        if (parts.Length > 2 && displayText3 != null) displayText3.text = parts[2].Replace("\n", "").Replace("\"", "").Trim();
        if (parts.Length > 3 && displayText4 != null) displayText4.text = parts[3].Replace("\n", "").Replace("\"", "").Trim();
        if (parts.Length > 4 && displayText5 != null) displayText5.text = parts[4].Replace("\n", "").Replace("\"", "").Trim();
    }

    void SetOptionsImagesOpaque()
    {
        // Find all game objects with the tag "Options"
        GameObject[] optionImages = GameObject.FindGameObjectsWithTag("options");

        foreach (GameObject imageObject in optionImages)
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
                Debug.LogWarning($"No Image component found on {imageObject.name}");
            }
        }
    }
}
