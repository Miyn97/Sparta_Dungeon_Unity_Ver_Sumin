using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// 인벤토리 UI에서 뒤로가기 버튼 이벤트를 처리하는 클래스
public class UIInventory : MonoBehaviour
{
    [Header("버튼")]
    [SerializeField] private Button backButton;                     // 메인 메뉴로 돌아가는 버튼

    [Header("프리팹 및 부모")]
    [SerializeField] private UISlot slotprefab;                     // 슬롯 프리팹 (아이콘만 복제)
    [SerializeField] private Transform slotParent;                  // 슬롯들을 배치할 부모 오브젝트

    [Header("인벤토리 정보 UI")]
    [SerializeField] private TextMeshProUGUI itemName;              // 마우스 오버 시 표시될 아이템 이름 텍스트
    [SerializeField] private TextMeshProUGUI inventoryCnt1;         // 현재 보유 중인 아이템 개수
    [SerializeField] private TextMeshProUGUI inventoryCnt2;         // 전체 인벤토리 최대 개수

    private List<UISlot> slotList = new List<UISlot>();             // 생성된 슬롯을 저장하는 리스트

    private void Start()
    {
        // 뒤로가기 버튼에 메인 메뉴 전환 기능 연결
        backButton.onClick.AddListener(() => UIManager.Instance.ChangeState(UIState.MainMenu));

        // 인벤토리 UI 초기화 실행
        InitInventoryUI();
    }

    public void InitInventoryUI()
    {
        Character player = GameManager.Instance.Player;             // 현재 플레이어 정보 가져오기
        List<Item> inventory = player.Inventory;                    // 플레이어가 보유한 아이템 리스트

        // 기존 슬롯 제거
        if (slotParent.childCount > 0)
        {
            foreach (Transform child in slotParent)
            {
                Destroy(child.gameObject);
            }
        }
        slotList.Clear();

        // 최대 9칸의 슬롯 생성
        for (int i = 0; i < 9; i++)
        {
            UISlot newSlot = Instantiate(slotprefab, slotParent);   // 프리팹 복제 후 부모에 부착

            if (i < inventory.Count)
            {
                Item item = inventory[i];                           // i번째 아이템 가져오기
                bool isEquipped = player.IsEquipped(item);         // 장착 여부 확인
                newSlot.SetItem(item, isEquipped, itemName);       // 아이템 슬롯 세팅
            }
            else
            {
                newSlot.SetNone(itemName);                          // 아이템 없으면 None 슬롯 세팅
            }

            slotList.Add(newSlot);                                  // 슬롯 리스트에 추가
        }

        // 보유 수량 텍스트 갱신
        inventoryCnt1.text = inventory.Count.ToString();

        // 최대 수량 텍스트 설정
        inventoryCnt2.text = "/ 120";
    }
}
