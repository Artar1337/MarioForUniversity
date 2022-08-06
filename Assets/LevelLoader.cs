using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public void LoadScene()
    {
        GameObject.Find("Canvas").transform.Find("Lives").gameObject.SetActive(true);
        SceneManager.LoadScene(1);
    }

    public void LoadTitle()
    {
        SceneManager.LoadScene(0);
    }

    public void Exit()
    {
        Application.Quit();
    }

}
