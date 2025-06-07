using UnityEngine;
using UnityEngine.UI;

// 인벤토리 UI에서 뒤로가기 버튼 이벤트를 처리하는 클래스
public class UIInventory : MonoBehaviour
{
    [SerializeField] private Button backButton; // 메인 메뉴로 돌아가는 버튼

    private void Start()
    {
        // 버튼 클릭 시 메인 메뉴 상태로 전환
        backButton.onClick.AddListener(() => UIManager.Instance.ChangeState(UIState.MainMenu));
    }
}
