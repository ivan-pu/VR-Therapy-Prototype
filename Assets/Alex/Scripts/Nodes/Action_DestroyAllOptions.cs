using FlowCanvas.Nodes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_DestroyAllOptions : CallableActionNode
{
    public override void Invoke()
    {
        GameManager.Instance.OptionManager.DestroyAllOptions();
    }
}
