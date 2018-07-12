using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof(GUITexture))]
public class ForcedReset : MonoBehaviour
{
    private void Update()
    {
        // if we have forced a reset ...
        if (CrossPlatformInputManager.GetButtonDown("ResetObject"))
        {
            //This may be wrong I am not sure where to get the scene name from <JF>                                                           
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
        }
    }
}