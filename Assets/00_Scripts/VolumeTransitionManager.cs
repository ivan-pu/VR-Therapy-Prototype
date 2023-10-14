using Oculus.Interaction.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeTransitionManager : MonoBehaviour
{
    [SerializeField] private Hand leftHand, rightHand;
    [SerializeField] private bool isPinchingBothHands = false;

    [SerializeField] Transform leftHandPos;
    [SerializeField] Transform rightHandPos;

    [SerializeField] private float currentDistance;
    [SerializeField] private float startDistance;

    [SerializeField] AudioSource rainSound;
    [SerializeField] private float startLevel;
    [SerializeField] private float soundLevel;

    [SerializeField] private float sensitivity = 2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (leftHand.GetIndexFingerIsPinching() && rightHand.GetIndexFingerIsPinching() && !isPinchingBothHands)
        {
            isPinchingBothHands = true;
            startDistance = Vector3.Distance(leftHandPos.position, rightHandPos.position);
            startLevel = rainSound.volume;
        } else if (!(leftHand.GetIndexFingerIsPinching() && rightHand.GetIndexFingerIsPinching()))
        {
            isPinchingBothHands = false;
        }

        if (isPinchingBothHands)
        {
            GetDistanceBetweenHands();

            UpdateSoundLevel();
        }
    }

    void GetDistanceBetweenHands()
    {
        currentDistance = Vector3.Distance(leftHandPos.position, rightHandPos.position);
    }

    void UpdateSoundLevel()
    {
        soundLevel = Mathf.Clamp01(startLevel + (currentDistance - startDistance) / sensitivity);
        if (rainSound) rainSound.volume = soundLevel;
    }
}
