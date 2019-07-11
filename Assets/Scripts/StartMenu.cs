using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public static bool gameIsStart = false;
    public GameObject startMenuUI;

    public void StartGame()
    {
        Debug.Log("Start");
        SceneManager.LoadScene("RomainSandBox");
    }
}
