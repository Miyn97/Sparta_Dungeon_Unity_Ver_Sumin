using UnityEngine;

// UI 상태를 정의하는 열거형 (FSM 방식)
public enum UIState
{
    MainMenu,     // 메인 메뉴 상태
    Status,       // 상태창 UI 상태
    Inventory     // 인벤토리 UI 상태
}

// UI 전환과 UI 접근을 담당하는 싱글톤 매니저 클래스
public class UIManager : MonoBehaviour
{
    // 싱글톤 인스턴스
    public static UIManager Instance { get; private set; }

    // 각각의 UI 오브젝트를 인스펙터에서 연결
    [SerializeField] private GameObject uiMainMenu;   // 메인 메뉴 UI 오브젝트
    [SerializeField] private GameObject uiStatus;     // 상태창 UI 오브젝트
    [SerializeField] private GameObject uiInventory;  // 인벤토리 UI 오브젝트

    // 현재 UI 상태 (메인, 상태창, 인벤토리)
    private UIState currentState;

    // 각 UI의 스크립트 컴포넌트를 캐싱
    private UIMainMenu uiMainMenuScript;
    private UIStatus uiStatusScript;
    private UIInventory uiInventoryScript;

    // 외부에서 각 UI 스크립트에 접근할 수 있는 프로퍼티
    public UIMainMenu UIMainMenu => uiMainMenuScript;
    public UIStatus UIStatus => uiStatusScript;
    public UIInventory UIInventory => uiInventoryScript;

    private void Awake()
    {
        // 싱글톤 초기화
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        // 하위 오브젝트에서 컴포넌트 참조 캐싱
        uiMainMenuScript = uiMainMenu.GetComponent<UIMainMenu>();
        uiStatusScript = uiStatus.GetComponent<UIStatus>();
        uiInventoryScript = uiInventory.GetComponent<UIInventory>();
    }

    private void Start()
    {
        // 초기화: 메인 메뉴만 보이도록 설정
        ShowMainMenuOnly();
    }

    // UI 상태 변경 메서드
    public void ChangeState(UIState newState)
    {
        currentState = newState;

        // 메인 메뉴는 항상 활성화 상태 유지
        uiMainMenu.SetActive(true);

        // 상태창과 인벤토리는 선택된 상태에 따라 활성화
        uiStatus.SetActive(newState == UIState.Status);
        uiInventory.SetActive(newState == UIState.Inventory);
    }

    // 서브 UI를 모두 끄고 메인 메뉴만 표시
    public void ShowMainMenuOnly()
    {
        currentState = UIState.MainMenu;

        uiMainMenu.SetActive(true);
        uiStatus.SetActive(false);
        uiInventory.SetActive(false);
    }

    // 현재 UI 상태 반환
    public UIState GetCurrentState()
    {
        return currentState;
    }
}
