using TMPro;
using UnityEngine.UI;
using UnityEngine;

// 마우스를 따라다니며 아이템 능력치를 보여주는 툴팁 UI 클래스
public class ItemTooltipUI : MonoBehaviour
{
    public static ItemTooltipUI Instance { get; private set; }   // 싱글톤 인스턴스

    [Header("배경 이미지")]
    [SerializeField] private Image backgroundImage; // 배경 Image 오브젝트 참조

    [Header("툴팁 패널")]
    [SerializeField] private CanvasGroup canvasGroup;            // 패널의 표시 여부 제어
    [SerializeField] private RectTransform panelRect;            // 패널의 위치 제어용 RectTransform

    [Header("옵션 이름")]
    [SerializeField] private TextMeshProUGUI contentText1;       // 능력치 이름 1 (예: 공격력)
    [SerializeField] private TextMeshProUGUI contentText2;       // 능력치 이름 2 (예: 체력)

    [Header("옵션 수치")]
    [SerializeField] private TextMeshProUGUI contentCnt1;        // 능력치 수치 1 (예: +15)
    [SerializeField] private TextMeshProUGUI contentCnt2;        // 능력치 수치 2 (예: +5)

    [Header("마우스 오프셋")]
    [SerializeField] private Vector2 offset = new Vector2(10f, -10f); // 마우스 기준 위치 조절

    private void Awake()
    {
        // 싱글톤 패턴 적용
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        Hide(); // 시작 시 비활성화
    }

    private void Update()
    {
        // 툴팁이 보일 때만 마우스를 따라다님
        if (canvasGroup.alpha > 0f)
        {
            Vector2 mousePos = Input.mousePosition;
            panelRect.position = mousePos + offset; // 마우스 위치에 오프셋 적용
        }
    }

    // 아이템 능력치를 받아 UI에 표시
    public void Show(Item item)
    {
        // 최대 2개의 능력치만 표시 가능하므로 임시 배열 사용
        string[] statNames = new string[2];
        string[] statValues = new string[2];
        int index = 0;

        // 순서대로 능력치가 있으면 채워 넣음 (최대 2개)
        if (item.Attack != 0 && index < 2)
        {
            statNames[index] = "공격력";
            statValues[index] = $"+{item.Attack}";
            index++;
        }

        if (item.Defense != 0 && index < 2)
        {
            statNames[index] = "방어력";
            statValues[index] = $"+{item.Defense}";
            index++;
        }

        if (item.Health != 0 && index < 2)
        {
            statNames[index] = "체력";
            statValues[index] = $"+{item.Health}";
            index++;
        }

        if (item.Critical != 0 && index < 2)
        {
            statNames[index] = "치명타";
            statValues[index] = $"+{item.Critical}";
            index++;
        }

        // 첫 번째 능력치 세팅
        if (index >= 1)
        {
            contentText1.text = statNames[0];
            contentCnt1.text = statValues[0];
        }
        else
        {
            contentText1.text = "";
            contentCnt1.text = "";
        }

        // 두 번째 능력치 세팅
        if (index >= 2)
        {
            contentText2.text = statNames[1];
            contentCnt2.text = statValues[1];
        }
        else
        {
            contentText2.text = "";
            contentCnt2.text = "";
        }

        // 패널 표시
        canvasGroup.alpha = 1f; // 전체 알파는 1로 유지
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;

        // 배경만 반투명하게 설정
        Color bgColor = backgroundImage.color;
        bgColor.a = 0.55f; // 원하는 투명도
        backgroundImage.color = bgColor;

    }

    // 툴팁 UI 숨김 처리
    public void Hide()
    {
        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }
}
