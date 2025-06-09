using System.Collections.Generic;
using System.Linq; // 리스트 합산에 필요한 Linq 사용

// 플레이어 정보를 담는 데이터 모델 클래스
public class Character
{
    public string Name { get; private set; }         // 플레이어 이름
    public int Level { get; private set; }           // 현재 레벨
    public int Exp { get; private set; }             // 현재 경험치
    public int MaxExp { get; private set; }          // 최대 경험치
    public int Attack { get; private set; }          // 기본 공격력
    public int Defense { get; private set; }         // 기본 방어력
    public int Health { get; private set; }          // 기본 체력
    public int Critical { get; private set; }        // 기본 치명타 확률
    public int Gold { get; private set; }            // 현재 보유한 골드

    public List<Item> Inventory { get; private set; } // 인벤토리에 있는 아이템 리스트

    // 장착된 아이템들 분류별로 관리
    private Item weaponItem;                         // 무기 아이템 (검, 망치, 활, 마법책)
    private Item shieldItem;                         // 방패 아이템
    private List<Item> accessoryItems;               // 장신구 리스트 (반지, 투구 등)

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
                weaponItem = item; // 무기 장착
                break;
            case "방패":
                shieldItem = item; // 방패 장착
                break;
            case "반지":
            case "투구":
                if (!accessoryItems.Contains(item))
                    accessoryItems.Add(item); // 장신구 장착
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

    // 현재 장착 중인 장신구 리스트 반환
    public List<Item> GetEquippedAccessories()
    {
        return accessoryItems;
    }

    // 장착 아이템을 모두 하나의 리스트로 반환 (상태창 표시용)
    public List<Item> GetAllEquippedItems()
    {
        List<Item> result = new List<Item>();
        if (weaponItem != null) result.Add(weaponItem);
        if (shieldItem != null) result.Add(shieldItem);
        result.AddRange(accessoryItems);
        return result;
    }

    // 장착된 아이템의 보너스 능력치를 모두 합산하여 반환
    public int TotalAttack => Attack + GetAllEquippedItems().Sum(i => i.Attack);
    public int TotalDefense => Defense + GetAllEquippedItems().Sum(i => i.Defense);
    public int TotalHealth => Health + GetAllEquippedItems().Sum(i => i.Health);
    public int TotalCritical => Critical + GetAllEquippedItems().Sum(i => i.Critical);
}
