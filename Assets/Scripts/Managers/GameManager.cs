using UnityEngine;

// 게임의 흐름을 총괄하는 매니저 클래스
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }   // 싱글톤 인스턴스

    private Character player;  // 플레이어 캐릭터 정보

    public Character Player => player; // 외부 접근을 위한 프로퍼티

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        SetData(); // 게임 시작 시 캐릭터 데이터 세팅
    }

    private void SetData()
    {
        // 캐릭터 데이터 생성 (이름, 레벨, 경험치, 공격, 방어, 체력, 치명타, 보유 골드)
        player = new Character("코딩노예", 10, 9, 12, 35, 40, 100, 25, 20000);

        // UIManager의 연결된 UI스크립트들을 통해 캐릭터 정보 전달
        UIManager.Instance.UIMainMenu.SetCharacter(player);
        UIManager.Instance.UIStatus.SetCharacter(player);
    }
}
