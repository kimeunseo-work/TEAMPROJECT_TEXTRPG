namespace TEAMPROJECT_TEXTRPG
{
    // Item 클래스
    internal class Item
    {
        // 주석_아이템 생성 필요하실 때 참고하세요! 아이템은 코드 가장 아랫쪽 class Items 에서 생성하시면 됩니다.

        /* 장비 Item_정의한 변수 설명
         - itemNumber       아이템 번호      장비 아이템을 식별하고자 할 때 사용할 변수
         - itemName         아이템 이름      장비 아이템의 이름을 판별할 때 사용할 변수
         - itemType         아이템 타입      장비 아이템을 장착/해제할 위치를 판별할 때 사용할 변수
                                             (0 == 장비 X, 1 == 무기, 2 == 방어구, ...)
         - itemStat         아이템 스탯      장비 아이템을 장착/해제 시 영향을 주는 값을 판별할 때 사용할 변수
                                             (0 == 영향 없음, 1 == 공격력, 2 == 방어력, ...)
         - itemStatValue    아이템 스탯 값   장비 아이템을 장착/해제 시 영향을 주는 값의 수치를 정의하는 변수
         - itemExplanation  아이템 설명      장비 아이템의 설명을 정의하는 변수
         - isEquip          장비 장착 여부   장비 아이템을 장착했는지 판별할 때 사용할 변수
         */

        /* 소비 Item_정의한 변수 설명
         - itemNumber       아이템 번호      소비 아이템을 식별하고자 할 때 사용할 변수
         - itemName         아이템 이름      소비 아이템의 이름을 판별할 때 사용할 변수
         - itemStat         아이템 스탯      소비 아이템을 사용 시 영향을 주는 값을 판별할 때 사용할 변수
                                             (0 == 사용 불가, 1 == 체력, 2 == 마나, ...)
         - itemStatValue    아이템 스탯 값   소비 아이템을 사용 시 영향을 주는 값의 수치를 정의하는 변수
         - itemExplanation  아이템 설명      소비 아이템의 설명을 정의하는 변수
         */

        /* 기타 Item_정의한 변수 설명
         - itemNumber       아이템 번호      기타 아이템을 식별하고자 할 때 사용할 변수
         - itemName         아이템 이름      기타 아이템의 이름을 판별할 때 사용할 변수
         - itemExplanation  아이템 설명      기타 아이템의 설명을 정의하는 변수
         */

        // Item 변수 정의
        public int itemNumber = 0;
        public string itemName = "";
        public int itemType = 0;
        public int itemStat = 0;
        public int itemStatValue = 0;
        public string itemExplanation = "";
        public bool isEquip = false;
    }

    // 장비 Item 클래스 (Item)
    internal class EquipItem : Item
    {
        // 장비 Item 생성에 참고할 메서드 생성
        public EquipItem(string itemName, int itemType, int itemStat, int itemStatValue, string itemExplanation, bool isEquip)
        {
            this.itemName = itemName;
            this.itemType = itemType;
            this.itemStat = itemStat;
            this.itemStatValue = itemStatValue;
            this.itemExplanation = itemExplanation;
            this.isEquip = isEquip;
        }

        // 장비 Item 복제에 참고할 메서드 생성
        public EquipItem(Item clone)
        {
            this.itemName = clone.itemName;
            this.itemType = clone.itemType;
            this.itemStat = clone.itemStat;
            this.itemStatValue = clone.itemStatValue;
            this.itemExplanation = clone.itemExplanation;
            this.isEquip = clone.isEquip;
        }
    }

    // 소비 Item 클래스
    internal class UseItem : Item
    {
        // 소비 Item 생성에 참고할 메서드 생성
        public UseItem(string itemName, int itemStat, int itemStatValue, string itemExplanation)
        {
            this.itemName = itemName;
            this.itemStat = itemStat;
            this.itemStatValue = itemStatValue;
            this.itemExplanation = itemExplanation;
        }

        // 소비 Item 복제에 참고할 메서드 생성
        public UseItem(Item clone)
        {
            this.itemName = clone.itemName;
            this.itemStat = clone.itemStat;
            this.itemStatValue = clone.itemStatValue;
            this.itemExplanation = clone.itemExplanation;
        }
    }

    // 기타 Item 클래스
    internal class EtcItem : Item
    {
        // 기타 Item 생성에 참고할 메서드 생성
        public EtcItem(string itemName, string itemExplanation)
        {
            this.itemName = itemName;
            this.itemExplanation = itemExplanation;
        }

        // 기타 Item 복제에 참고할 메서드 생성
        public EtcItem(Item clone)
        {
            this.itemName = clone.itemName;
            this.itemExplanation = clone.itemExplanation;
        }
    }

    // 게임에서 사용할 Items
    internal class Items
    {
        // 아이템 유형 별 Item 리스트 생성
        public List<Item> equipItems = new List<Item>(); // 장비 Item 리스트
        public List<Item> useItems = new List<Item>(); // 소비 Item 리스트
        public List<Item> etcItems = new List<Item>(); // 기타 Item 리스트

        // 게임에서 사용할 Items 목록(설계도 ?) 생성
        public Items()
        {
            // 장비 Item 리스트에 장비 Items 생성
            equipItems.Add(new EquipItem("", 0, 0, 0, "(비어있음)", false)); // 기본 값
            equipItems.Add(new EquipItem("낡은 검", 1, 1, 5, "낡은 검이다. 당근에서 싸게 거래했다.", false));
            equipItems.Add(new EquipItem("냄비 뚜껑", 2, 2, 2, "냄비 뚜껑이다. 붉은 얼룩이 묻어있다.", false));

            // 소비 Item 리스트에 소비 Items 생성
            useItems.Add(new UseItem("", 0, 0, "(비어있음)")); // 기본 값
            useItems.Add(new UseItem("체력 회복 포션", 1, 30, "체력을 30 회복할 수 있는 포션이다."));
            useItems.Add(new UseItem("마나 회복 포션", 2, 20, "마나를 20 회복할 수 있는 포션이다."));

            // 기타 Item 리스트에 기타 Items 생성
            etcItems.Add(new EtcItem("", "(비어있음)")); // 기본 값
            etcItems.Add(new EtcItem("작은 철 조각", "'미니언'의 잔해이다. 미니언이 휘두르던 무기의 파편인 것 같다.")); // '미니언'이 드랍할 전리품
            etcItems.Add(new EtcItem("빛나는 조각", "'공허충'의 잔해이다. 기묘한 보라빛으로 빛나고 있다.")); // '공허충'이 드랍할 전리품
            etcItems.Add(new EtcItem("무언가의 손잡이", "'대포 미니언'의 잔해이다. 미니언이 몰던 대포의 조종간인 것 같다.")); // '대포 미니언'이 드랍할 전리품
            etcItems.Add(new EtcItem("작은 돌 조각", "'돌거북'의 잔해이다. 왠지 미안해진다..")); // '돌거북'이 드랍할 전리품
            etcItems.Add(new EtcItem("돌 조각", "'고대 돌거북'의 잔해이다. 돌도끼로 사용해도 될 정도로 단단하다.")); // '고대 돌거북'이 드랍할 전리품
            etcItems.Add(new EtcItem("푸르스름한 깃털", "'칼날부리'의 잔해이다. 생각보다 부드럽고 연하다.")); // '칼날부리'가 드랍할 전리품
        }
    }

    // Player 인벤토리  클래스
    internal class Inven : Items
    {
        // Player 인벤토리로 사용할 inven 리스트 생성
        public List<Item> inven = new List<Item>();
    }

    internal class Drops : Items
    {
        // Monster가 드랍할 DropItems 리스트 생성
        public List<Item> monster1Drops = new List<Item>();
        public List<Item> monster2Drops = new List<Item>();
        public List<Item> monster3Drops = new List<Item>();
        public List<Item> monster4Drops = new List<Item>();
        public List<Item> monster5Drops = new List<Item>();
        public List<Item> monster6Drops = new List<Item>();

        public Drops()
        {
            // Item 리스트에서 Item 복제
            EquipItem equip1 = new EquipItem(equipItems[1]); // 낡은 검
            EquipItem equip2 = new EquipItem(equipItems[2]); // 냄비 뚜껑

            UseItem use1 = new UseItem(useItems[1]); // 체력 포션
            UseItem use2 = new UseItem(useItems[2]); // 마나 포션

            EtcItem etc1 = new EtcItem(etcItems[1]); // '미니언' 드랍 아이템
            EtcItem etc2 = new EtcItem(etcItems[2]); // '공허충' 드랍 아이템
            EtcItem etc3 = new EtcItem(etcItems[3]); // '대포 미니언' 드랍 아이템
            EtcItem etc4 = new EtcItem(etcItems[4]); // '돌거북' 드랍 아이템
            EtcItem etc5 = new EtcItem(etcItems[5]); // '고대 돌거북' 드랍 아이템
            EtcItem etc6 = new EtcItem(etcItems[6]); // '칼날부리' 드랍 아이템

            // DropItems 리스트에 Monster 별 드랍할 아이템 삽입
            // '미니언' 드랍 아이템 추가
            monster1Drops.Add(use1); // 체력 포션
            monster1Drops.Add(etc1); // 작은 철 조각

            // '공허충' 드랍 아이템 추가
            monster2Drops.Add(etc2); // 빛나는 조각

            // '대포 미니언' 드랍 아이템 추가
            monster3Drops.Add(equip2); // 냄비 뚜껑
            monster3Drops.Add(etc3); // 무언가의 손잡이

            // '돌거북' 드랍 아이템 추가
            monster4Drops.Add(etc4); // 작은 돌 조각

            // '고대 돌거북' 드랍 아이템 추가
            monster5Drops.Add(equip1); // 낡은 검
            monster5Drops.Add(equip2); // 냄비 뚜껑
            monster5Drops.Add(etc5); // 돌 조각

            // '칼날부리' 드랍 아이템 추가
            monster6Drops.Add(use1); // 체력 포션
            monster6Drops.Add(use2); // 마나 포션
            monster6Drops.Add(etc6); // 푸르스름한 깃털
        }
    }
}
