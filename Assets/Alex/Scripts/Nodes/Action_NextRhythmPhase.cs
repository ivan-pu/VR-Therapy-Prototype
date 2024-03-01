using FlowCanvas.Nodes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_NextRhythmPhase : CallableActionNode<RhythmController>
{
    public override void Invoke(RhythmController rhythmCtrl)
    {
        rhythmCtrl.StartNextPhase();
    }
}
