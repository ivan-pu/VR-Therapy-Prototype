using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum RhythmPhase
{
    Idle = 0,
    Phase_1,
    Phase_2,
    Phase_3,
}

public class RhythmController : MonoBehaviour
{
    [Title("Pattern")]
    [SerializeField] Transform cursorLeft;
    [SerializeField] Transform cursorRight;
    [SerializeField] float localLeftMaxDist;
    [SerializeField] float localRightMaxDist;
    [Title("Match Making")]
    [SerializeField] Transform handLeft;
    [SerializeField] Transform handRight;
    [SerializeField] float matchDist;
    [Title("Phase 1")]
    [SerializeField] AnimationCurve curve1;
    [SerializeField] float time1;
    [SerializeField] float matchTime1;
    [Title("Phase 2")]
    [SerializeField] AnimationCurve curve2;
    [SerializeField] float time2;
    [SerializeField] float matchTime2;
    [Title("Phase 3")]
    [SerializeField] AnimationCurve curve3;
    [SerializeField] float time3;
    [SerializeField] float matchTime3;

    float timer;
    [SerializeField][ReadOnly] RhythmPhase lastRhythmPhase;
    [SerializeField][ReadOnly]RhythmPhase curRhythmPhase;
    [SerializeField][ReadOnly] RhythmPhase nextRhythmPhase;
    Vector3 localOrigin;
    Vector3 localLeftMax;
    Vector3 localRightMax;

    float matchTimer;
    [SerializeField] bool curPhaseFinished = false;

    private void Awake()
    {
        localOrigin = Vector3.zero;
        localLeftMax = localOrigin + Vector3.left * localLeftMaxDist;
        localRightMax = localOrigin + Vector3.right * localRightMaxDist;
    }

    [Button]
    public void StartPhase(RhythmPhase newPhase)
    {
        if (newPhase == curRhythmPhase)
            return;

        timer = 0;
        matchTimer = 0;
        lastRhythmPhase = curRhythmPhase;
        curRhythmPhase = newPhase;
        cursorLeft.localPosition = localOrigin;
        cursorRight.localPosition = localOrigin;
        switch (curRhythmPhase)
        {
            case RhythmPhase.Idle:
                break;
            case RhythmPhase.Phase_1:
                nextRhythmPhase = RhythmPhase.Phase_2;
                break;
            case RhythmPhase.Phase_2:
                nextRhythmPhase = RhythmPhase.Phase_3;
                break;
            case RhythmPhase.Phase_3:
                nextRhythmPhase = RhythmPhase.Idle;
                break;
        }
    }

    [Button]
    public void StartNextPhase()
    {
        StartPhase(nextRhythmPhase);
        if(nextRhythmPhase != RhythmPhase.Idle)
            curPhaseFinished = false;
    }

    private void Update()
    {
        switch(curRhythmPhase)
        {
            case RhythmPhase.Idle:
                if (!CheckCurPhaseFinish())
                {
                    StartPhase(lastRhythmPhase);
                }
                break;
            case RhythmPhase.Phase_1:
                HandlePhase(time1, curve1, matchTime1);
                break;
            case RhythmPhase.Phase_2:
                HandlePhase(time2, curve2, matchTime2);
                break;
            case RhythmPhase.Phase_3:
                HandlePhase(time3, curve3, matchTime3);
                break;
        }
    }

    void HandlePhase(float totalTime, AnimationCurve curve, float matchTime)
    {
        if (timer < totalTime)
        {
            timer += Time.deltaTime;
            float t01 = timer / totalTime;
            float v01 = curve.Evaluate(t01);
            Vector3 curLeftPos = Vector3.Lerp(localOrigin, localLeftMax, v01);
            Vector3 curRightPos = Vector3.Lerp(localOrigin, localRightMax, v01);
            cursorLeft.localPosition = curLeftPos;
            cursorRight.localPosition = curRightPos;
            if (Vector3.Distance(cursorLeft.position, handLeft.position) <= matchDist)
                matchTimer += Time.deltaTime;
            if(Vector3.Distance(cursorRight.position, handRight.position) <= matchDist)
                matchTimer += Time.deltaTime;
        }
        else
        {
            cursorLeft.localPosition = localOrigin;
            cursorRight.localPosition = localOrigin;
            curPhaseFinished = (matchTimer >= matchTime);
            StartPhase(RhythmPhase.Idle);
        }
    }

    bool CheckCurPhaseFinish()
    {
        return curPhaseFinished;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position + Vector3.left * localLeftMaxDist, transform.position + Vector3.right * localRightMaxDist);
    }
}
