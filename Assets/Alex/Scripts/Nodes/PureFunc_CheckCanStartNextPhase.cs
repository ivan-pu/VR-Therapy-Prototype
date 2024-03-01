using FlowCanvas.Nodes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PureFunc_CheckCanStartNextPhase : PureFunctionNode<bool, RhythmController>
{
    public override bool Invoke(RhythmController rhythmCtrl)
    {
        return rhythmCtrl.CanStartNextPhase();
    }
}
