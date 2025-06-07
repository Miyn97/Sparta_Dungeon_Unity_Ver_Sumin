using UnityEngine;
using UnityEngine.UI;

// 메인 메뉴 UI에서 버튼 이벤트를 처리하는 클래스
public class UIMainMenu : MonoBehaviour
{
    [SerializeField] private Button statusButton;    // 상태창으로 이동하는 버튼
    [SerializeField] private Button inventoryButton; // 인벤토리로 이동하는 버튼

    private void Start()
    {
        // 버튼 클릭 시 UI 상태 변경
        statusButton.onClick.AddListener(() => UIManager.Instance.ChangeState(UIState.Status));
        inventoryButton.onClick.AddListener(() => UIManager.Instance.ChangeState(UIState.Inventory));
    }
}
