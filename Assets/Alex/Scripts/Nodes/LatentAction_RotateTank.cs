using FlowCanvas.Nodes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LatentAction_RotateTank : LatentActionNode<Transform, Transform, float>
{
    public float timeLeft { get; private set; }
    public float normalized { get; private set; }
    Quaternion originalRot;
    Vector3 originalPos;
    public override IEnumerator Invoke(Transform obj, Transform worldSpaceTarget, float time)
    {
        if (obj != null && worldSpaceTarget != null)
        {
            timeLeft = time;
            originalRot = obj.rotation;
            while (timeLeft > 0)
            {
                timeLeft = time - parentNode.elapsedTime;
                timeLeft = Mathf.Max(timeLeft, 0);
                normalized = timeLeft / time;
                obj.rotation = Quaternion.Lerp(originalRot, worldSpaceTarget.rotation, normalized);
                obj.position = Vector3.Lerp(originalPos, worldSpaceTarget.position, normalized);
                yield return null;
            }
        }
    }
}
