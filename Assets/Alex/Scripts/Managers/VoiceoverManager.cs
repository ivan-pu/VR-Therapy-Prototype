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

    private void Awake()
    {
        audioSource_BGM.clip = audioConfig.clip_BGM;
        audioSource_BGM.Play();
    }

    [Button]
    public AudioClip PlayVoiceover(FlowPhase phase, int index)
    {
        List<AudioClip> audioclips;
        audioSource_Voiceover.clip = null;
        switch (phase)
        {
            case FlowPhase.Phase_1:
                audioclips = audioConfig.phase1_Voiceovers;
                if (audioclips.Count > index)
                {
                    audioSource_Voiceover.clip = audioclips[index];
                    audioSource_Voiceover.Play();
                }
                break;
            case FlowPhase.Phase_2:
                audioclips = audioConfig.phase2_Voiceovers;
                if (audioclips.Count > index)
                {
                    audioSource_Voiceover.clip = audioclips[index];
                    audioSource_Voiceover.Play();
                }
                break;
            case FlowPhase.Phase_3:
                audioclips = audioConfig.phase3_Voiceovers;
                if (audioclips.Count > index)
                {
                    audioSource_Voiceover.clip = audioclips[index];
                    audioSource_Voiceover.Play();
                }
                break;
            case FlowPhase.Phase_4:
                audioclips = audioConfig.phase4_Voiceovers;
                if (audioclips.Count > index)
                {
                    audioSource_Voiceover.clip = audioclips[index];
                    audioSource_Voiceover.Play();
                }
                break;
            case FlowPhase.Phase_5:
                audioclips = audioConfig.phase5_Voiceovers;
                if (audioclips.Count > index)
                {
                    audioSource_Voiceover.clip = audioclips[index];
                    audioSource_Voiceover.Play();
                }
                break;
        }
        return audioSource_Voiceover.clip;
    }
}
