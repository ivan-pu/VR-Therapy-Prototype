using Oculus.Interaction.HandGrab;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankRotateChecker : MonoBehaviour
{
    [SerializeField] Transform tankTransform;
    public Vector3 targetTankUp = Vector3.forward;
    [SerializeField] Transform tankCubeTransform;
    public float tankCubeYOffset = 1f;
    [SerializeField] HandGrabInteractable handGrabInteractable;

    bool isRotateFinish = false;
    public bool IsRotateFinish { get => isRotateFinish; }

    public bool canMove = false;
    public bool nextPhase = false;

    Vector3 tankCubeOriginalLocalPos;
    private void Start()
    {
        tankCubeOriginalLocalPos = tankCubeTransform.localPosition;
        handGrabInteractable.enabled = false;
    }

    private void Update()
    {
        if (!canMove)
            return;

        float v01 = Mathf.Abs(tankTransform.eulerAngles.x - 0) / 90f;
        tankCubeTransform.localPosition = tankCubeYOffset * v01 * Vector3.up + tankCubeOriginalLocalPos;

        if (v01 >= 0.89f)
        {
            nextPhase = true;
            handGrabInteractable.enabled = false;
        }

        if(nextPhase && GameManager.Instance.FlowManager.IsFlow4Finish())
        {
            GameManager.Instance.FlowManager.StartPhase(FlowPhase.Phase_5);
        }
    }
}
