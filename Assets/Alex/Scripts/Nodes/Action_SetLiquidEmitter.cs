using FlowCanvas.Nodes;
using NodeCanvas.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.zibra.liquid.Manipulators;

public class Action_SetLiquidEmitter : CallableActionNode<float, Vector3>
{
    public BBParameter<ZibraLiquidEmitter> emitter;

    public override void Invoke(float volumnPerSimTime, Vector3 initialVelocity)
    {
        if (emitter != null)
        {
            emitter.value.VolumePerSimTime = volumnPerSimTime;
            emitter.value.InitialVelocity = initialVelocity;
        }
    }
}
