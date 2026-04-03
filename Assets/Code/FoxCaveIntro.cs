using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FoxCaveIntro : MonoBehaviour
{
    [Header("Canvas Groups")]
    [SerializeField] private CanvasGroup blackBackgroundGroup;
    [SerializeField] private CanvasGroup rogoGroup;
    [SerializeField] private CanvasGroup foxCaveGroup;
    [SerializeField] private CanvasGroup byNasamGroup;

    [Header("Timing")]
    [SerializeField] private float initialDelay = 0.6f;

    [SerializeField] private float rogoFadeTime = 0.5f;
    [SerializeField] private float gapAfterRogo = 0.2f;

    [SerializeField] private float foxCaveFadeTime = 0.5f;
    [SerializeField] private float gapAfterFoxCave = 0.2f;

    [SerializeField] private float byNasamFadeTime = 0.5f;
    [SerializeField] private float holdTime = 1.0f;

    [SerializeField] private float allFadeOutTime = 1.0f;

    [Header("Next Scene")]
    [SerializeField] private string nextSceneName = "main page";

    private void Start()
    {
        StartCoroutine(PlayIntroSequence());
    }

    private IEnumerator PlayIntroSequence()
    {
        // 시작 상태
        blackBackgroundGroup.alpha = 1f;
        rogoGroup.alpha = 0f;
        foxCaveGroup.alpha = 0f;
        byNasamGroup.alpha = 0f;

        // 처음 검은 화면만 잠깐 보여주기
        yield return new WaitForSeconds(initialDelay);

        // rogo 등장
        yield return StartCoroutine(FadeCanvasGroup(rogoGroup, 0f, 1f, rogoFadeTime));

        // 잠깐 대기
        yield return new WaitForSeconds(gapAfterRogo);

        // fox cave 등장
        yield return StartCoroutine(FadeCanvasGroup(foxCaveGroup, 0f, 1f, foxCaveFadeTime));

        // 잠깐 대기
        yield return new WaitForSeconds(gapAfterFoxCave);

        // by. nasam 등장
        yield return StartCoroutine(FadeCanvasGroup(byNasamGroup, 0f, 1f, byNasamFadeTime));

        // 전체 다 보이는 시간
        yield return new WaitForSeconds(holdTime);

        // 전부 같이 사라지기
        StartCoroutine(FadeCanvasGroup(blackBackgroundGroup, 1f, 0f, allFadeOutTime));
        StartCoroutine(FadeCanvasGroup(rogoGroup, 1f, 0f, allFadeOutTime));
        StartCoroutine(FadeCanvasGroup(foxCaveGroup, 1f, 0f, allFadeOutTime));
        yield return StartCoroutine(FadeCanvasGroup(byNasamGroup, 1f, 0f, allFadeOutTime));

        // 다음 씬으로 이동
        SceneManager.LoadScene(nextSceneName);
    }

    private IEnumerator FadeCanvasGroup(CanvasGroup targetGroup, float startAlpha, float endAlpha, float duration)
    {
        float time = 0f;
        targetGroup.alpha = startAlpha;

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;
            targetGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, t);
            yield return null;
        }

        targetGroup.alpha = endAlpha;
    }
}