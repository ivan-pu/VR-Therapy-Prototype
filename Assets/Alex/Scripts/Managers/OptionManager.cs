using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionManager : MonoBehaviour
{
    [SerializeField] Option optionPrefab;
    [SerializeField] List<Transform> optionPos;

    List<Option> generatedOptions = new List<Option>();

    public void GenerateOptions(List<string> optionTexts)
    {
        generatedOptions.Clear();
        for (int i = 0; i < optionTexts.Count; i++)
        {
            if (i < optionPos.Count)
            {
                Option o = Instantiate(optionPrefab, optionPos[i].position, optionPos[i].rotation, optionPos[i]);
                o.SetText(optionTexts[i]);
                generatedOptions.Add(o);
            }
        }
    }
}
