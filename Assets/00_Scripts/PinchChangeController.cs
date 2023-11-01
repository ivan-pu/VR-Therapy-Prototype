using com.zibra.liquid.Manipulators;
using Oculus.Interaction.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

public class PinchChangeController : MonoBehaviour
{
    [SerializeField] public bool canPinch = false;
    [SerializeField] private Hand leftHand, rightHand;
    [SerializeField] private bool isPinchingBothHands = false;

    [SerializeField] Transform leftHandPos;
    [SerializeField] Transform rightHandPos;

    private float currentDistance;
    private float startDistance;

    [SerializeField] AudioSource rainSound;
    [SerializeField] float soundMinValue, soundMaxValue;
    private float soundStartLevel;
    [SerializeField] private float soundCurrentLevel;
    [SerializeField] private float soundSensitivity = 0.5f;

    [SerializeField] ZibraLiquidEmitter generalEmitter;
    [SerializeField] float geMinValue, geMaxValue;
    private float rainStartLevel;
    [SerializeField] private float rainCurrentLevel;
    [SerializeField] private float rainSensitivity = 2e-05f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!canPinch) return;
        else
        {
            if (leftHand.GetIndexFingerIsPinching() && rightHand.GetIndexFingerIsPinching() && !isPinchingBothHands)
            {
                isPinchingBothHands = true;
                startDistance = Vector3.Distance(leftHandPos.position, rightHandPos.position);
                soundStartLevel = rainSound.volume;
                rainStartLevel = generalEmitter.VolumePerSimTime;
            }
            else if (!(leftHand.GetIndexFingerIsPinching() && rightHand.GetIndexFingerIsPinching()))
            {
                isPinchingBothHands = false;
            }

            if (isPinchingBothHands)
            {
                GetDistanceBetweenHands();
                UpdateSoundLevel();
                UpdateRainLevel();
            }
        }
    }

    void GetDistanceBetweenHands()
    {
        currentDistance = Vector3.Distance(leftHandPos.position, rightHandPos.position);
    }

    void UpdateSoundLevel()
    {
        soundCurrentLevel = Mathf.Clamp(soundStartLevel - (currentDistance - startDistance) * soundSensitivity, soundMinValue, soundMaxValue);
        if (rainSound) rainSound.volume = soundCurrentLevel;
    }

    void UpdateRainLevel()
    {
        rainCurrentLevel = Mathf.Clamp(rainStartLevel - (currentDistance - startDistance) * rainSensitivity, geMinValue, geMaxValue);
        generalEmitter.VolumePerSimTime = rainCurrentLevel;
    }
}
