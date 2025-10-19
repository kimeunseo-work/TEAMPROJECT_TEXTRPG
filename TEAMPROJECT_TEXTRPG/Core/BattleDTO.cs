namespace TEAMPROJECT_TEXTRPG.Core
{
    /// <summary>
    /// 인풋 유효성 및 선택지 판별
    /// 시간 없으니 나누지 않고 하나로 통일
    /// </summary>
    [Flags]
    internal enum BattleInput
    {
        None = 0,
        IsValid = 1 << 0,
        IsQuit = 1 << 1,
        IsDead = 1 << 2,
        IsBasicAttack = 1 << 3,
        IsSkillAttack = 1 << 4,
    }

    /// <summary>
    /// Battle Start Dto
    /// </summary>
    internal class BattleStartDto
    {
        public List<Monster> CurrentMonsters { get; }
        public Func<int, BattleInput> HandleBattleStartInput { get; }

        public BattleStartDto(List<Monster> currentMonsters, Func<int, BattleInput> handleBattleStartInput)
        {
            if (currentMonsters.Count <= 0)
            {
                throw new ArgumentException("스폰 몬스터가 없습니다. 확인 바람.");
            }

            CurrentMonsters = currentMonsters;
            HandleBattleStartInput = handleBattleStartInput;
        }
    }

    /// <summary>
    /// 플레이어 턴 Dto
    /// </summary>
    internal class PlayerTurnDto
    {
        public List<Monster> CurrentMonsters { get; }
        public Func<int, BattleInput> HandleMonsterSelectionInput { get; }
        public Func<int, (BattleInput, List<Skill>?)> HandleAttackTypeInput { get; }
        public Func<int, (string, SelectAttackBasicResult)> HandleBasicAttackInput { get; }
        public Func<int, int, (BattleInput, SkillAttackResult[]?)> HandleSkillAttackInput { get; }
        public PlayerTurnDto(
            List<Monster> currentMonsters,
            Func<int, BattleInput> handleMonsterSelectionInput,
            Func<int, (BattleInput, List<Skill>?)> getAttackType,
            Func<int, (string, SelectAttackBasicResult)> handleBasicAttackInput,
            Func<int, int, (BattleInput, SkillAttackResult[]?)> handleSkillAttackInput)
        {
            if (currentMonsters.Count <= 0)
            {
                throw new ArgumentException("스폰 몬스터가 없습니다. 확인 바람.");
            }

            CurrentMonsters = currentMonsters;
            HandleMonsterSelectionInput = handleMonsterSelectionInput;
            HandleAttackTypeInput = getAttackType;
            HandleBasicAttackInput = handleBasicAttackInput;
            HandleSkillAttackInput = handleSkillAttackInput;
        }
    }

    internal class MonsterTurnDto
    {
        public Monster Monster { get; }
        public int? PlayerOldHp { get; }
        public bool IsDodge { get; }

        public MonsterTurnDto(Monster monster, int? playerOldHp, bool isDodge)
        {
            if (monster == null)
            {
                throw new ArgumentException("스폰 몬스터가 없습니다. 확인 바람.");
            }

            Monster = monster;
            PlayerOldHp = playerOldHp;
            IsDodge = isDodge;
        }
    }

    internal readonly struct SelectAttackBasicResult
    {
        internal readonly bool IsCritical { get; }
        internal readonly int AttackPower { get; }
        internal readonly int OldHp { get; }
        internal readonly int NewHp { get; }
        internal readonly bool IsDead { get; }

        internal SelectAttackBasicResult(bool isCritical, int attackPower, int oldHp, int newHp, bool isDead)
        {
            if (attackPower < 0)
                throw new ArgumentException("AttackPower는 0 이상이어야 한다.");
            if (oldHp <= 0)
                throw new ArgumentException("OldHp 1 이상이어야 한다.");

            IsCritical = isCritical;
            AttackPower = attackPower;
            OldHp = oldHp;
            NewHp = newHp;
            IsDead = isDead;
        }
    }

    internal readonly struct SkillAttackResult
    {
        public readonly string TargetName { get; }
        public readonly string SkillName { get; }
        public readonly string SkillDesc { get; }
        public readonly int Damage { get; }
        public readonly bool IsDead { get; }

        public SkillAttackResult(string targetName, string skillName, string skillDesc, int damage, bool isDead)
        {
            if (damage < 0)
            {
                throw new ArgumentException("스킬 데미지가 음수입니다.");
            }

            TargetName = targetName;
            SkillName = skillName;
            SkillDesc = skillDesc;
            Damage = damage;
            IsDead = isDead;
        }
    }
}