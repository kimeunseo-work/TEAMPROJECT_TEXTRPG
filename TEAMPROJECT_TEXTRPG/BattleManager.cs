using TEAMPROJECT_TEXTRPG.Scenes;
using TEAMPROJECT_TEXTRPG.Skills;

namespace TEAMPROJECT_TEXTRPG
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
        // 몬스터 턴, ShowMonsterTurn
        internal Action<Monster, int?, bool> OnMonsterActioned;

        /* 필드 */
        //============================================================//
        //private NewBattleState currentBattleState = NewBattleState.None;
        private NewBattleState CurrentBattleState { get; set; }

        // 몬스터 리스트 원본
        private Monsters monsterList = new Monsters();
        // 현재 스폰된 몬스터
        internal List<Monster> CurrentMonsters;

        // 현재 직업에 따른 스킬
        private List<Skill> CurrentSkills { get; set; }
        // 배틀 결과
        internal bool? isLastBattleWin = null;

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
                // 플레이어 공격 입력 받고 처리해야 함.
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
                GameManager.Instance.ChangeGameState(GameState.BattleResult);
            }
            else if (CurrentBattleState == NewBattleState.Lose)
            {
                // 결과 창으로 전환 위해 상태 변경
                isLastBattleWin = false;
                GameManager.Instance.ChangeGameState(GameState.BattleResult);
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
                return BattleInput.IsValid | BattleInput.IsQuit;
            }
            // 전투 시작
            else if (input == 1)
            {
                GameManager.Instance.ChangeGameState(GameState.TotalBattle);
                return BattleInput.IsValid;
            }
            // 잘못된 입력
            else
                return BattleInput.None;
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