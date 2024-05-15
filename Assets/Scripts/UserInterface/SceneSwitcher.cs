using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneSwitcher : MonoBehaviour
{
    public string scenename;
    public void OnButtonClick()
    {
        Debug.Log("я перехожу!");
        SceneManager.LoadScene(scenename);
        //if(scenename == "quit")
        //{
        //    Application.Quit();
        //}
    }
}
