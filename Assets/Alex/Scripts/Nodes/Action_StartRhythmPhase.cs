using FlowCanvas.Nodes;
using NodeCanvas.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_StartRhythmPhase : CallableActionNode<RhythmController, RhythmPhase>
{
    public override void Invoke(RhythmController rhythmCtrl, RhythmPhase phase)
    {
        //rhythmController.value.StartPhase(newPhase);
        rhythmCtrl.StartPhase(phase);
    }
}
