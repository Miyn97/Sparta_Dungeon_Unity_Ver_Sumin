using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// 인벤토리 슬롯 하나를 담당하는 클래스
public class UISlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("UI 구성요소")]
    [SerializeField] private Image iconImage;                       // 슬롯에 표시될 아이템 이미지
    [SerializeField] private GameObject equipMark;                  // 장착 마크 (E 텍스트 등)
    [SerializeField] private Sprite noneSprite;                     // 아이템이 없을 때 표시할 None 아이콘

    private Item itemData;                                          // 현재 슬롯에 할당된 아이템 정보
    private TextMeshProUGUI hoverNameText;                          // 마우스 오버 시 이름을 표시할 텍스트

    private void Awake()
    {
        iconImage.gameObject.SetActive(false);                      // 슬롯 생성 시 아이콘 비활성화
        equipMark.SetActive(false);                                 // 슬롯 생성 시 장착 마크 비활성화
    }

    // 실제 아이템이 있는 경우 슬롯에 정보 세팅
    public void SetItem(Item item, bool isEquipped, TextMeshProUGUI hoverText)
    {
        itemData = item;                                            // 아이템 데이터 저장
        iconImage.sprite = item.Icon;                               // 아이콘 이미지 설정
        iconImage.gameObject.SetActive(true);                       // 아이콘 오브젝트 활성화
        equipMark.SetActive(isEquipped);                            // 장착 여부에 따라 마크 표시
        hoverNameText = hoverText;                                  // 마우스 오버용 이름 텍스트 저장
    }

    // 아이템이 없는 경우 슬롯에 None 아이콘 표시
    public void SetNone(TextMeshProUGUI hoverText)
    {
        itemData = null;                                            // 아이템 없음으로 설정
        iconImage.sprite = noneSprite;                              // None 아이콘으로 설정
        iconImage.gameObject.SetActive(true);                       // None 아이콘 오브젝트 활성화
        equipMark.SetActive(false);                                 // 장착 마크 비활성화
        hoverNameText = hoverText;                                  // 마우스 오버용 이름 텍스트 저장
    }

    // 마우스를 슬롯 위에 올렸을 때 아이템 이름 표시
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (hoverNameText == null) return;

        if (itemData != null)
            hoverNameText.text = itemData.Name;                     // 아이템 이름 출력
        else
            hoverNameText.text = "없음";                            // 아이템이 없으면 "없음" 출력
    }

    // 마우스가 슬롯을 벗어났을 때 텍스트 초기화
    public void OnPointerExit(PointerEventData eventData)
    {
        if (hoverNameText != null)
            hoverNameText.text = "";                                // 텍스트 초기화
    }

    // 슬롯에 할당된 아이템 정보로 UI 갱신
    public void RefreshUI()
    {
        if (itemData == null) return;

        iconImage.sprite = itemData.Icon;                           // 아이콘 갱신
        iconImage.gameObject.SetActive(true);                       // 아이콘이 꺼져 있을 수 있으므로 다시 켜기
    }
}
