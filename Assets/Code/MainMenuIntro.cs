using System.Collections;
using UnityEngine;

public class MainMenuIntro : MonoBehaviour
{
    public Animator circleBig;
    public Animator circleMid;
    public Animator circleSmall;

    public Animator btnNewGame;
    public Animator btnContinue;
    public Animator btnSetting;
    public Animator btnQuit;

    private void Start()
    {
        StartCoroutine(PlaySequence());
    }

    IEnumerator PlaySequence()
    {
        circleBig.Play("CircleBig_Intro", 0, 0f);

        yield return new WaitForSeconds(0.15f);
        circleMid.Play("CircleMid_Intro", 0, 0f);

        yield return new WaitForSeconds(0.15f);
        btnNewGame.Play("BtnNewGame_Intro", 0, 0f);

        yield return new WaitForSeconds(0.05f);
        circleSmall.Play("CircleSmall_Intro", 0, 0f);

        yield return new WaitForSeconds(0.08f);
        btnContinue.Play("BtnContinue_Intro", 0, 0f);

        yield return new WaitForSeconds(0.08f);
        btnSetting.Play("BtnSetting_Intro", 0, 0f);

        yield return new WaitForSeconds(0.08f);
        btnQuit.Play("BtnQuit_Intro", 0, 0f);
    }
}