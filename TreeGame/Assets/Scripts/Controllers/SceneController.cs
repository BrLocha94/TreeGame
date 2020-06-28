using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void LoadOpen()
    {
        SceneManager.LoadScene("Open");
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void LoadResuls()
    {
        SceneManager.LoadScene("Results");
    }
}
