using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class GeminiRequestHandler : MonoBehaviour
{
    public InputField promptInputField;  // Reference to the UI InputField
    public Button submitButton;          // Reference to the UI Button

    private string apiKey = "AIzaSyB_BNjL8n0MQW0p2FiZTYYXJYr1dlIjaUU";  // Replace with your API key
    private string apiUrl = "https://generativelanguage.googleapis.com/v1beta/models/gemini-1.5-flash:generateContent?key=";

    void Start()
    {
        // Add listener to the button to trigger the prompt submission
        submitButton.onClick.AddListener(OnSubmit);
    }

    // This method is called when the submit button is clicked
    public void OnSubmit()
    {
        string userInput = promptInputField.text;
        StartCoroutine(SendPromptToGeminiAPI(userInput));
    }

    IEnumerator SendPromptToGeminiAPI(string prompt)
    {
        string url = apiUrl + apiKey;

        // JSON request body using the input from the user
        string jsonRequestBody = @"{
            ""contents"": [
                {
                    ""role"": ""user"",
                    ""parts"": [
                        {
                            ""text"": "" You are Samboot, a patient and friendly assistant who specializes in teaching Python to Grade 12 students. Your goal is to ask multiple-choice questions (MCQs) and adjust the difficulty based on the student's performance.
                            When the student sends the keyword 'question', you will present an MCQ with options seperated by ___ on the SAME LINE along with correct answer at the end.
                            You will only ansk one liner questions.
                            You will only ask questions when the keyword 'question' is sent.
                            DO NOT ask questions related to 'what is the output of a specific Python syntax.' 
                            You will never repeat a question. 
                            DO NOT ask questions related to 'what is the output of a specific Python syntax.'
                            When you ask question, replace double quotes with single quotes EVERY time.
                            example response :
                            Which of these is a valid Python variable name?___123abc ___abc_123___abc-123 ___abc 123___2
                            the first part is the question, part 2,3,4,5 are the options and the part 6 is the correct answer.
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

        // Create and send the POST request
        using (UnityWebRequest request = new UnityWebRequest(url, "POST"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonRequestBody);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            // Handle the response
            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error: " + request.error);
            }
            else
            {
                string responseText = request.downloadHandler.text;
                Debug.Log("Full Response: " + responseText);

                // Log the generated content
                if (responseText.Contains("\"text\""))
                {
                    string[] lines = responseText.Split('\n');
                    foreach (string line in lines)
                    {
                        if (line.Contains("\"text\""))
                        {
                            Debug.Log("Generated Text: " + line.Trim());
                        }
                    }
                }
            }
        }
    }
}
