namespace TEAMPROJECT_TEXTRPG
{
    internal class Item
    {
        // 기타 Item에 할당할 변수 정의

        /* 정의한 변수 설명
         - itemNumber       아이템 넘버      아이템을 식별하고자 할 때 사용할 변수
         - itemName         아이템 이름      아이템의 이름을 판별할 때 사용할 변수
         - itemExplanation  아이템 설명      아이템의 설명을 정의하는 변수
         */
        public int itemNumber = 0;
        public string itemName = "";
        public string itemExplanation = "";

        // Item 생성에 참고할 메서드 생성
        public Item(int itemNumber, string itemName, string itemExplanation)
        {
            this.itemNumber = itemNumber;
            this.itemName = itemName;
            this.itemExplanation = itemExplanation;
        }
    }

    internal class EquipItem
    {
        // 장비 Item에 할당할 변수 정의

        /* 정의한 변수 설명
         - itemNumber       아이템 넘버      장비 아이템을 식별하고자 할 때 사용할 변수
         - itemName         아이템 이름      장비 아이템의 이름을 판별할 때 사용할 변수
         - itemType         아이템 타입      장비 아이템을 장착/해제할 위치를 판별할 때 사용할 변수
                                             (0 == 장비 X, 1 == 무기, 2 == 방어구, ...)
         - itemStat         아이템 스탯      장비 아이템을 장착/해제 시 영향을 주는 값을 판별할 때 사용할 변수
                                             (0 == 영향 없음, 1 == 공격력, 2 == 방어력, ...)
         - itemStatValue    아이템 스탯 값   장비 아이템을 장착/해제 시 영향을 주는 값의 수치를 정의하는 변수
         - itemExplanation  아이템 설명      장비 아이템의 설명을 정의하는 변수
         - isEquip          장비 장착 여부   장비 아이템을 장착했는지 판별할 때 사용할 변수
         */
        public int itemNumber = 0;
        public string itemName = "";
        public int itemType = 0;
        public int itemStat = 0;
        public int itemStatValue = 0;
        public string itemExplanation = "";
        public bool isEquip = false;

        // Item 생성에 참고할 메서드 생성
        public EquipItem(int itemNumber, string itemName, int itemType, int itemStat, int itemStatValue, string itemExplanation, bool isEquip)
        {
            this.itemNumber = itemNumber;
            this.itemName = itemName;
            this.itemType = itemType;
            this.itemStat = itemStat;
            this.itemStatValue = itemStatValue;
            this.itemExplanation = itemExplanation;
            this.isEquip = isEquip;
        }
    }

    internal class UseItem
    {
        // 소비 Item에 할당할 변수 정의

        /* 정의한 변수 설명
         - itemNumber       아이템 넘버      소비 아이템을 식별하고자 할 때 사용할 변수
         - itemName         아이템 이름      소비 아이템의 이름을 판별할 때 사용할 변수
         - itemStat         아이템 스탯      소비 아이템을 사용 시 영향을 주는 값을 판별할 때 사용할 변수
                                             (0 == 사용 불가, 1 == 체력, 2 == 마나, ...)
         - itemStatValue    아이템 스탯 값   소비 아이템을 사용 시 영향을 주는 값의 수치를 정의하는 변수
         - itemExplanation  아이템 설명      소비 아이템의 설명을 정의하는 변수
         */
        public int itemNumber = 0;
        public string itemName = "";
        public int itemStat = 0;
        public int itemStatValue = 0;
        public string itemExplanation = "";

        // Item 생성에 참고할 메서드 생성
        public UseItem(int itemNumber, string itemName, int itemStat, int itemStatValue, string itemExplanation)
        {
            this.itemNumber = itemNumber;
            this.itemName = itemName;
            this.itemStat = itemStat;
            this.itemStatValue = itemStatValue;
            this.itemExplanation = itemExplanation;
        }
    }

    internal class Items
    {
        // 새롭게 생성한 기타 Item
        Item itemDefault0 = new Item(0, "", ""); // 기본 값
        Item item1 = new Item(1, "전리품 A", ""); // '미니언'이 드랍할 전리품
        Item item2 = new Item(2, "전리품 B", ""); // '공허충'이 드랍할 전리품
        Item item3 = new Item(3, "전리품 C", ""); // '대포 미니언'이 드랍할 전리품
        Item item4 = new Item(4, "전리품 D", ""); // '돌거북'이 드랍할 전리품
        Item item5 = new Item(5, "전리품 E", ""); // '고대 돌거북'이 드랍할 전리품
        Item item6 = new Item(6, "전리품 F", ""); // '칼날부리'가 드랍할 전리품

        // 새롭게 생성한 장비 Item
        EquipItem equipItemDefault0 = new EquipItem(0, "", 0, 0, 0, "", false); // 기본 값
        EquipItem equipItem1 = new EquipItem(1, "낡은 검", 1, 1, 5, "", false);
        EquipItem equipItem2 = new EquipItem(2, "냄비 뚜껑", 2, 2, 2, "", false);

        // 새롭게 생성한 소비 Item
        UseItem useItemDefault0 = new UseItem(0, "", 0, 0, ""); // 기본 값
        UseItem useItem0 = new UseItem(1, "체력 회복 포션", 1, 30, "");
        UseItem useItem1 = new UseItem(2, "마나 회복 포션", 2, 20, "");
    }
}
