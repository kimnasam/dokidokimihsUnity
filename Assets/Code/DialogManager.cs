using System.Collections;
using UnityEngine;
using TMPro;

public class DialogManager : MonoBehaviour
{
    [Header("UI")]
    public GameObject dialogPanel;          // textbar
    public TextMeshProUGUI nameText;        // TextName
    public TextMeshProUGUI dialogText;      // TextDialog
    public GameObject dialogArrow;          // DialogArrow

    [Header("타자 효과")]
    public float typingSpeed = 0.05f;

    [Header("화살표 깜빡임")]
    public float arrowBlinkInterval = 0.4f;

    [Header("대사 데이터")]
    private int currentIndex = 0;

    private string[] names = new string[]
    {
        "문붕이",
        "문붕이",
        "문붕이"
    };

    private string[] dialogs = new string[]
    {
        "으음... 벌써 아침인가",
        "조금만 더 자고 싶은데...",
        "그래도 일어나야지."
    };

    private Coroutine typingCoroutine;
    private Coroutine arrowBlinkCoroutine;

    private bool isTyping = false;
    private bool isDialogActive = false;
    private bool canGoNext = false;

    private void Start()
    {
        if (dialogPanel != null)
        {
            dialogPanel.SetActive(false);
        }

        if (dialogArrow != null)
        {
            dialogArrow.SetActive(false);
        }
    }

    public void StartDialog()
    {
        if (dialogPanel == null || nameText == null || dialogText == null)
        {
            Debug.LogError("DialogManager UI 연결이 비어 있음");
            return;
        }

        dialogPanel.SetActive(true);
        currentIndex = 0;
        isDialogActive = true;
        canGoNext = false;

        if (dialogArrow != null)
        {
            dialogArrow.SetActive(false);
        }

        ShowDialog();
    }

    private void ShowDialog()
    {
        nameText.text = names[currentIndex];
        canGoNext = false;

        if (dialogArrow != null)
        {
            dialogArrow.SetActive(false);
        }

        if (arrowBlinkCoroutine != null)
        {
            StopCoroutine(arrowBlinkCoroutine);
            arrowBlinkCoroutine = null;
        }

        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        typingCoroutine = StartCoroutine(TypeDialog(dialogs[currentIndex]));
    }

    private IEnumerator TypeDialog(string fullText)
    {
        isTyping = true;
        dialogText.text = "";

        for (int i = 0; i < fullText.Length; i++)
        {
            dialogText.text += fullText[i];
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
        canGoNext = true;

        if (dialogArrow != null)
        {
            arrowBlinkCoroutine = StartCoroutine(BlinkArrow());
        }
    }

    private IEnumerator BlinkArrow()
    {
        while (canGoNext)
        {
            dialogArrow.SetActive(true);
            yield return new WaitForSeconds(arrowBlinkInterval);

            dialogArrow.SetActive(false);
            yield return new WaitForSeconds(arrowBlinkInterval);
        }

        dialogArrow.SetActive(false);
    }

    private void Update()
    {
        if (!isDialogActive || dialogPanel == null || !dialogPanel.activeSelf)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            // 아직 출력 중이면 전체 문장 즉시 표시
            if (isTyping)
            {
                CompleteCurrentLine();
            }
            // 출력 끝났고 화살표 깜빡이는 상태면 다음 대사
            else if (canGoNext)
            {
                NextDialog();
            }
        }
    }

    private void CompleteCurrentLine()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
            typingCoroutine = null;
        }

        dialogText.text = dialogs[currentIndex];
        isTyping = false;
        canGoNext = true;

        if (dialogArrow != null)
        {
            if (arrowBlinkCoroutine != null)
            {
                StopCoroutine(arrowBlinkCoroutine);
            }

            arrowBlinkCoroutine = StartCoroutine(BlinkArrow());
        }
    }

    private void NextDialog()
    {
        canGoNext = false;

        if (dialogArrow != null)
        {
            dialogArrow.SetActive(false);
        }

        if (arrowBlinkCoroutine != null)
        {
            StopCoroutine(arrowBlinkCoroutine);
            arrowBlinkCoroutine = null;
        }

        currentIndex++;

        if (currentIndex >= dialogs.Length)
        {
            dialogPanel.SetActive(false);
            isDialogActive = false;
            return;
        }

        ShowDialog();
    }
}