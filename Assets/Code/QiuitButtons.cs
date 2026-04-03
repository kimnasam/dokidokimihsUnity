using UnityEngine;

public class QiuitButtons : MonoBehaviour
{
    public void QuitGame()
    {
        Debug.Log("게임 종료 요청");
        Application.Quit();
    }
}