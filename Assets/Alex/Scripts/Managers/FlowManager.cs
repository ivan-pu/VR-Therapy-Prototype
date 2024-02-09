using FlowCanvas;
using NodeCanvas.Framework;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum FlowPhase
{
    Phase_1 = 0,
    Phase_2,
    Phase_3,
    Phase_4,
    Phase_5,
}
public class FlowManager : MonoBehaviour
{
    [SerializeField] FlowPhase curPhase;
    [SerializeField] FlowScriptController flow1;
    [SerializeField] FlowScriptController flow2;
    [SerializeField] FlowScriptController flow3;
    [SerializeField] FlowScriptController flow4;
    [SerializeField] FlowScriptController flow5;

    public delegate void ActionDelegate();
    private event ActionDelegate phase1StartEvent;
    private event ActionDelegate phase2StartEvent;
    private event ActionDelegate phase3StartEvent;
    private event ActionDelegate phase4StartEvent;
    private event ActionDelegate phase5StartEvent;

    [Button]
    public void StartPhase(FlowPhase newPhase)
    {
        curPhase = newPhase;
        switch (newPhase)
        {
            case FlowPhase.Phase_1:
                phase1StartEvent?.Invoke();
                flow1.StartBehaviour();
                break;
            case FlowPhase.Phase_2:
                phase2StartEvent?.Invoke();
                flow2.StartBehaviour();
                break;
            case FlowPhase.Phase_3:
                phase3StartEvent?.Invoke();
                flow3.StartBehaviour();
                break;
            case FlowPhase.Phase_4:     
                phase4StartEvent?.Invoke();
                flow4.StartBehaviour();
                break;
            case FlowPhase.Phase_5:
                phase5StartEvent?.Invoke();
                flow5.StartBehaviour();
                break;
        }
    }
}
