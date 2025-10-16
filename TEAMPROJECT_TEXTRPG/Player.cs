namespace TEAMPROJECT_TEXTRPG
{
    internal class Player
    {
        public int Level { get; set; } = 1;
        public string Name { get; set; }
        public Job CurrentJob { get; set; }
        public int Gold { get; set; } = 1500;
        public int MaxHP { get; set; }
        public int MaxMP { get; set; }
        public double BaseAttack { get; set; } = 10;
        public double BaseDefense { get; set; } = 5;
        public double Attack { get; set; }
        public double Defense { get; set; }

        public int Exp { get; set; }

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
            Attack = BaseAttack;
            Defense = BaseDefense;
            Hp = 100; //시작 체력 100
            Mp = 50; // 시작 마나 50
        }

        public void AddExp(int getExp)
        {
            Exp += getExp;
            Console.WriteLine($"\n경험치{getExp} 획득 (현재 {Exp} / 필요 {GetRequiredExp()})");

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
        private int GetRequiredExp() =>

            ExpTable.ContainsKey(Level) ? ExpTable[Level] : int.MaxValue;



        private void LevelUp()
        {
            Level++;
            MaxHP += 20;
            MaxMP += 10;
            BaseAttack += 0.5;
            BaseDefense += 1;

            //회복

            Hp = MaxHP;

            Console.WriteLine($"\n 레벨 업! 현재 레벨: {Level}");
            Console.WriteLine($"공격력 + 0.5 -> {BaseAttack}, 방어력 + 1 -> {BaseDefense}");

        }

        internal void TakeDamage(int amount) => Hp -= amount;
    }
}
