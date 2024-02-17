using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionManager : MonoBehaviour
{
    [SerializeField] Option optionPrefab;
    [SerializeField] List<Transform> optionPos;

    List<Option> generatedOptions = new List<Option>();

    bool isChoosing = false;
    public bool IsChoosing { get => isChoosing; set => isChoosing = value; }

    public void GenerateOptions(List<OptionInfo> optionInfos)
    {
        generatedOptions.Clear();
        for (int i = 0; i < optionInfos.Count; i++)
        {
            if (i < optionPos.Count)
            {
                Option o = Instantiate(optionPrefab, optionPos[i].position, optionPos[i].rotation, optionPos[i]);
                o.SetOption(optionInfos[i]);
                generatedOptions.Add(o);
            }
        }
        isChoosing = true;
    }
}
