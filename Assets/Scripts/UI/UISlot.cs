using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// 인벤토리 슬롯 하나를 담당하는 클래스
public class UISlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [Header("UI 구성요소")]
    [SerializeField] private Image iconImage;                       // 슬롯에 표시될 아이템 이미지
    [SerializeField] private GameObject equipMark;                  // 장착 마크 (E 텍스트 등)
    [SerializeField] private Sprite noneSprite;                     // 아이템이 없을 때 표시할 None 아이콘

    private Item itemData;                                          // 현재 슬롯에 할당된 아이템 정보
    private TextMeshProUGUI hoverNameText;                          // 마우스 오버 시 이름을 표시할 텍스트 (지금은 사용하지 않지만 구조 유지)

    private void Awake()
    {
        iconImage.gameObject.SetActive(false);                      // 슬롯 생성 시 아이콘 비활성화
        equipMark.SetActive(false);                                 // 슬롯 생성 시 장착 마크 비활성화
    }

    // 슬롯에 실제 아이템 정보가 들어왔을 때 설정
    public void SetItem(Item item, bool isEquipped, TextMeshProUGUI hoverText)
    {
        itemData = item;                                            // 아이템 정보 저장
        iconImage.sprite = item.Icon;                               // 아이콘 이미지 설정
        iconImage.gameObject.SetActive(true);                       // 아이콘 이미지 보이게 설정
        equipMark.SetActive(isEquipped);                            // 장착된 상태면 E 마크 활성화
        hoverNameText = hoverText;                                  // (현재 사용되지 않음, 예전 hover용 텍스트)
    }

    // 슬롯에 아이템이 없을 경우 None 슬롯으로 초기화
    public void SetNone(TextMeshProUGUI hoverText)
    {
        itemData = null;                                            // 아이템 비우기
        iconImage.sprite = noneSprite;                              // None 이미지 설정
        iconImage.gameObject.SetActive(true);                       // None 아이콘 활성화
        equipMark.SetActive(false);                                 // 장착 마크 비활성화
        hoverNameText = hoverText;                                  // (현재 사용되지 않음)
    }

    // 슬롯에 마우스 올렸을 때 실행
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (hoverNameText != null)
            hoverNameText.text = itemData != null ? itemData.Name : "없음";

        if (itemData != null)
            ItemTooltipUI.Instance.Show(itemData); // 툴팁 표시
    }

    // 슬롯에서 마우스 벗어날 때
    public void OnPointerExit(PointerEventData eventData)
    {
        if (hoverNameText != null)
            hoverNameText.text = "";

        ItemTooltipUI.Instance.Hide(); // 툴팁 숨기기
    }


    // 슬롯 클릭 시 장착 UI 호출
    public void OnPointerClick(PointerEventData eventData)
    {
        if (itemData == null) return;

        // 좌클릭일 때만 실행
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            EquipUIManager ui = FindObjectOfType<EquipUIManager>();
            bool isEquipped = GameManager.Instance.Player.IsEquipped(itemData);

            // 클릭된 슬롯, 아이템, 장착 여부 전달
            ui.Show(this, itemData, isEquipped);
        }
    }

    // 외부에서 호출: E 마크 상태를 갱신함
    public void RefreshEquipMark()
    {
        if (itemData == null) return;

        bool equipped = GameManager.Instance.Player.IsEquipped(itemData);
        equipMark.SetActive(equipped);
    }

    // 아이콘 이미지 강제로 갱신 (리프레시용)
    public void RefreshUI()
    {
        if (itemData == null) return;

        iconImage.sprite = itemData.Icon;
        iconImage.gameObject.SetActive(true);
    }
}
