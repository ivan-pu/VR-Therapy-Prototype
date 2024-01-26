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

    [SerializeField] VoiceoverManager voiceoverManager;
    public VoiceoverManager VoiceoverManager { get => voiceoverManager; }

    [SerializeField] FlowManager flowManager;
    public FlowManager FlowManager { get => flowManager;}


}
