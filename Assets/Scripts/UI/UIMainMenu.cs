using TMPro;

using UnityEngine;
using UnityEngine.UI;

// 메인 메뉴 UI에서 캐릭터 정보 출력 및 버튼 기능 담당 클래스
public class UIMainMenu : MonoBehaviour
{
    [Header("텍스트")]
    [SerializeField] private TextMeshProUGUI nameText;            // 캐릭터 이름 텍스트
    [SerializeField] private TextMeshProUGUI goldText;            // 보유 골드 텍스트
    [SerializeField] private TextMeshProUGUI expText;             // 경험치 텍스트 (슬라이더 위)
    [SerializeField] private TextMeshProUGUI levelValueText;      // 레벨 수치 텍스트 (슬라이더 옆)
    [SerializeField] private TextMeshProUGUI descriptionText;     // 캐릭터 설명 텍스트

    [Header("슬라이더")]
    [SerializeField] private Slider levelBar;                     // 경험치 표시용 슬라이더

    [Header("버튼")]
    [SerializeField] private Button statusButton;                 // 상태창 열기 버튼
    [SerializeField] private Button inventoryButton;              // 인벤토리 열기 버튼

    [Header("버튼 오브젝트")]
    [SerializeField] private GameObject statusButtonObj;          // 상태창 버튼 오브젝트
    [SerializeField] private GameObject inventoryButtonObj;       // 인벤토리 버튼 오브젝트

    private void Start()
    {
        // 상태창 버튼 클릭 시: 상태창 열기 + 버튼 숨김
        statusButton.onClick.AddListener(() =>
        {
            HideMenuButtons();                                      // 버튼 숨기기
            UIManager.Instance.ChangeState(UIState.Status);        // 상태창으로 전환
        });

        // 인벤토리 버튼 클릭 시: 인벤토리 열기 + 버튼 숨김
        inventoryButton.onClick.AddListener(() =>
        {
            HideMenuButtons();                                      // 버튼 숨기기
            UIManager.Instance.ChangeState(UIState.Inventory);     // 인벤토리로 전환
        });
    }

    // 캐릭터 정보를 받아 UI에 표시
    public void SetCharacter(Character character)
    {
        nameText.text = character.Name;
        levelBar.maxValue = character.MaxExp;
        levelBar.value = character.Exp;
        expText.text = $"{character.Exp} / {character.MaxExp}";
        levelValueText.text = $"{character.Level}";

        descriptionText.text =
            "코딩의 노예가 된지 10년짜리 되는 머슴입\n" +
            "니다. 오늘도 밤샐일만 남아서 치킨을 시킬\n" +
            "지도 모른다는 생각에 배민을 키고 있네요.";

        goldText.text = $"{character.Gold:n0}";
    }

    // 버튼을 비활성화 (숨김 처리)
    public void HideMenuButtons()
    {
        statusButtonObj.SetActive(false);
        inventoryButtonObj.SetActive(false);
    }

    // 버튼을 다시 활성화 (다시 보이게)
    public void ShowMenuButtons()
    {
        statusButtonObj.SetActive(true);
        inventoryButtonObj.SetActive(true);
    }
}
