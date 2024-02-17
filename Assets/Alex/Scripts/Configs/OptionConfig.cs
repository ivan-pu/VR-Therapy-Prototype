using ParadoxNotion.Design;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class OptionInfo
{
    public string optionText;
    public bool changedVoiceover = false;
    public FlowPhase targetPhase;
    public int targetVoiceoverIndex;
}

[System.Serializable]
[CreateAssetMenu(fileName = "Option Data", menuName = "Custom Data/Option Data")]
public class OptionConfig : ScriptableObject
{
    public List<OptionInfo> options;
}
