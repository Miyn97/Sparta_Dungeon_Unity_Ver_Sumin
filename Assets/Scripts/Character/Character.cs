using System.Collections.Generic;

// 플레이어 정보를 담는 데이터 모델 클래스
public class Character
{
    public string Name { get; private set; }         // 플레이어 이름
    public int Level { get; private set; }           // 현재 레벨
    public int Exp { get; private set; }             // 현재 경험치
    public int MaxExp { get; private set; }          // 최대 경험치
    public int Attack { get; private set; }          // 공격력
    public int Defense { get; private set; }         // 방어력
    public int Health { get; private set; }          // 체력
    public int Critical { get; private set; }        // 치명타 확률
    public int Gold { get; private set; }            // 현재 보유한 골드

    public List<Item> Inventory { get; private set; }   // 인벤토리에 있는 아이템 리스트
    private Item equippedItem;                          // 현재 장착된 아이템

    // 생성자를 통해 모든 플레이어 정보를 초기화
    public Character(string name, int level, int exp, int maxExp, int attack, int defense, int health, int critical, int gold)
    {
        Name = name;
        Level = level;
        Exp = exp;
        MaxExp = maxExp;
        Attack = attack;
        Defense = defense;
        Health = health;
        Critical = critical;
        Gold = gold;

        Inventory = new List<Item>();                   // 인벤토리 리스트 초기화
    }

    // 아이템을 인벤토리에 추가하는 메서드
    public void AddItem(Item item)
    {
        Inventory.Add(item);
    }

    // 아이템을 장착하는 메서드
    public void Equip(Item item)
    {
        // 인벤토리에 존재하지 않는 아이템은 장착 불가
        if (!Inventory.Contains(item)) return;

        equippedItem = item;                            // 해당 아이템을 장착
    }

    // 아이템이 현재 장착된 상태인지 확인하는 메서드
    public bool IsEquipped(Item item)
    {
        return equippedItem == item;
    }
}
