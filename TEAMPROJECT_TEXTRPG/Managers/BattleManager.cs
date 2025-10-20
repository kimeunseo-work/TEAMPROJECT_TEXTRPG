using TEAMPROJECT_TEXTRPG.Core;

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
    }

    internal class BattleManager
    {
        private readonly GameManager gameManager;
        private readonly Player player;

        public BattleManager()
        {
            // 가독성 살리기 위해
            gameManager = GameManager.Instance;
            player = CharacterManager.Instance.player;

            // 게임 매니저 상태 구독
            GameManager.Instance.OnBattleEntered += SetBattle;
        }

        #region /* 싱글톤 */
        //============================================================//
        private static BattleManager instance;
        public static BattleManager Instance
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
        #endregion

        #region /* 이벤트 */
        //============================================================//

        // GameManager 상태 구독용 이벤트
        public Action OnBattleEnd;
        public Action OnBattleExit;

        // BattleResult 전달용 이벤트
        public Action<NewBattleState, int[]> OnBattleResultReady;

        // 배틀 매니저의 CurrentBattleState를 추적하여 화면에 전송.
        public Action<NewBattleState, object?> OnBattleStateChanged;
        #endregion

        #region /* 필드 */
        //============================================================//

        // 현재 상태
        private NewBattleState CurrentBattleState { get; set; } = NewBattleState.None;
        // 몬스터 리스트 원본
        private readonly Monsters monsterList = new();
        // 현재 스폰된 몬스터
        private List<Monster>? CurrentMonsters { get; set; } = new List<Monster>();
        // 현재 직업에 따른 스킬
        private List<Skill> CurrentSkills { get; set; } = new List<Skill>();
        // 화면 출력이 끝났는지 체크
        private bool hasScreenCallEnded = false;
        #endregion

        /* 게임 진입 + 진행 */
        //============================================================//

        /// <summary>
        /// 배틀 데이터 할당
        /// </summary>
        private async Task SetBattle()
        {
            // 몬스터 데이터 초기 세팅
            CurrentMonsters = monsterList.SpawnRandomMonsters();
            // 전투 후 결과창에 출력할 Hp
            player.hped = player.Hp;
            // 스킬 최초 할당
            if (CurrentSkills.Count <= 0)
            {
                CurrentSkills = player.CurrentJob.Name switch
                {
                    "전사" => SkillDatabase.Instance.WarriorSkills,
                    "마법사" => SkillDatabase.Instance.MageSkills,
                    "도적" => SkillDatabase.Instance.ThiefSkills,
                    _ => throw new ArgumentException("현재 직업에 없는 직업을 가지고 있습니다. 확인 바람."),
                };
            }

            // 실제 씬 전환까지 대기
            while (gameManager.currentScene == gameManager.scenes[GameState.Home])
            {
                await Task.Delay(100);
            }
            // 이벤트 호출
            await Task.Run(RunBattle);
        }

        private async Task RunBattle()
        {
            // 상태에 따른 로직
            while (true)
            {
                hasScreenCallEnded = false;

                ChangeBattleState();
                HandleBattleState();

                // 화면 호출이 끝날 때 까지 대기
                while (!hasScreenCallEnded)
                {
                    await Task.Delay(100);
                }

                // 게임 종료 조건
                if (CurrentBattleState == NewBattleState.Victory
                    || CurrentBattleState == NewBattleState.Lose) break;

                // 중간에 나가기
                if (CurrentBattleState == NewBattleState.None)
                {
                    OnBattleExit?.Invoke();
                    break;
                }
            }
        }

        /// <summary>
        /// 배틀 결과
        /// </summary>
        private async Task ExitBattle()
        {
            var monstersExps = CurrentMonsters.Select(monster => monster.MonExp).ToArray();
            // 배틀이 끝났음을 게임 매니저에게 알림
            OnBattleEnd?.Invoke();

            // 실제로 배틀 씬에서 결과 씬을 전환될 때 까지 대기
            while (gameManager.currentScene == gameManager.scenes[GameState.TotalBattle])
            {
                await Task.Delay(100);
            }
            // 배틀 결과 데이터를 BattleResult에 보냄
            OnBattleResultReady?.Invoke(CurrentBattleState, monstersExps);

            // 상태 초기화
            // 현재 상태에서 none으로 변경
            CurrentBattleState = NewBattleState.None;
            // 몬스터 초기화
            CurrentMonsters = null;
        }

        #region /* 배틀 흐름 제어 메서드 */
        //============================================================//

        /// <summary>
        /// 현재 배틀 상태를 적절히 바꾸는 메서드
        /// </summary>
        private void ChangeBattleState()
        {
            // 나중에 이걸로 배틀 상태 저장할 수 있음.
            if (GameManager.Instance.CurrentState != GameState.TotalBattle)
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
                        ChangeBattleState(NewBattleState.Lose);
                    }
                    else
                        // 아니면 몬스터 턴으로
                        ChangeBattleState(NewBattleState.PlayerTurn);
                    break;
            }
        }

        /// <summary>
        /// 현재 배틀 상태에 따라
        /// 적절한 로직을 호출하는 메서드
        /// </summary>
        private void HandleBattleState()
        {
            switch (CurrentBattleState)
            {
                case NewBattleState.Start:
                    // 시작 화면 호출
                    OnBattleStateChanged?
                        .Invoke(
                            CurrentBattleState,
                            new BattleStartDto(
                                CurrentMonsters,
                                HandleBattleStartInput));
                    break;
                case NewBattleState.PlayerTurn:
                    // 플레이어 턴 화면 호출
                    OnBattleStateChanged?
                        .Invoke(
                            CurrentBattleState,
                            new PlayerTurnDto(
                                CurrentMonsters,
                                HandleMonsterSelectionInput,
                                HandleAttackTypeInput,
                                HandleBasicAttackInput,
                                HandleSkillAttackInput));
                    break;
                case NewBattleState.MonsterTurn:
                    // 몬스터 수 만큼 반복
                    foreach (var monster in CurrentMonsters)
                    {
                        if (monster.IsDead == true) continue;
                        var result = AttackByMonster(monster);
                        // 몬스터 화면 호출
                        OnBattleStateChanged?
                            .Invoke(
                                CurrentBattleState,
                                result);
                    }
                    break;
                case NewBattleState.Victory:
                case NewBattleState.Lose:
                    // 배틀 종료
                    ExitBattle();
                    break;
            }

            hasScreenCallEnded = true;
        }
        #endregion

        #region /* 이벤트용 */
        //============================================================//

        /// <summary>
        /// ShowBattleStart, 배틀 시작할지 홈으로 나갈지
        /// </summary>
        private BattleInput HandleBattleStartInput(int input)
        {
            // 전투 취소
            switch (input)
            {
                // 나가기
                case 0:
                    ChangeBattleState(NewBattleState.None);
                    return BattleInput.IsValid | BattleInput.IsQuit;
                // 전투 시작
                case 1:
                    return BattleInput.IsValid;
                // 잘못된 입력
                default:
                    return BattleInput.None;
            }
        }

        /// <summary>
        /// ShowPlayerTurn, 몬스터 고를지 홈으로 나갈지
        /// </summary>
        private BattleInput HandleMonsterSelectionInput(int input)
        {
            // 배틀 취소
            switch (input)
            {
                // 나가기
                case 0:
                    ChangeBattleState(NewBattleState.None);
                    return BattleInput.IsValid | BattleInput.IsQuit;
                // 몬스터 선택
                case >= 1 when input <= CurrentMonsters.Count:
                    // 생존 몬스터
                    if (!CurrentMonsters[input - 1].IsDead)
                    {
                        return BattleInput.IsValid;
                    }
                    return BattleInput.IsValid | BattleInput.IsDead;
                // 잘못된 입력
                default:
                    return BattleInput.None;
            }
        }

        /// <summary>
        /// ShowPlayerTurn, 스킬 선택할지 뒤로 갈지
        /// </summary>
        private (BattleInput, List<Skill>?) HandleAttackTypeInput(int input)
        {
            // 몬스터 다시 고르기
            return input switch
            {
                0 => (BattleInput.IsValid | BattleInput.IsQuit, null),
                1 => (BattleInput.IsValid | BattleInput.IsBasicAttack, null),
                2 => (BattleInput.IsValid, CurrentSkills),
                _ => (BattleInput.None, null)
            };
        }

        /// <summary>
        /// ShowPlayerTurn, 기본 공격 선택시
        /// </summary>
        public (string, SelectAttackBasicResult) HandleBasicAttackInput(int index)
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
        public (BattleInput, SkillAttackResult[]?) HandleSkillAttackInput(int skillIndex, int monsterIndex)
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
                    // 유효하지만 마나 없음
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

                // 유효하고 스킬맞음
                return (BattleInput.IsValid | BattleInput.IsSkillAttack, skillAttackResult.ToArray());
            }
            else
                return (BattleInput.None, null);
        }

        #endregion

        #region /* 내부 로직 */
        //============================================================//

        /// <summary>
        /// 몬스터 공격 처리
        /// </summary>
        private MonsterTurnDto AttackByMonster(Monster monster)
        {
            // 회피
            var chance = new Random().Next(0, 100);
            if (chance < 10)
            {
                return new MonsterTurnDto(monster, null, true);
            }

            var playerOldHp = CharacterManager.Instance.player.Hp;
            monster.Attack(CharacterManager.Instance.player);

            return new MonsterTurnDto(monster, playerOldHp, false);
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
        #endregion

        /* 유틸리티 */
        //============================================================//
        private void ChangeBattleState(NewBattleState state) => CurrentBattleState = state;
        private bool IsMonstersAllDead() => CurrentMonsters.All(monster => monster.IsDead == true);
        private bool IsPlayerDead() => player.Hp <= 0;
        private bool IsCritical() => new Random().Next(100) < 15;
        private bool IsMpEnough(int skillMp) => player.Mp >= skillMp;
    }
}