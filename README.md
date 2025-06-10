# 인벤토리 시스템 README (2025)

이 문서는 3D 방치형 RPG 개인과제 중 **인벤토리 파트**만 떼어 내어 사용할 수 있도록 전체 구조, 폴더 배치, 핵심 스크립트 사용법을 정리한 것입니다.
![image](https://github.com/user-attachments/assets/fa699b93-2c61-4e4f-ace7-ea96ad1f1048)


![Hierarchy & Project 구조](./Docs/InventoryHierarchy.png)

---

## 📂 폴더 구조

```
Assets/
 ├─ Scenes/
 │   └─ MainScene.unity        // UI 테스트용 메인 씬
 ├─ Scripts/
 │   ├─ Character/             // 데이터 모델
 │   │   └─ Character.cs
 │   ├─ Inventory/             // 인벤토리 전용 로직
 │   │   ├─ Item.cs
 │   │   ├─ UISlot.cs
 │   │   ├─ UIInventory.cs
 │   │   └─ ItemTooltipUI.cs
 │   ├─ Managers/              // 전역 매니저 계층
 │   │   ├─ UIManager.cs
 │   │   ├─ GameManager.cs
 │   │   └─ EquipUIManager.cs
 │   └─ UI/                    // 일반 UI (메인·상태창)
 │       ├─ UIMainMenu.cs
 │       └─ UIStatus.cs
 ├─ Prefabs/
 │   ├─ UI/
 │   │   ├─ UISlot.prefab
 │   │   └─ InventoryCanvas.prefab
 │   └─ Images/None.png
 └─ Resources/
     └─ Images/Inventory/…     // 아이템 아이콘 PNG
```


---

## 🏗️ 씬 하이라키 템플릿

```
============= [SET] =============
 Main Camera
 Directional Light
 Global Volume
 EventSystem

============= [UI] ==============
 UIMainMenu      (Canvas)
 UIStatus        (Canvas)
 EquipUI         (Canvas)  // EquipUIManager가 조작
 UIInventory     (Canvas)

============= [MANAGER] =========
 UIManager       (Singleton)
 GameManager     (Singleton)
 EquipUIManager  (Singleton)
```

* **SET** : 씬 공통 세팅 오브젝트
* **UI** : 실제로 켜고 끄는 Canvas 들
* **MANAGER** : DontDestroyOnLoad 미사용, 씬마다 한 번만 존재


---

## 🔑 스크립트 개요

| 스크립트                  | 역할                                 | 주요 공개 API                                                   |
| --------------------- | ---------------------------------- | ----------------------------------------------------------- |
| **Character.cs**      | 플레이어 데이터 모델 (인벤토리·장착 관리)           | `AddItem`, `Equip`, `UnEquip`, `IsEquipped`, `TotalAttack`… |
| **Item.cs**           | 순수 데이터 오브젝트 (SO 아님)                | 생성자 인자 : `name, icon, attack, defense, …`                   |
| **UISlot.cs**         | 스크롤뷰 내 1칸 슬롯                       | `SetItem`, `SetNone`, `RefreshEquipMark`                    |
| **UIInventory.cs**    | 슬롯 동적 생성 & Back 버튼                 | `InitInventoryUI()`(파티클 없이 순수 동적 생성)                        |
| **ItemTooltipUI.cs**  | 마우스 따라다니는 능력치 툴팁                   | `Show`, `Hide`                                              |
| **EquipUIManager.cs** | 장착/해제 팝업 + 장착 로직 중계                | `Show(slot, item, isEquipped)`                              |
| **UIMainMenu.cs**     | 메인 화면 + 상태/인벤토리 버튼                 | `SetCharacter`, `Hide/ShowMenuButtons`                      |
| **UIStatus.cs**       | 장착 효과 합산 스탯 표시                     | `SetCharacter`, `RefreshStatusUI`                           |
| **UIManager.cs**      | UI FSM (Main / Status / Inventory) | `ChangeState`, `ShowMainMenuOnly`                           |
| **GameManager.cs**    | 캐릭터 & 테스트 아이템 초기화                  | `Player` 프로퍼티, `SetData()`                                  |

SOLID 원칙 적용 — 각 스크립트는 1 책임, 의존성은 Inspector `[SerializeField]`로 주입하여 GC 부담 최소화.

---

## 🚀 빠른 시작

1. **프리팹 배치**

   * `InventoryCanvas.prefab` → 씬 최상단 (하이라키 \[UI] 그룹)
   * `UISlot.prefab` → `UIInventory.slotPrefab` 필드에 연결
2. **매니저 배치**

   * `UIManager`, `GameManager`, `EquipUIManager`를 빈 GameObject에 붙여 놓습니다.
3. **아이템 아이콘 준비**

   * `Resources/Images/Inventory/` 내부에 `Sword.png`, `Shield.png` 등 파일명을 코드와 맞춤
4. **Play 실행**

   * `GameManager.SetData()` 에서 예시 아이템이 생성되며 자동 장착됨
   * `UIMainMenu` → *Inventory* 버튼 클릭 → 슬롯 9개 생성 / 툴팁·장착 UI 동작 확인

---

## 🛠️ 확장 포인트

* **ScriptableObject 사용** : `Item` 클래스를 SO로 교체하면 디자이너 친화적 데이터 테이블을 만들 수 있습니다.
* **인벤토리 용량/페이징** : `UIInventory.InitInventoryUI()` 에서 `for (int i = page*9; …)` 식으로 페이징 기능을 추가해 보세요.
* **정렬·필터** : `List<Item> inventory = player.Inventory.OrderBy(i => i.Rarity)…` 처럼 LINQ 정렬 후 슬롯 배치.
* **데이터 저장** : PlayerPrefs 또는 JSON 세이브/로드로 `Character`와 `Inventory` 상태를 직렬화할 수 있습니다.

---

## 🐞 알려진 한계 & 버그

* **툴팁 클리핑** : 화면 모서리에 슬롯이 위치하면 패널이 잘릴 수 있음 → `RectTransformUtility.ScreenPointToLocalPointInRectangle`로 피벗 보정 예정
* **씬 전환 싱글톤** : Additive 씬 로드를 사용할 경우, 두 씬에 동시에 `EquipUIManager`가 존재하지 않도록 주의

---

## 🙌 크레딧

* 개발 : **수민** (2025)
* 아이콘 : MiriCanvas.com (CC0)
