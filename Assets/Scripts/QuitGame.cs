using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGame : MonoBehaviour
{
    public void OnButtonPress()
    {
        Debug.Log(" Application.Quit()");
        Application.Quit();
    }
}
