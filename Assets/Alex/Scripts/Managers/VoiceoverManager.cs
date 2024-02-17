using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VoiceoverManager : MonoBehaviour
{
    [Title("Audio Mixers")]
    public AudioMixer audioMixer;

    [Title("Audio Sources")]
    public AudioSource audioSource_BGM;
    public AudioSource audioSource_Voiceover;

    [Title("Audio Config")]
    public AudioConfig audioConfig;

    public Dictionary<FlowPhase, int> phaseVoiceoverTable = new Dictionary<FlowPhase, int>();

    public void SetPhaseVoiceover(FlowPhase targetPhase, int targetIndex)
    {
        if(phaseVoiceoverTable.ContainsKey(targetPhase))
            phaseVoiceoverTable[targetPhase] = targetIndex;
    }

    private void Awake()
    {
        audioSource_BGM.clip = audioConfig.clip_BGM;
        audioSource_BGM.Play();
        phaseVoiceoverTable.Add(FlowPhase.Phase_1, 0);
        phaseVoiceoverTable.Add(FlowPhase.Phase_2, 0);
        phaseVoiceoverTable.Add(FlowPhase.Phase_3, 0);
        phaseVoiceoverTable.Add(FlowPhase.Phase_4, 0);
        phaseVoiceoverTable.Add(FlowPhase.Phase_5, 0);
    }

    [Button]
    public AudioClip PlayVoiceover(FlowPhase phase, int voiceoverIndex)
    {
        PhaseVoiceover audioclips;
        audioSource_Voiceover.clip = null;
        switch (phase)
        {
            case FlowPhase.Phase_1:
                audioclips = audioConfig.phase1_Voiceovers[phaseVoiceoverTable[FlowPhase.Phase_1]];
                if (audioclips.audios.Count > voiceoverIndex)
                {
                    audioSource_Voiceover.clip = audioclips.audios[voiceoverIndex];
                    audioSource_Voiceover.Play();
                }
                break;
            case FlowPhase.Phase_2:
                audioclips = audioConfig.phase2_Voiceovers[phaseVoiceoverTable[FlowPhase.Phase_2]];
                if (audioclips.audios.Count > voiceoverIndex)
                {
                    audioSource_Voiceover.clip = audioclips.audios[voiceoverIndex];
                    audioSource_Voiceover.Play();
                }
                break;
            case FlowPhase.Phase_3:
                audioclips = audioConfig.phase3_Voiceovers[phaseVoiceoverTable[FlowPhase.Phase_3]];
                if (audioclips.audios.Count > voiceoverIndex)
                {
                    audioSource_Voiceover.clip = audioclips.audios[voiceoverIndex];
                    audioSource_Voiceover.Play();
                }
                break;
            case FlowPhase.Phase_4:
                audioclips = audioConfig.phase4_Voiceovers[phaseVoiceoverTable[FlowPhase.Phase_4]];
                if (audioclips.audios.Count > voiceoverIndex)
                {
                    audioSource_Voiceover.clip = audioclips.audios[voiceoverIndex];
                    audioSource_Voiceover.Play();
                }
                break;
            case FlowPhase.Phase_5:
                audioclips = audioConfig.phase5_Voiceovers[phaseVoiceoverTable[FlowPhase.Phase_5]];
                if (audioclips.audios.Count > voiceoverIndex)
                {
                    audioSource_Voiceover.clip = audioclips.audios[voiceoverIndex];
                    audioSource_Voiceover.Play();
                }
                break;
        }
        return audioSource_Voiceover.clip;
    }
}
