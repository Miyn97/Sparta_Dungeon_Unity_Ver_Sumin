using UnityEngine;

// 게임의 흐름을 총괄하는 매니저 클래스
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }    // 싱글톤 인스턴스

    private Character player;                                   // 플레이어 캐릭터 정보

    public Character Player => player;                          // 외부에서 접근할 수 있는 프로퍼티

    private void Awake()
    {
        // 싱글톤 패턴 처리
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        SetData(); // 게임 시작 시 플레이어 및 아이템 정보 초기화
    }

    private void SetData()
    {
        // 캐릭터 생성 (이름, 레벨, 경험치, 최대경험치, 공격력, 방어력, 체력, 치명타, 골드)
        player = new Character("코딩노예", 10, 9, 12, 20, 40, 100, 20, 20000);

        // 아이템 스프라이트 로드
        Sprite swordIcon = Resources.Load<Sprite>("Images/Inventory/Sword");
        Sprite shieldIcon = Resources.Load<Sprite>("Images/Inventory/Shield");
        Sprite hammerIcon = Resources.Load<Sprite>("Images/Inventory/Hammer");
        Sprite bowIcon = Resources.Load<Sprite>("Images/Inventory/Bow");
        Sprite magicbookIcon = Resources.Load<Sprite>("Images/Inventory/MagicBook");
        Sprite helmetIcon = Resources.Load<Sprite>("Images/Inventory/Helmet");
        Sprite ringIcon = Resources.Load<Sprite>("Images/Inventory/Ring");
        Sprite noneIcon = Resources.Load<Sprite>("Images/Inventory/None");

        // 아이템 객체 생성 (능력치 포함)
        Item sword = new Item("검", swordIcon, 15, 0, 0, 5);                      // 공격력 15, 치명타 5
        Item shield = new Item("방패", shieldIcon, 0, 10, 5, 0);                  // 방어력 10, 체력 5
        Item hammer = new Item("망치", hammerIcon, 20, 0, 0, 10);                 //공격력 20, 치명타 10
        Item bow = new Item("활", bowIcon, 10, 0, 0, 10);                         //공격력 10, 치명타 10
        Item magicbook = new Item("마법책", magicbookIcon, 5, 0, 0, 15);           //공격력 5, 치명타 15
        Item helmet = new Item("투구", helmetIcon, 0, 10, 5, 0);                  //방어력 10, 체력 5
        Item ring = new Item("반지", ringIcon, 0, 0, 20, 5);                      //체력 20, 치명타 5
        Item none = new Item("없음", noneIcon);                                   //빈 슬롯용

        // 플레이어 인벤토리에 아이템 추가
        player.AddItem(sword);
        player.AddItem(shield);
        player.AddItem(hammer);
        player.AddItem(bow);
        player.AddItem(magicbook);
        player.AddItem(helmet);
        player.AddItem(ring);
        player.AddItem(none);

        // 검 아이템 장착
        player.Equip(sword);

        // UI 매니저를 통해 캐릭터 정보 전달
        UIManager.Instance.UIMainMenu.SetCharacter(player);
        UIManager.Instance.UIStatus.SetCharacter(player);

        // 인벤토리 초기화 호출
        UIManager.Instance.UIInventory.InitInventoryUI();
    }
}
