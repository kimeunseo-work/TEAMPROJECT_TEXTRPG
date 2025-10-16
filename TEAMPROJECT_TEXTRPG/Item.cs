namespace TEAMPROJECT_TEXTRPG
{
    internal class Item
    {
        // Item에 할당할 변수 정의

        /* 정의한 변수 설명
         - itemNumber       아이템 넘버      아이템을 식별하고자 할 때 사용할 변수
         - itemName         아이템 이름      아이템의 이름을 판별할 때 사용할 변수
         - itemType         아이템 타입      어떤 아이템인지 판별할 때 사용할 변수
                                             (0 == 기타, 1 == 장비, 2 == 소비, ...)
         - equipType        장비 타입        장비를 장착/해제할 위치를 판별할 때 사용할 변수
                                             (0 == 장비 X, 1 == 무기, 2 == 방어구, ...)
         - itemStat         아이템 스탯      아이템을 장착/사용 시 영향을 주는 값을 판별할 때 사용할 변수
                                             (0 == 사용 불가, 1 == 공격력, 2 == 방어력, 3 == 체력, 4 == 마나, ...)
         - itemStatValue    아이템 스탯 값   아이템을 장착/사용 시 영향을 주는 값의 수치를 정의하는 변수
         - itemExplanation  아이템 설명      아이템의 설명을 정의하는 변수
         - isEquip          장비 장착 여부   장비를 장착했는지 판별할 때 사용할 변수
         */
        public int itemNumber = 0;
        public string itemName = "";
        public int itemType = 0;
        public int equipType = 0;
        public int itemStat = 0;
        public int itemStatValue = 0;
        public string itemExplanation = "";
        public bool isEquip = false;

        // Item 생성에 참고할 메서드 생성
        public Item(int itemNumber, string itemName, int itemType, int equipType, int itemStat, int itemStatValue, string itemExplanation, bool isEquip)
        {
            this.itemNumber = itemNumber;
            this.itemName = itemName;
            this.itemType = itemType;
            this.equipType = equipType;
            this.itemStat = itemStat;
            this.itemStatValue = itemStatValue;
            this.itemExplanation = itemExplanation;
            this.isEquip = isEquip;
        }
    }

    internal class Items()
    {
        internal List<Item> items = new List<Item>()
        {
            new Item(0, "", 0, 0, 0, 0, "", false), // 기본 값
            new Item(1, "낡은 검", 1, 1, 1, 5, "", false),
            new Item(2, "냄비 뚜껑", 1, 2, 2, 2, "", false),
            new Item(3, "체력 회복 포션", 2, 0, 3, 30, "", false),
            new Item(4, "마나 회복 포션", 2, 0, 4, 20, "", false),
            new Item(5, "전리품 A", 0, 0, 0, 0, "", false), // 미니언이 드랍할 전리품
            new Item(6, "전리품 B", 0, 0, 0, 0, "", false), // 공허충이 드랍할 전리품
            new Item(7, "전리품 C", 0, 0, 0, 0, "", false), // 대포 미니언이 드랍할 전리품
        };
    }
}
