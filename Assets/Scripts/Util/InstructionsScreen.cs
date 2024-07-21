using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionsScreen : MonoBehaviour
{
    [SerializeField]
    private GameObject[] instructionScreens;

    private int currentScreen = 0;

    public void NextScreen() 
    {
        if ( currentScreen <= instructionScreens.Length - 2 ) 
        {
            instructionScreens[currentScreen++].SetActive(false);
            instructionScreens[currentScreen].SetActive(true);
        }
    }

    public void PrevScreen() 
    {
        if ( currentScreen >= 1 )
        {
            instructionScreens[currentScreen--].SetActive(false);
            instructionScreens[currentScreen].SetActive(true);
        }
    }
    
}
