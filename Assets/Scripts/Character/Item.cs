using UnityEngine;

// 아이템 정보를 저장하는 클래스
public class Item
{
    public string Name { get; private set; }     // 아이템 이름
    public Sprite Icon { get; private set; }     // 아이템 아이콘 이미지

    // 능력치 보너스 정보
    public int Attack { get; private set; }      // 공격력 증가량
    public int Defense { get; private set; }     // 방어력 증가량
    public int Health { get; private set; }      // 체력 증가량
    public int Critical { get; private set; }    // 치명타 증가량

    // 생성자를 통해 이름, 아이콘, 능력치 설정
    public Item(string name, Sprite icon, int attack = 0, int defense = 0, int health = 0, int critical = 0)
    {
        Name = name;
        Icon = icon;
        Attack = attack;
        Defense = defense;
        Health = health;
        Critical = critical;
    }
}
