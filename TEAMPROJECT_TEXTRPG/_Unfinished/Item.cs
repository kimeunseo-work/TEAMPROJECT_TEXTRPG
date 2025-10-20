namespace TEAMPROJECT_TEXTRPG._Unfinished
{
    // Item 클래스
    internal class Item
    {
        // 주석_아이템 생성 필요하실 때 참고하세요! 아이템은 코드 가장 아랫쪽 class Items 에서 생성하시면 됩니다.

        /* 장비 Item_정의한 변수 설명
         - itemIndex        아이템 식별 번호    장비 아이템을 식별하고자 할 때 사용할 변수
         - itemName         아이템 이름         장비 아이템의 이름을 판별할 때 사용할 변수
         - itemType         아이템 타입         아이템의 유형을 판별할 때 사용할 변수
                                                (0 == X, 1 == 장비 Item, 2 == 소비 Item, 3 == 기타 Item, ...)
         - equipType        장비 타입           장비 아이템을 장착/해제할 위치를 판별할 때 사용할 변수
                                                (0 == X, 1 == 무기, 2 == 방어구, ...)
         - itemStat         아이템 스탯         장비 아이템을 장착/해제 시 영향을 주는 값을 판별할 때 사용할 변수
                                                (0 == 영향 없음, 1 == 공격력, 2 == 방어력, ...)
         - itemStatValue    아이템 스탯 값      장비 아이템을 장착/해제 시 영향을 주는 값의 수치를 정의하는 변수
         - itemExplanation  아이템 설명         장비 아이템의 설명을 정의하는 변수
         - isEquip          장비 장착 여부      장비 아이템을 장착했는지 판별할 때 사용할 변수
         */

        /* 소비 Item_정의한 변수 설명
         - itemIndex        아이템 식별 번호    소비 아이템을 식별하고자 할 때 사용할 변수
         - itemName         아이템 이름         소비 아이템의 이름을 판별할 때 사용할 변수
         - itemType         아이템 타입         아이템의 유형을 판별할 때 사용할 변수
                                                (0 == null, 1 == 장비 Item, 2 == 소비 Item, 3 == 기타 Item, ...)
         - itemStat         아이템 스탯         소비 아이템을 사용 시 영향을 주는 값을 판별할 때 사용할 변수
                                                (0 == 사용 불가, 1 == 체력, 2 == 마나, ...)
         - itemStatValue    아이템 스탯 값      소비 아이템을 사용 시 영향을 주는 값의 수치를 정의하는 변수
         - itemExplanation  아이템 설명         소비 아이템의 설명을 정의하는 변수
         */

        /* 기타 Item_정의한 변수 설명
         - itemIndex        아이템 식별 번호    기타 아이템을 식별하고자 할 때 사용할 변수
         - itemName         아이템 이름         기타 아이템의 이름을 판별할 때 사용할 변수
         - itemType         아이템 타입         아이템의 유형을 판별할 때 사용할 변수
                                                (0 == null, 1 == 장비 Item, 2 == 소비 Item, 3 == 기타 Item, ...)
         - itemExplanation  아이템 설명         기타 아이템의 설명을 정의하는 변수
         */

        // Item 변수 정의
        public int itemIndex = 0;
        public string itemName = "";
        public int itemType = 0;
        public int equipType = 0;
        public int itemStat = 0;
        public int itemStatValue = 0;
        public string itemExplanation = "";
        public bool isEquip = false;
    }

    // 장비 Item 클래스 (Item)
    internal class EquipItem : Item
    {
        // 장비 Item 생성에 참고할 메서드 생성
        public EquipItem(int itemIndex, string itemName, int itemType, int equipType, int itemStat, int itemStatValue, string itemExplanation, bool isEquip)
        {
            this.itemIndex = itemIndex;
            this.itemName = itemName;
            this.itemType = itemType;
            this.equipType = equipType;
            this.itemStat = itemStat;
            this.itemStatValue = itemStatValue;
            this.itemExplanation = itemExplanation;
            this.isEquip = isEquip;
        }

        // 장비 Item 복제에 참고할 메서드 생성
        public EquipItem(Item clone)
        {
            this.itemIndex = clone.itemIndex;
            this.itemName = clone.itemName;
            this.itemType = clone.itemType;
            this.equipType= clone.equipType;
            this.itemStat = clone.itemStat;
            this.itemStatValue = clone.itemStatValue;
            this.itemExplanation = clone.itemExplanation;
            this.isEquip = clone.isEquip;
        }
    }

    // 소비 Item 클래스 (Item)
    internal class UseItem : Item
    {
        // 소비 Item 생성에 참고할 메서드 생성
        public UseItem(int itemIndex, string itemName, int itemType, int itemStat, int itemStatValue, string itemExplanation)
        {
            this.itemIndex = itemIndex;
            this.itemName = itemName;
            this.itemType = itemType;
            this.itemStat = itemStat;
            this.itemStatValue = itemStatValue;
            this.itemExplanation = itemExplanation;
        }

        // 소비 Item 복제에 참고할 메서드 생성
        public UseItem(Item clone)
        {
            this.itemIndex = clone.itemIndex;
            this.itemName = clone.itemName;
            this.itemType = clone.itemType;
            this.itemStat = clone.itemStat;
            this.itemStatValue = clone.itemStatValue;
            this.itemExplanation = clone.itemExplanation;
        }
    }

    // 기타 Item 클래스 (Item)
    internal class EtcItem : Item
    {
        // 기타 Item 생성에 참고할 메서드 생성
        public EtcItem(int itemIndex, string itemName, int itemType, string itemExplanation)
        {
            this.itemIndex = itemIndex;
            this.itemName = itemName;
            this.itemType = itemType;
            this.itemExplanation = itemExplanation;
        }

        // 기타 Item 복제에 참고할 메서드 생성
        public EtcItem(Item clone)
        {
            this.itemIndex = clone.itemIndex;
            this.itemName = clone.itemName;
            this.itemExplanation = clone.itemExplanation;
        }
    }

    // 게임에서 사용할 Items 클래스
    internal class Items
    {
        // 장비 Item 생성
        public EquipItem equipItem0 = new EquipItem(0, "", 1, 0, 0, 0, "(비어있음)", false); // 기본 값
        public EquipItem equipItem1 = new EquipItem(1, "낡은 검", 1, 1, 1, 5, "낡은 검이다. 당근에서 싸게 거래했다.", false);
        public EquipItem equipItem2 = new EquipItem(2, "냄비 뚜껑", 1, 2, 2, 2, "냄비 뚜껑이다. 붉은 얼룩이 묻어있다.", false);

        // 소비 Item 생성
        public UseItem useItem0 = new UseItem(0, "", 2, 0, 0, "(비어있음)"); // 기본 값
        public UseItem useItem1 = new UseItem(1, "체력 회복 포션", 2, 1, 30, "체력을 30 회복할 수 있는 포션이다.");
        public UseItem useItem2 = new UseItem(2, "마나 회복 포션", 2, 2, 20, "마나를 20 회복할 수 있는 포션이다.");

        // 기타 Item 생성
        public EtcItem etcItem0 = new EtcItem(0, "", 3, "(비어있음)"); // 기본 값
        public EtcItem etcItem1 = new EtcItem(1, "작은 철 조각", 3, "'미니언'의 잔해이다. 미니언이 휘두르던 무기의 파편인 것 같다."); // '미니언'이 드랍할 전리품
        public EtcItem etcItem2 = new EtcItem(2, "빛나는 조각", 3, "'공허충'의 잔해이다. 기묘한 보라빛으로 빛나고 있다."); // '공허충'이 드랍할 전리품
        public EtcItem etcItem3 = new EtcItem(3, "무언가의 손잡이", 3, "'대포 미니언'의 잔해이다. 미니언이 몰던 대포의 조종간인 것 같다."); // '대포 미니언'이 드랍할 전리품
        public EtcItem etcItem4 = new EtcItem(4, "작은 돌 조각", 3, "'돌거북'의 잔해이다. 왠지 미안해진다.."); // '돌거북'이 드랍할 전리품
        public EtcItem etcItem5 = new EtcItem(5, "돌 조각", 3, "'고대 돌거북'의 잔해이다. 돌도끼로 사용해도 될 정도로 단단하다."); // '고대 돌거북'이 드랍할 전리품
        public EtcItem etcItem6 = new EtcItem(6, "푸르스름한 깃털", 3, "'칼날부리'의 잔해이다. 생각보다 부드럽고 연하다."); // '칼날부리'가 드랍할 전리품
    }

    // Player가 사용할 인벤토리 클래스
    internal class Inventory
    {
        // Player 인벤토리로 사용할 inventory 리스트 생성
        public List<Item> inventory = new List<Item>();

        // 게임에서 사용할 inventory 목록 생성
        public Inventory()
        {
            // inventory 내 아이템 추가_낡은 검
            inventory.Add(new Items().equipItem1);
        }
    }

    // Monster 드랍 아이템 클래스
    internal class Drops
    {
        // Monster가 드랍할 DropItems 리스트 생성
        public List<Item> mon1Drops = new List<Item>();
        public List<Item> mon2Drops = new List<Item>();
        public List<Item> mon3Drops = new List<Item>();
        public List<Item> mon4Drops = new List<Item>();
        public List<Item> mon5Drops = new List<Item>();
        public List<Item> mon6Drops = new List<Item>();
    
        // 게임에서 사용할 DropItems 목록 생성
        public Drops()
        {            
            // DropItems 리스트에 Monster 별 드랍할 아이템 삽입
            // '미니언' 드랍 아이템 추가
            mon1Drops.Add(new Items().useItem1); // 체력 포션
            mon1Drops.Add(new Items().etcItem1); // 작은 철 조각

            // '공허충' 드랍 아이템 추가
            mon2Drops.Add(new Items().etcItem2); // 빛나는 조각

            // '대포 미니언' 드랍 아이템 추가
            mon3Drops.Add(new Items().equipItem2); // 냄비 뚜껑
            mon3Drops.Add(new Items().etcItem3); // 무언가의 손잡이

            // '돌거북' 드랍 아이템 추가
            mon4Drops.Add(new Items().etcItem4); // 작은 돌 조각

            // '고대 돌거북' 드랍 아이템 추가
            mon5Drops.Add(new Items().equipItem1); // 낡은 검
            mon5Drops.Add(new Items().equipItem2); // 냄비 뚜껑
            mon5Drops.Add(new Items().etcItem5); // 돌 조각

            // '칼날부리' 드랍 아이템 추가
            mon6Drops.Add(new Items().useItem1); // 체력 포션
            mon6Drops.Add(new Items().useItem2); // 마나 포션
            mon6Drops.Add(new Items().etcItem6); // 푸르스름한 깃털
        }
    }
}