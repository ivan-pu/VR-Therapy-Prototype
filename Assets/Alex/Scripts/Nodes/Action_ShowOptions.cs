using FlowCanvas.Nodes;
using NodeCanvas.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_ShowOptions : CallableActionNode
{
    public BBParameter<OptionConfig> config;

    public override void Invoke()
    {
        if(config != null)
            GameManager.Instance.OptionManager.GenerateOptions(config.value.options);
    }
}
