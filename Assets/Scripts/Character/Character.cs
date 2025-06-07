using UnityEngine;

// 플레이어 정보를 담는 데이터 모델 클래스
public class Character
{
    public string Name { get; private set; }         // 플레이어 이름
    public int Level { get; private set; }           // 레벨
    public int Exp { get; private set; }             // 현재 경험치
    public int MaxExp { get; private set; }          // 최대 경험치
    public int Attack { get; private set; }          // 공격력
    public int Defense { get; private set; }         // 방어력
    public int Health { get; private set; }          // 체력
    public int Critical { get; private set; }        // 치명타 확률

    // 생성자에서 모든 데이터를 초기화
    public Character(string name, int level, int exp, int maxExp, int attack, int defense, int health, int critical)
    {
        Name = name;
        Level = level;
        Exp = exp;
        MaxExp = maxExp;
        Attack = attack;
        Defense = defense;
        Health = health;
        Critical = critical;
    }
}
