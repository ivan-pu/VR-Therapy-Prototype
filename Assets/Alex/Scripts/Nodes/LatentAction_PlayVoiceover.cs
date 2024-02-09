using FlowCanvas.Nodes;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LatentAction_PlayVoiceover : LatentActionNode<FlowPhase, int>
{
    public float timeLeft { get; private set; }
    public float normalized { get; private set; }
    public override IEnumerator Invoke(FlowPhase phase, int index)
    {
        AudioClip clip = GameManager.Instance.VoiceoverManager.PlayVoiceover(phase, index);
        if (clip != null)
            timeLeft = clip.length;
        else
            timeLeft = 0;

        float time = timeLeft;

        while (timeLeft > 0)
        {
            timeLeft = time - parentNode.elapsedTime;
            timeLeft = Mathf.Max(timeLeft, 0);
            normalized = timeLeft / time;
            yield return null;
        }
    }
}
