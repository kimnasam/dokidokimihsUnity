using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject selectWindow;

    [Header("Scene")]
    [SerializeField] private string startSceneName = "GameScene";

    [Header("Black Fade")]
    [SerializeField] private Animator blackBgAnimator;
    [SerializeField] private string blackBgStateName = "BlackBg_Intro";
    [SerializeField] private float blackBgAnimDuration = 1.0f;

    private bool isStartingGame = false;

    private void Start()
    {
        if (selectWindow != null)
        {
            selectWindow.SetActive(false);
        }
    }

    // 새 게임 버튼
    public void OpenStartConfirm()
    {
        if (selectWindow != null)
        {
            selectWindow.SetActive(true);
        }
    }

    // "응 시작 안해~" 버튼
    public void CloseStartConfirm()
    {
        if (selectWindow != null)
        {
            selectWindow.SetActive(false);
        }
    }

    // "시작하기" 버튼
    public void StartNewGame()
    {
        if (isStartingGame)
            return;

        StartCoroutine(StartGameSequence());
    }

    private IEnumerator StartGameSequence()
    {
        isStartingGame = true;

        if (selectWindow != null)
        {
            selectWindow.SetActive(false);
        }

        if (blackBgAnimator != null)
        {
            blackBgAnimator.gameObject.SetActive(true);
            blackBgAnimator.Play(blackBgStateName, 0, 0f);
            yield return new WaitForSeconds(blackBgAnimDuration);
        }

        SceneManager.LoadScene(startSceneName);
    }

    // 나가기 버튼
    public void QuitGame()
    {
        Debug.Log("게임 종료 요청");
        Application.Quit();
    }
}