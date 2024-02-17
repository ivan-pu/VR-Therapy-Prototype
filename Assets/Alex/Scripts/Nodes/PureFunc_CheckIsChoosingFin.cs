using FlowCanvas.Nodes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PureFunc_CheckIsChoosingFin : PureFunctionNode<bool>
{
    public override bool Invoke()
    {
        return (GameManager.Instance.OptionManager.IsChoosing == false);
    }
}
