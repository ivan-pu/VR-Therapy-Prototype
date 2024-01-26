using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTankMover : MonoBehaviour
{
    [SerializeField] Transform waterTank;

    public Vector3 worldTargetPosition;

    public void StartMove()
    {
        waterTank.DOMove(worldTargetPosition, 1f);
    }

}
