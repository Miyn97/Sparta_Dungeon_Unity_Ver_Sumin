using TMPro;
using UnityEngine;
using UnityEngine.UI;

// 캐릭터의 스탯 정보를 보여주는 상태창 UI 클래스
public class UIStatus : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI attackText;    // 공격력 텍스트
    [SerializeField] private TextMeshProUGUI attackCnt;     // 공격력수치
    [SerializeField] private TextMeshProUGUI defenseText;   // 방어력 텍스트
    [SerializeField] private TextMeshProUGUI defenseCnt;    // 방어력수치
    [SerializeField] private TextMeshProUGUI healthText;    // 체력 텍스트
    [SerializeField] private TextMeshProUGUI healthCnt;     // 체력수치
    [SerializeField] private TextMeshProUGUI criticalText;  // 치명타 텍스트
    [SerializeField] private TextMeshProUGUI criticalCnt;   // 치명타수치
    [SerializeField] private Button backButton;             // 뒤로가기 버튼

    private void Start()
    {
        // 뒤로가기 버튼 누르면 메인메뉴만 표시
        backButton.onClick.AddListener(() => UIManager.Instance.ShowMainMenuOnly());
    }

    // 외부에서 캐릭터 데이터를 받아 UI에 표시
    public void SetCharacter(Character character)
    {
        attackText.text = $"공격력";                       // 공격력
        defenseText.text = $"방어력";                      // 방어력
        healthText.text = $"체력";                         // 체력
        criticalText.text = $"치명타";                     // 치명타
        attackCnt.text = $"{character.Attack}";           // 공격력수치
        defenseCnt.text = $"{character.Defense}";          // 방어력수치
        healthCnt.text = $"{character.Health}";            // 체력수치
        criticalCnt.text = $"{character.Critical}";        // 치명타수치
    }
}
