using System.Collections;
using UnityEngine;

public class IntroManager : MonoBehaviour
{
    [Header("Sound")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip alarmClip;

    [Header("Animators")]
    [SerializeField] private Animator eyesAnimator1;
    [SerializeField] private Animator eyesAnimator2;

    [Header("Animation Names")]
    [SerializeField] private string eyesStateName1 = "eyes_Intro";
    [SerializeField] private string eyesStateName2 = "eyes_Intro2";

    [Header("Timing")]
    [SerializeField] private float delayBeforeEyes = 1.5f;
    [SerializeField] private float dialogDelayAfterEyes = 0.5f;
    [SerializeField] private float eyesAnimDuration = 1.0f;

    [Header("Dialog")]
    [SerializeField] private DialogManager dialogManager;

    private void Start()
    {
        StartCoroutine(IntroSequence());
    }

    private IEnumerator IntroSequence()
    {
        Debug.Log("IntroSequence 시작");

        if (audioSource != null && alarmClip != null)
        {
            audioSource.PlayOneShot(alarmClip);
            Debug.Log("알람 재생");
        }
        else
        {
            Debug.LogWarning("audioSource 또는 alarmClip 연결 안 됨");
        }

        yield return new WaitForSeconds(delayBeforeEyes);

        if (eyesAnimator1 != null)
        {
            eyesAnimator1.Play(eyesStateName1, 0, 0f);
            Debug.Log("eyesAnimator1 재생: " + eyesStateName1);
        }
        else
        {
            Debug.LogWarning("eyesAnimator1 연결 안 됨");
        }

        if (eyesAnimator2 != null)
        {
            eyesAnimator2.Play(eyesStateName2, 0, 0f);
            Debug.Log("eyesAnimator2 재생: " + eyesStateName2);
        }
        else
        {
            Debug.LogWarning("eyesAnimator2 연결 안 됨");
        }

        // 눈 애니메이션 끝날 때까지 기다림
        yield return new WaitForSeconds(eyesAnimDuration);

        // 추가로 0.5초 기다림
        yield return new WaitForSeconds(dialogDelayAfterEyes);

        if (dialogManager != null)
        {
            Debug.Log("dialogManager.StartDialog 호출 직전");
            dialogManager.StartDialog();
        }
        else
        {
            Debug.LogError("dialogManager 연결 안 됨");
        }
    }
}