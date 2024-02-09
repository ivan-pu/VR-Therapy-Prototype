using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Option : MonoBehaviour
{
    public TextMeshProUGUI text;

    public void SetText(string optionText)
    {
        if(text)
            text.text = optionText;
    }
}
