namespace TEAMPROJECT_TEXTRPG
{
    internal class Item
    {
        // 주석_아이템 생성 필요하실 때 참고하세요! 아이템은 코드 가장 아랫쪽 class Items 에서 생성하시면 됩니다.

        /* 장비 Item_정의한 변수 설명
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
         - itemNumber       아이템 넘버      소비 아이템을 식별하고자 할 때 사용할 변수
         - itemName         아이템 이름      소비 아이템의 이름을 판별할 때 사용할 변수
         - itemStat         아이템 스탯      소비 아이템을 사용 시 영향을 주는 값을 판별할 때 사용할 변수
                                             (0 == 사용 불가, 1 == 체력, 2 == 마나, ...)
         - itemStatValue    아이템 스탯 값   소비 아이템을 사용 시 영향을 주는 값의 수치를 정의하는 변수
         - itemExplanation  아이템 설명      소비 아이템의 설명을 정의하는 변수
         */

        /* 기타 Item_정의한 변수 설명
         - itemNumber       아이템 넘버      아이템을 식별하고자 할 때 사용할 변수
         - itemName         아이템 이름      아이템의 이름을 판별할 때 사용할 변수
         - itemExplanation  아이템 설명      아이템의 설명을 정의하는 변수
         */

        // Item 변수 정의
        public string itemName = "";
        public int itemType = 0;
        public int itemStat = 0;
        public int itemStatValue = 0;
        public string itemExplanation = "";
        public bool isEquip = false;
    }

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

        public EquipItem(Item other)
        {
            this.itemName = other.itemName;
            this.itemType = other.itemType;
            this.itemStat = other.itemStat;
            this.itemStatValue = other.itemStatValue;
            this.itemExplanation = other.itemExplanation;
            this.isEquip = other.isEquip;
        }
    }

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

        public UseItem(Item other)
        {
            this.itemName = other.itemName;
            this.itemStat = other.itemStat;
            this.itemStatValue = other.itemStatValue;
            this.itemExplanation = other.itemExplanation;
        }
    }

    internal class EtcItem : Item
    {
        // 기타 Item 생성에 참고할 메서드 생성
        public EtcItem(string itemName, string itemExplanation)
        {
            this.itemName = itemName;
            this.itemExplanation = itemExplanation;
        }

        public EtcItem(Item other)
        {
            this.itemName = other.itemName;
            this.itemExplanation = other.itemExplanation;
        }
    }

    internal class Items
    {
        public List<EquipItem> equipItems = new List<EquipItem>();
        public List<UseItem> useItems = new List<UseItem>();
        public List<EtcItem> etcItems = new List<EtcItem>();

        public Items()
        {
            // 새롭게 생성한 장비 Item
            equipItems.Add(new EquipItem("", 0, 0, 0, "", false)); // 기본 값
            equipItems.Add(new EquipItem("낡은 검", 1, 1, 5, "", false));
            equipItems.Add(new EquipItem("냄비 뚜껑", 2, 2, 2, "", false));

            // 새롭게 생성한 소비 Item
            useItems.Add(new UseItem("", 0, 0, "")); // 기본 값
            useItems.Add(new UseItem("체력 회복 포션", 1, 30, ""));
            useItems.Add(new UseItem("마나 회복 포션", 2, 20, ""));

            // 새롭게 생성한 기타 Item
            etcItems.Add(new EtcItem("", "")); // 기본 값
            etcItems.Add(new EtcItem("전리품 A", "")); // '미니언'이 드랍할 전리품
            etcItems.Add(new EtcItem("전리품 B", "")); // '공허충'이 드랍할 전리품
            etcItems.Add(new EtcItem("전리품 C", "")); // '대포 미니언'이 드랍할 전리품
            etcItems.Add(new EtcItem("전리품 D", "")); // '돌거북'이 드랍할 전리품
            etcItems.Add(new EtcItem("전리품 E", "")); // '고대 돌거북'이 드랍할 전리품
            etcItems.Add(new EtcItem("전리품 F", "")); // '칼날부리'가 드랍할 전리품
        }
    }

    internal class Inven : Items
    {
        public List<Items> inven = new List<Items>();
    }

    internal class DropItems : Items
    {
        public List<EtcItem> monster1DropItems = new List<EtcItem>();

        public DropItems()
        {
            // 예: etcItems 리스트에서 "전리품 A", "전리품 B" 복제해서 드롭리스트에 삽입
            EtcItem etc1 = new EtcItem(etcItems[1]); // 전리품 A
            EtcItem etc2 = new EtcItem(etcItems[2]); // 전리품 B

            monster1DropItems.Add(etc1);
            monster1DropItems.Add(etc2);
        }
    }
}
