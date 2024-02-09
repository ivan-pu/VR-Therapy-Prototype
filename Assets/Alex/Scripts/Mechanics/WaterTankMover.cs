using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTankMover : MonoBehaviour
{
    [SerializeField] Transform waterTank;

    public void StartMove(Vector3 worldTargetPos, float t)
    {
        waterTank.DOPause();
        waterTank.DOMove(worldTargetPos, t);
    }
    
}
