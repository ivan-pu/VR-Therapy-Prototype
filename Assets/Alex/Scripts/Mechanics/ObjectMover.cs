using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    [SerializeField] Transform targetObj;

    [Button]
    public void StartMove(Vector3 worldTargetPos, float t)
    {
        targetObj.DOPause();
        targetObj.DOMove(worldTargetPos, t);
    }
    
}
