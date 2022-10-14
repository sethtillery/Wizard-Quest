using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerText : MonoBehaviour
{
    //character for the text
    [HideInInspector]
    public player character;

    //text to show
    public Text text;

    //updates the text based off the character
    void Update()
    {
        if (character != null)
        {

            text.text = character.textChar;

        }
    }

}
