using UnityEngine;

[System.Serializable]
public class CharacterVoice
{
    public string characterName;
    public AudioClip voiceClip;
}

public class VoiceManager : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;

    [Header("캐릭터 보이스")]
    [SerializeField] private CharacterVoice[] characterVoices;

    public void PlayTypingVoice(string characterName)
    {
        if (string.IsNullOrEmpty(characterName))
            return;

        if (audioSource == null)
            return;

        foreach (CharacterVoice voice in characterVoices)
        {
            if (voice.characterName == characterName)
            {
                if (voice.voiceClip != null)
                {
                    audioSource.PlayOneShot(voice.voiceClip, 0.5f);
                }

                return;
            }
        }
    }
}