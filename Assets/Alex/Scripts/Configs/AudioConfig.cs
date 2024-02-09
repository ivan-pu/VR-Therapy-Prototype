using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Audio Data", menuName = "Custom Data/Audio Data")]
public class AudioConfig : ScriptableObject
{
    [Title("BGM")]
    public AudioClip clip_BGM;
    [Header("Voiceover")]
    [Title("Phase 1")]
    public List<AudioClip> phase1_Voiceovers;
    [Title("Phase 2")]
    public List<AudioClip> phase2_Voiceovers;
    [Title("Phase 3")]
    public List<AudioClip> phase3_Voiceovers;
    [Title("Phase 4")]
    public List<AudioClip> phase4_Voiceovers;
    [Title("Phase 5")]
    public List<AudioClip> phase5_Voiceovers;
}
