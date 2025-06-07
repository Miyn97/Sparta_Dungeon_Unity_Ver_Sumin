using TMPro;
using UnityEngine;
using UnityEngine.UI;

// 메인 메뉴 UI에서 캐릭터 정보 출력 및 버튼 기능 담당 클래스
public class UIMainMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameText;         // 캐릭터 이름 표시 텍스트
    [SerializeField] private TextMeshProUGUI levelValueText;   // 하단의 경험치 수치 텍스트 (슬라이더 옆)
    [SerializeField] private TextMeshProUGUI expText;          // 슬라이더 위에 표시되는 경험치 텍스트 (중앙)
    [SerializeField] private TextMeshProUGUI descriptionText;  // 레벨 텍스트 (Lv.표시)
    [SerializeField] private TextMeshProUGUI goldText;         // 현재 보유중인 골드 텍스트
    [SerializeField] private Slider levelBar;                  // 경험치 표시용 슬라이더
    [SerializeField] private Button statusButton;              // 상태창 열기 버튼
    [SerializeField] private Button inventoryButton;           // 인벤토리 열기 버튼

    private void Start()
    {
        // 상태창 버튼 클릭 시 UI 상태 변경
        statusButton.onClick.AddListener(() => UIManager.Instance.ChangeState(UIState.Status));

        // 인벤토리 버튼 클릭 시 UI 상태 변경
        inventoryButton.onClick.AddListener(() => UIManager.Instance.ChangeState(UIState.Inventory));
    }

    // 캐릭터 데이터를 받아서 UI 요소에 적용하는 메서드
    public void SetCharacter(Character character)
    {
        nameText.text = character.Name; // 캐릭터 이름 표시

        levelBar.maxValue = character.MaxExp; // 경험치 최대값
        levelBar.value = character.Exp;       // 현재 경험치

        expText.text = $"{character.Exp} / {character.MaxExp}";  // 슬라이더 위 텍스트
        levelValueText.text = $"{character.Level}";           // 슬라이더 하단 (→ 10)

        // 설명 텍스트 삽입
        descriptionText.text =
            "코딩의 노예가 된지 10년짜리 되는 머슴입\n" +
            "니다. 오늘도 밤샐일만 남아서 치킨을 시킬\n" +
            "지도 모른다는 생각에 배민을 키고 있네요.";

        goldText.text = $"{character.Gold:n0}"; // 골드 텍스트 (콤마 포함)
    }

}
