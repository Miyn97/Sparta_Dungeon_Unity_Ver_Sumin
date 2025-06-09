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

    // 장착된 아이템들 분류별로 관리
    private Item weaponItem;     // 검, 활, 망치, 마법책
    private Item shieldItem;     // 방패
    private List<Item> accessoryItems; // 반지, 투구 등

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

        Inventory = new List<Item>();             // 인벤토리 리스트 초기화
        accessoryItems = new List<Item>();        // 장신구 리스트 초기화
    }

    // 아이템을 인벤토리에 추가하는 메서드
    public void AddItem(Item item)
    {
        Inventory.Add(item);
    }

    // 아이템을 장착하는 메서드 (조건은 EquipUIManager에서 체크)
    public void Equip(Item item)
    {
        if (!Inventory.Contains(item)) return;

        switch (item.Name)
        {
            case "검":
            case "망치":
            case "활":
            case "마법책":
                weaponItem = item;
                break;
            case "방패":
                shieldItem = item;
                break;
            case "반지":
            case "투구":
                if (!accessoryItems.Contains(item))
                    accessoryItems.Add(item);
                break;
        }
    }

    // 아이템 장착 해제
    public void UnEquip(Item item)
    {
        if (item == weaponItem)
            weaponItem = null;
        else if (item == shieldItem)
            shieldItem = null;
        else if (accessoryItems.Contains(item))
            accessoryItems.Remove(item);
    }

    // 해당 아이템이 현재 장착 중인지 확인
    public bool IsEquipped(Item item)
    {
        return item == weaponItem || item == shieldItem || accessoryItems.Contains(item);
    }

    // 무기 장착 여부 확인
    public bool HasEquippedWeapon()
    {
        return weaponItem != null;
    }

    // 현재 장착 중인 무기 반환
    public Item GetEquippedWeapon()
    {
        return weaponItem;
    }

    // 현재 장착 중인 방패 반환
    public Item GetEquippedShield()
    {
        return shieldItem;
    }

}
