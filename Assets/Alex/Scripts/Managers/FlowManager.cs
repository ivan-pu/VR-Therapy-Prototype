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
    Idle,
}
public class FlowManager : MonoBehaviour
{
    [SerializeField] FlowPhase curPhase;
    public FlowPhase CurPhase { get => curPhase; }
    [SerializeField] FlowScriptController flow1;
    [SerializeField] FlowScriptController flow2;
    [SerializeField] FlowScriptController flow3;
    [SerializeField] FlowScriptController flow4;
    [SerializeField] FlowScriptController flow5;

    public delegate void PhaseStartDelegate();
    private event PhaseStartDelegate phase1StartEvent;
    private event PhaseStartDelegate phase2StartEvent;
    private event PhaseStartDelegate phase3StartEvent;
    private event PhaseStartDelegate phase4StartEvent;
    private event PhaseStartDelegate phase5StartEvent;

    [Button]
    public void StartPhase(FlowPhase newPhase)
    {
        curPhase = newPhase;
        switch (newPhase)
        {
            case FlowPhase.Idle:
                break;
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

    private void Update()
    {
        switch(curPhase)
        {
            case FlowPhase.Idle:
                break;
            case FlowPhase.Phase_1:
                if(!flow1.isRunning)
                    StartPhase(FlowPhase.Phase_2);
                break;
            case FlowPhase.Phase_2:
                if (!flow2.isRunning)
                    StartPhase(FlowPhase.Phase_3);
                break;
            case FlowPhase.Phase_3:
                if (!flow3.isRunning)
                    StartPhase(FlowPhase.Phase_4);
                break;
            case FlowPhase.Phase_4:
                if (!flow4.isRunning)
                    StartPhase(FlowPhase.Phase_5);
                break;
            case FlowPhase.Phase_5:
                if (!flow5.isRunning)
                    StartPhase(FlowPhase.Idle);
                break;
        }
    }
}
