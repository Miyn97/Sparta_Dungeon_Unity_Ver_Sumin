using UnityEngine;

// 아이템 정보를 저장하는 클래스
public class Item
{
    public string Name { get; private set; }     // 아이템 이름
    public Sprite Icon { get; private set; }     // 아이템 아이콘 이미지

    // 생성자를 통해 이름과 아이콘을 설정
    public Item(string name, Sprite icon)
    {
        Name = name;
        Icon = icon;
    }
}
