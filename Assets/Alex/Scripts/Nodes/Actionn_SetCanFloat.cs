using FlowCanvas.Nodes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actionn_SetCanFloat : CallableActionNode<FloatController, bool>
{
    public override void Invoke(FloatController fCtrl, bool canFloat)
    {
        fCtrl.SetCanFloat(canFloat);
    }
}
