using TMPro;
using UnityEngine;
using UnityEngine.UI;

// 캐릭터의 스탯 정보를 보여주는 상태창 UI 클래스
public class UIStatus : MonoBehaviour
{
    [Header("텍스트")]
    [SerializeField] private TextMeshProUGUI attackText;      // 공격력 라벨 텍스트
    [SerializeField] private TextMeshProUGUI defenseText;     // 방어력 라벨 텍스트
    [SerializeField] private TextMeshProUGUI healthText;      // 체력 라벨 텍스트
    [SerializeField] private TextMeshProUGUI criticalText;    // 치명타 라벨 텍스트

    [Header("수치")]
    [SerializeField] private TextMeshProUGUI attackCnt;       // 최종 공격력 수치
    [SerializeField] private TextMeshProUGUI defenseCnt;      // 최종 방어력 수치
    [SerializeField] private TextMeshProUGUI healthCnt;       // 최종 체력 수치
    [SerializeField] private TextMeshProUGUI criticalCnt;     // 최종 치명타 수치

    [Header("버튼")]
    [SerializeField] private Button backButton;               // 뒤로가기 버튼

    private Character character;                              // 참조할 캐릭터 정보 캐싱

    private void Start()
    {
        // 뒤로가기 버튼을 클릭하면 메인 메뉴 UI만 표시되도록 설정
        backButton.onClick.AddListener(() => UIManager.Instance.ShowMainMenuOnly());
    }

    // 상태창이 켜질 때 자동으로 최신 스탯 갱신
    private void OnEnable()
    {
        RefreshStatusUI(); // 항상 Total 값으로 최신화
    }

    // 외부에서 캐릭터 정보를 받아와 UI에 세팅하는 메서드
    public void SetCharacter(Character character)
    {
        this.character = character;         // 전달받은 캐릭터 참조 저장
        RefreshStatusUI();                  // 상태창 텍스트 갱신
    }

    // 캐릭터의 상태를 UI에 갱신하는 메서드
    public void RefreshStatusUI()
    {
        if (character == null) return;      // 캐릭터가 없으면 종료

        // 텍스트 라벨은 고정된 이름으로 표시
        attackText.text = "공격력";
        defenseText.text = "방어력";
        healthText.text = "체력";
        criticalText.text = "치명타";

        // 장착 아이템 포함한 총합 스탯을 표시
        attackCnt.text = character.TotalAttack.ToString();       // 최종 공격력 표시
        defenseCnt.text = character.TotalDefense.ToString();     // 최종 방어력 표시
        healthCnt.text = character.TotalHealth.ToString();       // 최종 체력 표시
        criticalCnt.text = character.TotalCritical.ToString();   // 최종 치명타 표시
    }
}
