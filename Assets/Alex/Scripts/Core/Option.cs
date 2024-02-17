using Oculus.Interaction;
using Oculus.Interaction.HandGrab;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Option : OneGrabTranslateTransformer, ITransformer
{
    public TextMeshProUGUI text;
    public float detectReceiverRadius = 1f;
    public LayerMask receiverLayerMask;

    public delegate void ObjectTransformEvent();
    public event ObjectTransformEvent onObjGrabbed;
    public event ObjectTransformEvent onObjMoved;
    public event ObjectTransformEvent onObjReleased;

    bool changePhaseVoiceover = false;
    FlowPhase targetPhase;
    int targetIndex;

    public void SetOption(OptionInfo optionInfo)
    {
        if(text)
            text.text = optionInfo.optionText;
        changePhaseVoiceover = optionInfo.changedVoiceover;
        targetPhase = optionInfo.targetPhase;
        targetIndex = optionInfo.targetVoiceoverIndex;
    }

    public new void Initialize(IGrabbable grabbable)
    {
        base.Initialize(grabbable);
    }
    public new void BeginTransform()
    {
        base.BeginTransform();
        onObjGrabbed?.Invoke();
    }
    public new void UpdateTransform()
    {
        base.UpdateTransform();
        onObjMoved?.Invoke();
    }
    public new void EndTransform()
    {
        base.EndTransform();
        onObjReleased?.Invoke();
    }

    private void OnEnable()
    {
        onObjGrabbed += OnGrab;
        onObjReleased += OnRelease;
    }

    private void OnDisable()
    {
        onObjGrabbed -= OnGrab;
        onObjReleased -= OnRelease;
    }

    Collider[] detectReceiver = new Collider[1];

    public void OnRelease()
    {
        Debug.Log("Released" + gameObject.name);
        int detectNum = Physics.OverlapSphereNonAlloc(transform.position, detectReceiverRadius, detectReceiver, receiverLayerMask);
        if(detectNum > 0)
        {
            GameManager.Instance.OptionManager.IsChoosing = false;
            if(changePhaseVoiceover)
            {
                GameManager.Instance.VoiceoverManager.SetPhaseVoiceover(targetPhase, targetIndex);
            }
        }
    }
    public void OnGrab()
    {
        Debug.Log("Grab" + gameObject.name);
    }
}
