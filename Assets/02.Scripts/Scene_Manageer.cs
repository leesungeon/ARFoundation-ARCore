using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Manageer : MonoBehaviour
{
    public void OnClickHomeScene()
    {
        SceneManager.LoadScene("StartScene");
    }

    public void OnClickMaskScene()
    {
        SceneManager.LoadScene("FaceScene");
    }

    public void OnClickGPSScene()
    {
        SceneManager.LoadScene("GPSScene");
    }

    public void OnClickImageScene()
    {
        SceneManager.LoadScene("ImageScene");
    }

    public void OnClickExitGame()
    {
        Application.Quit();
    }
}
