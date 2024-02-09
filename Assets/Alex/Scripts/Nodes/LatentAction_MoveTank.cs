using FlowCanvas.Nodes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LatentAction_MoveTank : LatentActionNode<Transform, float>
{
    public float timeLeft { get; private set; }
    public float normalized { get; private set; }
    public override IEnumerator Invoke(Transform worldSpaceTarget, float time)
    {
        if (worldSpaceTarget != null)
        {
            GameManager.Instance.WaterTankMover.StartMove(worldSpaceTarget.position, time);
            timeLeft = time;
            while (timeLeft > 0)
            {
                timeLeft = time - parentNode.elapsedTime;
                timeLeft = Mathf.Max(timeLeft, 0);
                normalized = timeLeft / time;
                yield return null;
            }
        }
    }
}
