using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitApp : MonoBehaviour
{
   void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Debug.Log("Player hit ESC and quit the app.");
            Application.Quit();
        }
    }
}
