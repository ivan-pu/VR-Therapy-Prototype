using com.zibra.liquid.Manipulators;
using FlowCanvas.Nodes;
using NodeCanvas.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_SetUpRotateChecker : CallableActionNode<bool>
{
    public BBParameter<TankRotateChecker> rotateChecker;

    public override void Invoke(bool canMove)
    {
        if (rotateChecker != null)
        {
            rotateChecker.value.canMove = canMove;
        }
    }
}
