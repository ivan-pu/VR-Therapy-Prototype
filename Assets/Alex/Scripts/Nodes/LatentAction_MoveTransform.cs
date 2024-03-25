using FlowCanvas.Nodes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LatentAction_MoveTransform : LatentActionNode<Transform, Transform, float>
{
    public float timeLeft { get; private set; }
    public float normalized { get; private set; }
    Vector3 originalPos;
    Quaternion originalRot;
    public override IEnumerator Invoke(Transform obj, Transform worldSpaceTarget, float time)
    {
        if (obj != null && worldSpaceTarget != null)
        {
            timeLeft = time;
            originalPos = obj.position;
            originalRot = obj.rotation;
            while (timeLeft > 0)
            {
                timeLeft = time - parentNode.elapsedTime;
                timeLeft = Mathf.Max(timeLeft, 0);
                normalized = timeLeft / time;
                obj.position = Vector3.Lerp(originalPos, worldSpaceTarget.position, normalized);
                obj.rotation = Quaternion.Lerp(originalRot, worldSpaceTarget.rotation, normalized);
                yield return null;
            }
        }
    }
}
