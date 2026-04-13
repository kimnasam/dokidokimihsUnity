using System.Collections;
using UnityEngine;
using TMPro;

[System.Serializable]
public class DialogLine
{
    public string name;
    public string dialog;
    public string animation;
}

[System.Serializable]
public class DialogData
{
    public DialogLine[] lines;
}

public class DialogManager : MonoBehaviour
{
    [Header("UI")]
    public GameObject dialogPanel;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogText;
    public GameObject dialogArrow;
    public Animator dialogArrowAnimator;

    [Header("배경 애니메이션")]
    public Animator myRoomAnimator;
    [Header("타자 효과")]
    public float typingSpeed = 0.05f;

    [Header("JSON 설정")]
    public string jsonFileName = "Dialog/scene1_dialog";

    private int currentIndex = 0;
    private DialogData dialogData;

    private Coroutine typingCoroutine;

    private bool isTyping = false;
    private bool isDialogActive = false;
    private bool canGoNext = false;

    private void Start()
    {
        LoadDialogFromJson();

        if (dialogPanel != null)
        {
            dialogPanel.SetActive(false);
        }

        if (dialogArrow != null)
        {
            dialogArrow.SetActive(false);
        }
    }

    private void LoadDialogFromJson()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>(jsonFileName);

        if (jsonFile == null)
        {
            Debug.LogError($"JSON 파일을 찾을 수 없음: Resources/{jsonFileName}.json");
            return;
        }

        dialogData = JsonUtility.FromJson<DialogData>(jsonFile.text);

        if (dialogData == null || dialogData.lines == null || dialogData.lines.Length == 0)
        {
            Debug.LogError("JSON 대사 데이터가 비어 있음");
        }
    }

    public void StartDialog()
    {
        if (dialogPanel == null || nameText == null || dialogText == null)
        {
            Debug.LogError("DialogManager UI 연결이 비어 있음");
            return;
        }

        if (dialogData == null || dialogData.lines == null || dialogData.lines.Length == 0)
        {
            Debug.LogError("불러온 대사 데이터가 없음");
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
        nameText.text = dialogData.lines[currentIndex].name;
        canGoNext = false;

        if (dialogArrow != null)
        {
            dialogArrow.SetActive(false);
        }

        // 현재 대사 줄의 animation 값이 비어있지 않으면 실행
        if (!string.IsNullOrEmpty(dialogData.lines[currentIndex].animation))
        {
            if (myRoomAnimator != null)
            {
                myRoomAnimator.Play(dialogData.lines[currentIndex].animation, 0, 0f);
            }
            else
            {
                Debug.LogWarning("myRoomAnimator가 연결되지 않았음");
            }
        }

        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        typingCoroutine = StartCoroutine(TypeDialog(dialogData.lines[currentIndex].dialog));
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

        ShowArrowPulse();
    }

    private void ShowArrowPulse()
    {
        if (dialogArrow == null)
            return;

        dialogArrow.SetActive(true);

        if (dialogArrowAnimator != null)
        {
            dialogArrowAnimator.Play("DialogArrow_Pulse", 0, 0f);
        }
    }

    private void HideArrow()
    {
        if (dialogArrow != null)
        {
            dialogArrow.SetActive(false);
        }
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
            // 출력 끝났으면 다음 대사
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

        dialogText.text = dialogData.lines[currentIndex].dialog;
        isTyping = false;
        canGoNext = true;

        ShowArrowPulse();
    }

    private void NextDialog()
    {
        canGoNext = false;
        HideArrow();

        currentIndex++;

        if (currentIndex >= dialogData.lines.Length)
        {
            dialogPanel.SetActive(false);
            isDialogActive = false;
            return;
        }

        ShowDialog();
    }
}