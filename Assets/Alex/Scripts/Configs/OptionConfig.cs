using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Option Data", menuName = "Custom Data/Option Data")]
public class OptionConfig : ScriptableObject
{
    public List<string> options;
}
