using TEAMPROJECT_TEXTRPG.Core;
using TEAMPROJECT_TEXTRPG.Utility;

namespace TEAMPROJECT_TEXTRPG.Managers
{
    /// <summary>
    /// 배틀 결과 상태
    /// </summary>
    internal enum NewBattleState
    {
        None,// 전투 X
        Start,// 전투 시작. 몬스터 소환
        PlayerTurn,
        MonsterTurn,
        Victory,
        Lose,
        //Result,
    }

    internal class BattleManager
    {
        /* 싱글톤 */
        //============================================================//
        private static BattleManager instance;
        internal static BattleManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BattleManager();
                }
                return instance;
            }
        }

        /* 이벤트 */
        //============================================================//

        // 몬스터 소환, ShowBattleStart
        internal Action<List<Monster>, Func<int, BattleInput>> OnMonsterSpawned;
        // 플레이어 턴, ShowPlayerTurn
        internal Action<
            List<Monster>,
            Func<int, BattleInput>,
            Func<int, (BattleInput, List<Skill>?)>,
            Func<int, (string, SelectAttackBasicResult)>,
            Func<int, int, (BattleInput, SkillAttackResult[]?)>> OnPlayerTurn;
        // 몬스터 턴, ShowMonsterTurn
        internal Action<Monster, int?, bool> OnMonsterActioned;

        /* 필드 */
        //============================================================//
        //private NewBattleState currentBattleState = NewBattleState.None;
        private NewBattleState CurrentBattleState { get; set; }

        // 몬스터 리스트 원본
        private Monsters monsterList = new Monsters();
        // 현재 스폰된 몬스터
        private List<Monster> CurrentMonsters { get; set; }
        // 현재 직업에 따른 스킬
        private List<Skill> CurrentSkills { get; set; }
        // 배틀 결과
        internal bool? isLastBattleWin = null;

        #region Test
        /* 테스트 전용 */
        //============================================================//

        internal void Test()
        {
            UIColorUtility.WriteColoredLine("냐호", ConsoleColor.DarkBlue);
            Console.WriteLine("진짜?");
            UIColorUtility.WriteColoredLine("야호", ConsoleColor.Blue);
        }
        #endregion

        /* 상태 관련 메서드 */
        //============================================================//

        /// <summary>
        /// 현재 배틀 상태를 바꾸는 메서드.
        /// Battle 씬의 첫 번째로 작동하는 외부 호출용.
        /// </summary>
        internal void ChangeBattleState()
        {
            // 나중에 이걸로 배틀 상태 저장할 수 있음.
            if (GameManager.Instance.currentState != GameState.TotalBattle)
            {
                // 이어서 하기
                //ChangeBattleState(currentBattleState);
                ChangeBattleState(NewBattleState.None);
                return;
            }

            switch (CurrentBattleState)
            {
                case NewBattleState.None:
                    // 현재 상태가 배틀이 아니면 배틀 스타트
                    ChangeBattleState(NewBattleState.Start);
                    break;
                case NewBattleState.Start:
                    // 현재 상태가 배틀 시작이면 플레이어 턴으로
                    ChangeBattleState(NewBattleState.PlayerTurn);
                    break;
                case NewBattleState.PlayerTurn:
                    if (IsMonstersAllDead())
                    {
                        // 플레이어 턴에서 몬스터가 다 죽었으면 승리
                        //ChangeBattleState(NewBattleState.Result);
                        ChangeBattleState(NewBattleState.Victory);
                    }
                    else
                        // 아니면 몬스터 턴으로
                        ChangeBattleState(NewBattleState.MonsterTurn);
                    break;
                case NewBattleState.MonsterTurn:
                    if (IsPlayerDead())
                    {
                        // 몬스터 턴에서 플레이어가 죽었으면 패배
                        //ChangeBattleState(NewBattleState.Result);
                        ChangeBattleState(NewBattleState.Lose);
                    }
                    else
                        // 아니면 몬스터 턴으로
                        ChangeBattleState(NewBattleState.PlayerTurn);
                    break;
                    //case NewBattleState.Result:
                    //    ChangeBattleState(NewBattleState.None);
                    //    break;
                    //case NewBattleState.Victory:
                    //case NewBattleState.Lose:
                    //    // 현재 상태가 승리 또는 패배라면
                    //    // 배틀 상태가 아니므로 None
                    //    ChangeBattleState(NewBattleState.None);
                    //    break;
            }

            HandleBattleState();
        }

        /// <summary>
        /// 현재 배틀 상태에 따라
        /// 적절한 로직을 호출하는 메서드
        /// </summary>
        private void HandleBattleState()
        {
            if (CurrentBattleState == NewBattleState.None)
            {
                // 이전 배틀 내용을 초기화?
            }
            else if (CurrentBattleState == NewBattleState.Start)
            {
                // 배틀 시작 위한 준비 코드
                SetBattle();

                // 화면 호출
                OnMonsterSpawned?.Invoke(CurrentMonsters, GetBattleStart);
            }
            else if (CurrentBattleState == NewBattleState.PlayerTurn)
            {
                // 플레이어 턴 화면 호출
                OnPlayerTurn?.Invoke(CurrentMonsters,
                    GetVaildMonsterSelection,
                    GetAttackType,
                    GetBasicAttackResult,
                    GetSkillAttackResult);
            }
            else if (CurrentBattleState == NewBattleState.MonsterTurn)
            {
                // 몬스터가 공격하는 로직
                MonsterTurn();
            }
            else if (CurrentBattleState == NewBattleState.Victory)
            {
                // 결과 창으로 전환 위해 상태 변경
                isLastBattleWin = true;
                GameManager.Instance.ChangeGameState(GameState.NewBattleResult);
            }
            else if (CurrentBattleState == NewBattleState.Lose)
            {
                // 결과 창으로 전환 위해 상태 변경
                isLastBattleWin = false;
                GameManager.Instance.ChangeGameState(GameState.NewBattleResult);
            }
        }

        /* 내부 로직 */
        //============================================================//

        /// <summary>
        /// 전투 시작시 필요한 정보 로드
        /// </summary>
        private void SetBattle()
        {
            CurrentMonsters = monsterList.SpawnRandomMonsters();

            // 와 이게 뭐냐... 대박...
            CurrentSkills = CharacterManager.Instance.player.CurrentJob.Name switch
            {
                "전사" => SkillDatabase.Instance.WarriorSkills,
                "마법사" => SkillDatabase.Instance.MageSkills,
                "도적" => SkillDatabase.Instance.ThiefSkills,
                _ => throw new ArgumentException("현재 직업에 없는 직업을 가지고 있습니다. 확인 바람."),
            };

            CharacterManager.Instance.player.hped = CharacterManager.Instance.player.Hp;
        }

        /// <summary>
        /// 몬스터 공격 처리
        /// </summary>
        private void MonsterTurn()
        {
            foreach (var monster in CurrentMonsters)
            {
                if (monster.IsDead) continue;

                // 회피
                var chance = new Random().Next(0, 100);
                if (chance < 10)
                {
                    OnMonsterActioned?.Invoke(monster, null, true);
                    return;
                }

                var playerOldHp = CharacterManager.Instance.player.Hp;
                monster.Attack(CharacterManager.Instance.player);

                OnMonsterActioned?.Invoke(monster, playerOldHp, false);
            }
        }

        /// <summary>
        /// 일반 공격
        /// </summary>
        /// <param name="monster"></param>
        /// <returns></returns>
        private (bool, int) AttackByBasicToMonster(Monster monster)
        {
            var attackDamage = 0;
            if (IsCritical())
            {
                attackDamage = CharacterManager.Instance.player.AttackCritical(monster);
                return (true, attackDamage);
            }
            attackDamage = CharacterManager.Instance.player.AttackBasic(monster);
            return (false, attackDamage);
        }

        /// <summary>
        /// 스킬 공격
        /// </summary>
        /// <param name="skill"></param>
        /// <param name="monsterIndex"></param>
        /// <returns></returns>
        private (string, int, bool)[] AttackBySkillToMonster(Skill skill, int monsterIndex)
        {
            var playerAtk = CharacterManager.Instance.player.Attack;
            int damage = (int)(playerAtk * skill.Multiple); // 실행시 데미지 계산

            if (!skill.IsRandom)
            {
                var monster = CurrentMonsters[monsterIndex - 1];
                ApplyDamage(monster, damage);
                return [(monster.Name, damage, monster.IsDead)];
            }

            //복수 대상
            //살아있는 몬스터만 대상으로
            var aliveMonsters = CurrentMonsters
                                .Where(m => !m.IsDead)
                                .ToList();

            // 반환용
            var tempList = new List<(string, int, bool)>();
            // 실제 타격 수는 Count와 생존 수 중 작은 값으로
            var count = Math.Min(skill.Count, aliveMonsters.Count);
            var random = new Random();
            for (int i = 0; i < count; i++)
            {
                if (aliveMonsters.Count == 0) break;
                // aliveMonsters 에서만 대상 선정
                var index = random.Next(aliveMonsters.Count);
                var targetMonster = aliveMonsters[index];

                //같은 대상 중복 방지
                aliveMonsters.RemoveAt(index);

                ApplyDamage(targetMonster, damage);

                tempList.Add((targetMonster.Name, damage, targetMonster.IsDead));
            }

            return tempList.ToArray();
        }

        private void ApplyDamage(Monster target, int damage)
        {
            target.Hp -= damage;

            if (target.Hp <= 0)
            {
                target.Hp = 0;
                target.IsDead = true;
            }
        }

        /* 이벤트용 */
        //============================================================//

        /// <summary>
        /// ShowBattleStart
        /// </summary>
        private BattleInput GetBattleStart(int input)
        {
            // 전투 취소
            if (input == 0)
            {
                GameManager.Instance.ChangeGameState(GameState.Home);
                ChangeBattleState();
                return BattleInput.IsValid | BattleInput.IsQuit;
            }
            // 전투 시작
            else if (input == 1)
            {
                ChangeBattleState();
                return BattleInput.IsValid;
            }
            // 잘못된 입력
            else
                return BattleInput.None;
        }

        /// <summary>
        /// ShowPlayerTurn, 몬스터 고를지 나갈지
        /// </summary>
        private BattleInput GetVaildMonsterSelection(int input)
        {
            // 배틀 취소
            if (input == 0)
            {
                GameManager.Instance.ChangeGameState(GameState.Home);
                return BattleInput.IsValid | BattleInput.IsQuit;
            }
            else if (input >= 1 && input <= CurrentMonsters.Count)
            {
                // 몬스터를 골랐는데 죽은 몬스터일 때
                if (CurrentMonsters[input - 1].IsDead)
                {
                    return BattleInput.IsValid | BattleInput.IsDead;
                }
                return BattleInput.IsValid;
            }
            else
                return BattleInput.None;
        }

        /// <summary>
        /// ShowPlayerTurn, 스킬 선택할지 뒤로 갈지
        /// </summary>
        private (BattleInput, List<Skill>?) GetAttackType(int input)
        {
            // 몬스터 다시 고르기
            if (input == 0)
            {
                return (BattleInput.IsValid | BattleInput.IsQuit, null);
            }
            // 기본 공격
            else if (input == 1)
            {
                return (BattleInput.IsValid | BattleInput.IsBasicAttack, null);
            }
            // 스킬 공격
            else if (input == 2)
            {
                return (BattleInput.IsValid, CurrentSkills);
            }
            else
                return (BattleInput.None, null);
        }

        /// <summary>
        /// ShowPlayerTurn, 기본 공격 선택시
        /// </summary>
        internal (string, SelectAttackBasicResult) GetBasicAttackResult(int index)
        {
            var targetMonster = CurrentMonsters[index - 1];
            var oldHp = targetMonster.Hp;

            (bool isCritical, int attackPower) = AttackByBasicToMonster(targetMonster);

            var targetName = targetMonster.Name;
            var attackResult = new SelectAttackBasicResult(isCritical, attackPower, oldHp, targetMonster.Hp, targetMonster.IsDead);
            var tuple = (targetName, attackResult);
            return tuple;
        }

        /// <summary>
        /// ShowPlayerTurn, 스킬 공격 선택시
        /// </summary>
        internal (BattleInput, SkillAttackResult[]?) GetSkillAttackResult(int skillIndex, int monsterIndex)
        {
            // 뒤로 가기
            if (skillIndex == 0)
            {
                return (BattleInput.IsValid | BattleInput.IsQuit, null);
            }
            else if (skillIndex > 0 && skillIndex <= CurrentSkills.Count)
            {
                var skill = CurrentSkills[skillIndex - 1];
                if (!IsMpEnough(skill.Mp))
                {
                    return (BattleInput.IsValid, null);
                }

                //MP 차감
                CharacterManager.Instance.player.Mp -= skill.Mp;
                (string targetName, int damage, bool isDead)[] results = AttackBySkillToMonster(skill, monsterIndex);
                var skillAttackResult = new List<SkillAttackResult>();
                foreach (var result in results)
                {
                    skillAttackResult
                        .Add(new SkillAttackResult(
                            result.targetName,
                            skill.Name,
                            skill.Description,
                            result.damage,
                            result.isDead
                            ));
                }

                return (BattleInput.IsValid | BattleInput.IsSkillAttack, skillAttackResult.ToArray());
            }
            else
                return (BattleInput.None, null);
        }

        /* 결과창 전용 */
        //============================================================//

        /// <summary>
        /// 배틀 결과를 전달하는 메서드.
        /// BattleResultScene에서 호출.
        /// </summary>
        internal (bool?, int[]) GetBattleResult()
        {
            // 전투 씬 나가고 3초 뒤 초기화
            Task.Run(async () =>
            {
                await Task.Delay(3000);
                ChangeBattleState(NewBattleState.None);
                isLastBattleWin = null;
            });

            return (isLastBattleWin, CurrentMonsters.Select(monster => monster.MonExp).ToArray());
        }

        /* 유틸리티 */
        //============================================================//
        private void ChangeBattleState(NewBattleState state) => CurrentBattleState = state;
        private bool IsMonstersAllDead() => CurrentMonsters.All(monster => monster.IsDead == true);
        private bool IsPlayerDead() => CharacterManager.Instance.player.Hp <= 0;
        private bool IsCritical() => new Random().Next(100) < 15;
        private bool IsMpEnough(int skillMp) => CharacterManager.Instance.player.Mp >= skillMp;
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
    internal readonly string TargetName { get; }
    internal readonly string SkillName { get; }
    internal readonly string SkillDesc { get; }
    internal readonly int Damage { get; }
    internal readonly bool IsDead { get; }

    internal SkillAttackResult(string targetName, string skillName, string skillDesc, int damage, bool isDead)
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