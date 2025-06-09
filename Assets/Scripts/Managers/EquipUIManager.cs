using System.Collections.Generic;

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// 장착 UI를 관리하는 매니저 클래스
public class EquipUIManager : MonoBehaviour
{
    public static EquipUIManager Instance { get; private set; } // 싱글톤 인스턴스

    [Header("UI 구성요소")]
    [SerializeField] private GameObject equipPanel;       // 장착/해제 UI 전체 오브젝트
    [SerializeField] private Button equipButton;          // 장착 버튼
    [SerializeField] private Button unequipButton;        // 해제 버튼

    private RectTransform rectTransform;                  // 패널 위치 조정용 RectTransform
    private UISlot currentSlot;                           // 클릭된 슬롯 캐싱 (E 마크 갱신용)

    private void Awake()
    {
        // 싱글톤 설정
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        // 패널 RectTransform 캐싱
        rectTransform = equipPanel.GetComponent<RectTransform>();

        // 시작 시 패널 비활성화
        equipPanel.SetActive(false);
    }

    private void Update()
    {
        // 패널이 열려있을 때만 검사
        if (!equipPanel.activeSelf) return;

        // 좌클릭 시 UI 외부 클릭 여부 검사
        if (Input.GetMouseButtonDown(0))
        {
            // EquipUI 내부가 아닌 곳을 클릭했다면 닫기
            if (!IsPointerOverEquipUI())
                Hide();
        }
    }

    // 슬롯 클릭 시 장착 UI를 표시
    public void Show(UISlot slot, Item item, bool isEquipped)
    {
        currentSlot = slot;                         // 클릭된 슬롯 저장
        equipPanel.SetActive(true);                 // UI 활성화

        // 마우스 클릭 위치 기준으로 위치 설정 (약간 오른쪽 아래 오프셋)
        Vector2 mousePosition = Input.mousePosition;
        rectTransform.position = mousePosition + new Vector2(100f, -50f);

        // 버튼 활성화 상태 결정
        equipButton.interactable = CanEquip(item) && !isEquipped;
        unequipButton.interactable = isEquipped;

        // 리스너 초기화
        equipButton.onClick.RemoveAllListeners();
        unequipButton.onClick.RemoveAllListeners();

        // 장착 버튼 리스너 등록
        equipButton.onClick.AddListener(() =>
        {
            GameManager.Instance.Player.Equip(item); // 아이템 장착
            UIManager.Instance.UIInventory.InitInventoryUI(); // 슬롯 상태 동기화
            UIManager.Instance.UIMainMenu.SetCharacter(GameManager.Instance.Player); // 메인메뉴 갱신
            UIManager.Instance.UIStatus.RefreshStatusUI(); // 상태창 갱신
            CloseUI();
        });

        // 해제 버튼 리스너 등록
        unequipButton.onClick.AddListener(() =>
        {
            GameManager.Instance.Player.UnEquip(item); // 아이템 해제
            UIManager.Instance.UIInventory.InitInventoryUI(); // 슬롯 상태 동기화
            UIManager.Instance.UIMainMenu.SetCharacter(GameManager.Instance.Player); // 메인메뉴 갱신
            UIManager.Instance.UIStatus.RefreshStatusUI(); // 상태창 갱신
            CloseUI();
        });
    }

    // 외부에서 강제 비활성화할 때 호출
    public void Hide()
    {
        equipPanel.SetActive(false); // UI 닫기만 처리
    }

    // UI 닫고 현재 슬롯의 장착 마크 갱신
    private void CloseUI()
    {
        equipPanel.SetActive(false);

        if (currentSlot != null)
            currentSlot.RefreshEquipMark();
    }

    // 아이템 장착 가능 여부 판단
    private bool CanEquip(Item item)
    {
        var player = GameManager.Instance.Player;
        string name = item.Name;

        bool isWeapon = name == "검" || name == "망치" || name == "활" || name == "마법책";
        bool isShield = name == "방패";
        bool isAccessory = name == "반지" || name == "투구";

        Item currentWeapon = player.GetEquippedWeapon();
        Item currentShield = player.GetEquippedShield();

        if (name == "방패")
        {
            // 무기가 장착돼 있고, 활이 아닐 경우에만 장착 가능
            return currentWeapon != null && currentWeapon.Name != "활";
        }

        if (name == "활")
        {
            // 방패가 없는 상태 + 무기도 미장착 상태에서만 장착 가능
            return currentShield == null && !player.HasEquippedWeapon();
        }

        if (isWeapon)
        {
            // 다른 무기가 장착돼 있지 않은 경우만 장착 가능
            return !player.HasEquippedWeapon();
        }

        if (isAccessory)
        {
            // 장신구는 언제든 장착 가능
            return true;
        }

        return false; // 나머지는 장착 불가
    }

    // 현재 마우스 클릭이 EquipUI 내부인지 확인하는 함수
    private bool IsPointerOverEquipUI()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        };

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        foreach (var result in results)
        {
            if (result.gameObject == null) continue;

            // EquipPanel 내부 요소를 클릭했는지 확인
            if (result.gameObject.transform.IsChildOf(equipPanel.transform))
                return true;
        }

        return false; // 외부 클릭
    }
}
