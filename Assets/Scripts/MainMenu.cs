using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MainMenu : MonoBehaviour
{
    public void OnStartButtonClick()
    {
        Application.LoadLevel(1);
    }

    public void OnQuitButtonClick()
    {
        Application.Quit();
    }
}