using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoEndSceneSwitcher : MonoBehaviour
{
    public VideoPlayer videoPlayer;  // Drag and drop your VideoPlayer here in the Inspector
    public int sceneIndexToLoad;     // Set the build index of the scene you want to load after the video ends

    void Start()
    {
        if (videoPlayer != null)
        {
            // Subscribe to the loopPointReached event, which is triggered when the video ends
            videoPlayer.loopPointReached += OnVideoEnd;
        }
        else
        {
            Debug.LogError("No VideoPlayer assigned to the script!");
        }
    }

    // Method that is called when the video finishes playing
    void OnVideoEnd(VideoPlayer vp)
    {
        Debug.Log("Video has finished. Switching scenes...");
        SceneManager.LoadScene(sceneIndexToLoad);  // Load the scene by index
    }
}
