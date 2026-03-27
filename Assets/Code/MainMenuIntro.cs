using System.Collections;
using UnityEngine;

public class MainMenuIntro : MonoBehaviour
{
    [Header("Circles")]
    public Animator circleBig;
    public Animator circleMid;
    public Animator circleSmall;

    [Header("Buttons")]
    public Animator btnNewGame;
    public Animator btnContinue;
    public Animator btnSetting;
    public Animator btnQuit;

    void Start()
    {
        StartCoroutine(PlaySequence());
    }

    IEnumerator PlaySequence()
    {
        // 🔵 원들
        circleBig.Play("CircleBig_Intro", 0, 0f);

        yield return new WaitForSeconds(0.1f);
        circleMid.Play("CircleMid_Intro", 0, 0f);

        yield return new WaitForSeconds(0.1f);
        circleSmall.Play("CircleSmall_Intro", 0, 0f);

        // 🔥 원 끝나고 잠깐 텀
        yield return new WaitForSeconds(0.2f);

        // 🔴 버튼들
        btnNewGame.Play("NewGame_Intro", 0, 0f);

        yield return new WaitForSeconds(0.08f);
        btnContinue.Play("Continue_Intro", 0, 0f);

        yield return new WaitForSeconds(0.08f);
        btnSetting.Play("Setting_Intro", 0, 0f);

        yield return new WaitForSeconds(0.08f);
        btnQuit.Play("Quit_Intro", 0, 0f);
    }
}