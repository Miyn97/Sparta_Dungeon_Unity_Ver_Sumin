using UnityEngine;

// UI 상태를 정의하는 열거형
public enum UIState
{
    MainMenu,     // 메인 메뉴 상태
    Status,       // 상태창 UI 상태
    Inventory     // 인벤토리 UI 상태
}

// UI 전환을 담당하는 싱글톤 매니저 클래스
public class UIManager : MonoBehaviour
{
    // 전역 접근이 가능한 UIManager 싱글톤 인스턴스
    public static UIManager Instance { get; private set; }

    // 각각의 UI GameObject들을 인스펙터에서 연결
    [SerializeField] private GameObject uiMainMenu;   // 항상 보이는 메인 메뉴 UI
    [SerializeField] private GameObject uiStatus;     // 상태창 UI (조건부 표시)
    [SerializeField] private GameObject uiInventory;  // 인벤토리 UI (조건부 표시)

    // 현재 UI 상태를 저장하는 변수
    private UIState currentState;

    private void Awake()
    {
        // 싱글톤 패턴 설정: 인스턴스가 없으면 this를 사용하고, 중복 생성 방지
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        // 게임 시작 시 초기 상태를 MainMenuOnly로 설정 (UIStatus/UIInventory 숨김)
        ShowMainMenuOnly();
    }

    // UI 상태 변경 메서드 (FSM처럼 동작)
    public void ChangeState(UIState newState)
    {
        // 상태를 변경하고 저장
        currentState = newState;

        // MainMenu는 항상 켜짐
        uiMainMenu.SetActive(true);

        // 선택된 상태에 따라 추가 UI를 조건부로 보여줌
        uiStatus.SetActive(newState == UIState.Status);
        uiInventory.SetActive(newState == UIState.Inventory);
    }

    // 상태를 완전히 MainMenu 전용으로 초기화하는 메서드
    public void ShowMainMenuOnly()
    {
        currentState = UIState.MainMenu;

        // 메인 메뉴는 항상 활성화
        uiMainMenu.SetActive(true);

        // 다른 UI는 모두 비활성화
        uiStatus.SetActive(false);
        uiInventory.SetActive(false);
    }

    // 외부에서 현재 UI 상태를 조회할 수 있도록 하는 Getter
    public UIState GetCurrentState()
    {
        return currentState;
    }
}
