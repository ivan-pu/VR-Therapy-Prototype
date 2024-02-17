using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    [Title("Managers")]
    [SerializeField] VoiceoverManager voiceoverManager;
    public VoiceoverManager VoiceoverManager { get => voiceoverManager; }

    [SerializeField] FlowManager flowManager;
    public FlowManager FlowManager { get => flowManager;}

    [SerializeField] OptionManager optionManager;
    public OptionManager OptionManager { get => optionManager; }

    [Title("References")]
    [SerializeField] ObjectMover waterTankMover;
    public ObjectMover WaterTankMover { get => waterTankMover; }

    [Title("References")]
    [SerializeField] ObjectMover manMover;
    public ObjectMover ManMover { get => manMover; }


}
