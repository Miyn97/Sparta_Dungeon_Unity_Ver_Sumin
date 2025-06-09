using UnityEngine;

// UI 상태를 정의하는 열거형 (FSM 방식)
public enum UIState
{
    MainMenu,     // 메인 메뉴 상태
    Status,       // 상태창 UI 상태
    Inventory     // 인벤토리 UI 상태
}

// UI 전환과 접근을 관리하는 싱글톤 UI 매니저 클래스
public class UIManager : MonoBehaviour
{
    // 외부에서 접근 가능한 싱글톤 인스턴스
    public static UIManager Instance { get; private set; }

    [Header("UI 오브젝트들")]
    [SerializeField] private GameObject uiMainMenu;    // 메인 메뉴 UI 오브젝트
    [SerializeField] private GameObject uiStatus;      // 상태창 UI 오브젝트
    [SerializeField] private GameObject uiInventory;   // 인벤토리 UI 오브젝트

    // 현재 UI 상태를 저장할 변수 (FSM 방식으로 사용)
    private UIState currentState;

    // 각 UI의 스크립트 컴포넌트를 캐싱해두는 변수
    private UIMainMenu uiMainMenuScript;
    private UIStatus uiStatusScript;
    private UIInventory uiInventoryScript;

    // 외부에서 UI 스크립트에 접근할 수 있는 프로퍼티
    public UIMainMenu UIMainMenu => uiMainMenuScript;
    public UIStatus UIStatus => uiStatusScript;
    public UIInventory UIInventory => uiInventoryScript;

    private void Awake()
    {
        // 싱글톤 패턴 초기화
        if (Instance == null)
        {
            Instance = this;                    // 인스턴스 할당
        }
        else
        {
            Destroy(gameObject);               // 중복 방지
        }

        // 하위 오브젝트에서 각 UI 스크립트 컴포넌트 찾아서 캐싱
        uiMainMenuScript = uiMainMenu.GetComponent<UIMainMenu>();
        uiStatusScript = uiStatus.GetComponent<UIStatus>();
        uiInventoryScript = uiInventory.GetComponent<UIInventory>();
    }

    private void Start()
    {
        // 게임 시작 시 초기 상태는 메인 메뉴로 설정
        ShowMainMenuOnly();
    }

    // 상태를 변경하고 해당 UI를 보여주는 메서드
    public void ChangeState(UIState newState)
    {
        currentState = newState;                       // 현재 상태 갱신

        uiMainMenu.SetActive(true);                    // 메인 메뉴는 항상 켜짐

        uiStatus.SetActive(newState == UIState.Status);      // 상태창 조건부 On/Off
        uiInventory.SetActive(newState == UIState.Inventory); // 인벤토리 조건부 On/Off
    }

    // 서브 UI(Status, Inventory)를 모두 끄고 메인 메뉴만 보여주는 메서드
    public void ShowMainMenuOnly()
    {
        currentState = UIState.MainMenu;               // 상태를 메인으로 갱신

        uiMainMenu.SetActive(true);                    // 메인 메뉴 UI 켜기
        uiStatus.SetActive(false);                     // 상태창 끄기
        uiInventory.SetActive(false);                  // 인벤토리 끄기

        uiMainMenuScript.ShowMenuButtons();            // 숨겨졌던 버튼 다시 보이기
    }

    // 현재 UI 상태를 반환하는 메서드 (필요 시 외부 조회용)
    public UIState GetCurrentState()
    {
        return currentState;
    }
}
