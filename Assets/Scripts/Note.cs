using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Note : MonoBehaviour
{
    public void ViewNote()
    {
        SceneManager.LoadScene(4);
    }
}
