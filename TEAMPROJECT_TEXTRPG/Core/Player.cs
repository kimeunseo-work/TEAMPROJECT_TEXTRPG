using TEAMPROJECT_TEXTRPG._Unfinished;

namespace TEAMPROJECT_TEXTRPG.Core
{

    public class Player
    {
        public int Level { get; set; } = 1;
        public string Name { get; set; }
        public Job CurrentJob { get; set; }
        private int hp;
        public int Hp
        {
            get => hp;
            set => hp = Math.Max(0, value); // hp가 0보다 작으면 0으로 고정
        }
        private int mp;
        public int Mp
        {
            get => mp;
            set => mp = Math.Max(0, value); // mp가 0보다 작으면 0으로 고정
        }
        public int MaxHP { get; set; }
        public int MaxMP { get; set; }
        public double BaseAttack { get; set; }
        public double BaseDefense { get; set; }
        public double Attack { get; set; }
        public double Defense { get; set; }
        public double LvUpAttack { get; set; }
        public double LvUpDefense { get; set; }
        public Inventory Inventory; // Player 인벤토리 정의
        public int Exp { get; set; }
        public int Gold { get; set; } = 1500;

        // 전투 전 체력 정의
        public int Hped;
        //경험치 테이블
        private Dictionary<int, int> ExpTable = new Dictionary<int, int>()
        {
            { 1, 10 },
            { 2, 35 },
            { 3, 65 },
            { 4, 100 },
        };

        public Player() // 기본 공격력과 추가된 총 공격력 구별
        {
            // 직업 선택 전 임시 값
            MaxHP = 100;
            MaxMP = 50;
            BaseAttack = 10;
            BaseDefense = 5;
            Attack = BaseAttack;
            Defense = BaseDefense;
            Hp = MaxHP;
            Mp = MaxMP;
            LvUpAttack = 0;
            LvUpDefense = 0;
            Inventory = new Inventory(); // 생성자에 인벤토리 생성
        }

        public void SetJobsStat(Job selectedJob)
        {
            CurrentJob = selectedJob;

            MaxHP = CurrentJob.MaxHP;
            MaxMP = CurrentJob.MaxMP;
            BaseAttack = CurrentJob.BaseAttack;
            BaseDefense = CurrentJob.BaseDefense;
            Attack = BaseAttack;
            Defense = BaseDefense;
            Hp = MaxHP;
            Mp = MaxMP;
            LvUpAttack = CurrentJob.LvUpAttack;
            LvUpDefense = CurrentJob.LvUpDefense;
        }

        public void AddExp(int getExp)
        {
            Exp += getExp;

            while (true)
            {
                int RequiredExp = GetRequiredExp();
                if (Exp < RequiredExp)
                    break;
                Exp -= RequiredExp;
                LevelUp();
            }


        }
        // 경험치 반환
        public int GetRequiredExp() =>

            ExpTable.ContainsKey(Level) ? ExpTable[Level] : int.MaxValue;

        private void LevelUp()
        {
            Level++;
            MaxHP += CurrentJob.LvUpAddMaxHP;
            MaxMP += CurrentJob.LvUpAddMaxMP;
            BaseAttack += CurrentJob.LvUpAttack;
            BaseDefense += CurrentJob.LvUpDefense;
            Attack = BaseAttack;
            Defense = BaseDefense;

            //회복

            Hp = MaxHP;
            Mp = MaxMP;
        }

        public void TakeDamage(int amount) => Hp -= amount;

        /// <summary>
        /// 플레이어 일반 공격
        /// </summary>
        public int AttackBasic(Monster monster)
        {
            var damageErrorValue = (int)Math.Ceiling(Attack * 0.1d);
            var actualDamage = new Random().Next((int)Attack - damageErrorValue, (int)Attack + damageErrorValue + 1);

            monster.TakeDamage(actualDamage);
            return actualDamage;
        }

        /// <summary>
        /// 플레이어 크리티컬 공격
        /// </summary>
        /// <param name="player"></param>
        public int AttackCritical(Monster monster)
        {
            var damageErrorValue = (int)Math.Ceiling(Attack * 0.1d);
            var actualDamage = new Random().Next((int)Attack - damageErrorValue, (int)Attack + damageErrorValue + 1);

            actualDamage = (int)(actualDamage * 1.6d);
            monster.TakeDamage(actualDamage);

            return actualDamage;
        }

        // Inventory 내 Item 추가 메서드
        public void AddItemToInventory(Item item)
        {
            Inventory.inventory.Add(item);
        }

        // Inventory 출력 메서드
        public void ShowInventory()
        {
            // 반복문_인벤토리 내 Item
            foreach (Item item in Inventory.inventory)
            {
                // Item 유형 판별 식
                switch (item.itemType)
                {
                    // 아이템 유형 == 장비 Item
                    case 1:
                        Console.WriteLine($"- 아이템 이름 : {item.itemName} / {item.itemExplanation}");
                        break;
                    // 아이템 유형 == 소비 Item
                    case 2:
                        Console.WriteLine($"- 아이템 이름 : {item.itemName} / {item.itemExplanation}");
                        break;
                    // 아이템 유형 == 기타 Item
                    case 3:
                        Console.WriteLine($"- 아이템 이름 : {item.itemName} / {item.itemExplanation}");
                        break;
                }
            }
        }
    }
}