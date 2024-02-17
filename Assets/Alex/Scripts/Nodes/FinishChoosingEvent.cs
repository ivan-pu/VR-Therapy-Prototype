using FlowCanvas;
using FlowCanvas.Nodes;
using NodeCanvas.Framework;
using Sirenix.OdinInspector.Editor.GettingStarted;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishChoosingEvent : EventNode, IUpdatable
{
    private FlowOutput finished;

    protected override void RegisterPorts()
    {
        finished = AddFlowOutput("Finished");
    }
    void IUpdatable.Update()
    {
        if (GameManager.Instance.OptionManager.IsChoosing == false)
        {
            finished.Call(new Flow());
        }
    }
}
