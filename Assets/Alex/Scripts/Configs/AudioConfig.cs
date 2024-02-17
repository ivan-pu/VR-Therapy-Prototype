using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PhaseVoiceover
{
    public List<AudioClip> audios;
}

[System.Serializable]
[CreateAssetMenu(fileName = "Audio Data", menuName = "Custom Data/Audio Data")]
public class AudioConfig : ScriptableObject
{
    [Title("BGM")]
    public AudioClip clip_BGM;
    [Header("Voiceover")]
    [Title("Phase 1")]
    [SerializeField]
    public List<PhaseVoiceover> phase1_Voiceovers ;
    [Title("Phase 2")]
    [SerializeField]
    public List<PhaseVoiceover> phase2_Voiceovers;
    [Title("Phase 3")]
    [SerializeField]
    public List<PhaseVoiceover> phase3_Voiceovers;
    [Title("Phase 4")]
    [SerializeField]
    public List<PhaseVoiceover> phase4_Voiceovers;
    [Title("Phase 5")]
    [SerializeField]
    public List<PhaseVoiceover> phase5_Voiceovers;

}
