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
        player = new Character("코딩노예", 10, 9, 12, 35, 40, 100, 25, 20000);

        // 아이템 스프라이트 로드
        Sprite swordIcon = Resources.Load<Sprite>("Images/Inventory/Sword");
        Sprite shieldIcon = Resources.Load<Sprite>("Images/Inventory/Shield");
        Sprite hammerIcon = Resources.Load<Sprite>("Images/Inventory/Hammer");
        Sprite bowIcon = Resources.Load<Sprite>("Images/Inventory/Bow");
        Sprite magicbookIcon = Resources.Load<Sprite>("Images/Inventory/MagicBook");
        Sprite helmetIcon = Resources.Load<Sprite>("Images/Inventory/Helmet");
        Sprite ringIcon = Resources.Load<Sprite>("Images/Inventory/Ring");
        Sprite noneIcon = Resources.Load<Sprite>("Images/Inventory/None");

        // 아이템 객체 생성
        Item sword = new Item("검", swordIcon);
        Item shield = new Item("방패", shieldIcon);
        Item hammer = new Item("망치", hammerIcon);
        Item bow = new Item("활", bowIcon);
        Item magicbook = new Item("마법책", magicbookIcon);
        Item helmet = new Item("투구", helmetIcon);
        Item ring = new Item("반지", ringIcon);
        Item none = new Item("없음", noneIcon); // 빈 슬롯용 아이템

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
