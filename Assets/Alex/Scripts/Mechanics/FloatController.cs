using com.zibra.liquid.Manipulators;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatController : MonoBehaviour
{
    [SerializeField] ZibraLiquidCollider liquidCollider;
    [SerializeField] Collider collider;
    [SerializeField] Rigidbody rgbd;

    private void Awake()
    {
        liquidCollider.enabled = false;
        collider.enabled = false;
        rgbd.useGravity = false;
        rgbd.isKinematic = true;
    }

    public void SetCanFloat(bool canFloat)
    {
        if(canFloat)
        {
            liquidCollider.enabled = true;
            collider.enabled = true;
            rgbd.useGravity = true;
            rgbd.isKinematic = false;
        }
        else
        {
            liquidCollider.enabled = false;
            collider.enabled = false;
            rgbd.useGravity = false;
            rgbd.isKinematic = true;
        }
    }
}
