using TEAMPROJECT_TEXTRPG.Core;
using TEAMPROJECT_TEXTRPG.Managers;

namespace TEAMPROJECT_TEXTRPG.Scenes
{
    internal class NewBattleResultScene : Scene
    {
        private string input;       // Player 의 입력을 받아올 값에 대한 선언 (= 입력 좀 해줄래?)
        private int subExp;         // 랜덤으로 생성한 몬스터들의 경험치의 합 연산 값을 정의하기 위한 선언 (= 경험치 {subExp} 획득!)
        private int leveled;        // 레벨업 전 레벨 값 정의를 위한 선언 (= 레벨_이었던 것)
        private int leveling;       // 레벨업 후 레벨 값 정의를 위한 선언 (= 지금 레벨인 것)

        private Player player;      // Player 정보를 가져오기 위한 변수 선언

        // 해당 Class 작업에 참고할 메서드 생성
        public NewBattleResultScene()
        {
            // Player 정보를 가져오기 위한 변수 초기화
            player = CharacterManager.Instance.player;
        }

        // GameManager 에서 요구한 작업을 실행하는 메서드
        public override void Show()
        {
            // BattleManager에서 요구한 작업을 실행하기 위한 구독 값 생성
            BattleManager.Instance.OnBattleResultReady += HandleResult;

            // 반복_현재 씬이 NewBattleResult 일 때
            while (GameManager.Instance.CurrentState == GameState.NewBattleResult)
            {
                // 반복문의 무한반복을 막기 위한 코드
                Thread.Sleep(100);
            }

            // BattleManager에서 요구한 작업을 실행하기 위한 구독 값 소멸
            BattleManager.Instance.OnBattleResultReady -= HandleResult;
        }

        // BattleManager에서 요구한 작업을 실행하기 위한 메서드
        private void HandleResult(NewBattleState result, int[] monsterExps)
        {
            // 전투의 결과가 '승리'일 경우
            if (result == NewBattleState.Victory)
            {
                // '전투 결과창(승리)' 출력
                BattleResultWin(monsterExps);
            }
            // 전투의 결과가 '패배'일 경우
            else if (result == NewBattleState.Lose)
            {
                // '전투 결과창(패배)' 출력
                BattleResultLose();
            }
        }

        // 전투 결과창(승리)   ==>>   전투에서 승리했을 경우 출력하는 씬
        internal void BattleResultWin(int[] monsterExps)
        {
            // 랜덤 몬스터 경험치 합 값 초기화
            subExp = 0;
        
            // 랜덤 몬스터 경험치 합 연산 (반복문)
            for (int i = 0; i < monsterExps.Length; i++)
            {
                subExp += monsterExps[i];
            }

            // 레벨업 전 레벨 값 정의
            leveled = player.Level;

            // 위 for 문(합 연산)에서 출력된 값을 Player 경험치에 합산
            player.AddExp(subExp);

            // 레벨업 후 레벨 값 정의
            leveling = player.Level;

            // 무한 반복 <<-- Player 가 입력한 값이 '0' 일 때까지
            while (true)
            {
                // Console 창 Clear
                Console.Clear();

                // '전투 결과창(승리) Text' 묶음 출력하고
                BattleResultWinText(monsterExps.Length);

                Console.WriteLine();
                // 'Quest 현황 갱신' 묶음 실행하고
                QuestManager.Instance.OnMonsterKilled();
                Console.WriteLine();
                
                // 'Player 입력 대기 Text' 묶음 출력하고
                DaumText();
                
                // Player 입력 값 체크하고
                input = Console.ReadLine();

                // Player 가 입력한 값이 '0' 이 맞으면
                if (input == "0")
                {
                    // '메인 화면 복귀 Text' 출력
                    Console.WriteLine("메인 화면으로 돌아갑니다.");
                    // Player 키 입력_아무 키
                    Console.ReadKey();

                    // 무한 반복문 탈출
                    break;
                }
                // Player 가 입력한 값이 '0' 이 아니면
                else
                {
                    // '잘못된 입력 Text' 출력
                    Console.WriteLine("잘못된 입력입니다.");
                    // Player 키 입력_아무 키
                    Console.ReadKey();
                }
            }

            // Console 창 Clear
            Console.Clear();
            // Home 씬 불러오기
            GameManager.Instance.CurrentState = GameState.Home;
        }

        // 전투 결과창(패배)   ==>>   전투에서 패배했을 경우 출력하는 씬
        internal void BattleResultLose()
        {
            // 무한 반복 <<-- Player 가 입력한 값이 '0' 일 때까지
            while (true)
            {
                // Console 창 Clear
                Console.Clear();

                // '전투 결과창(패배) Text' 묶음 출력하고
                BattleResultLoseText();

                // 'Player 입력 대기 Text' 묶음 출력하고
                DaumText();

                // Player 입력 값 체크하고
                input = Console.ReadLine();

                // Player 가 입력한 값이 '0' 이 맞으면
                if (input == "0")
                {
                    // '메인 화면 복귀 Text' 출력
                    Console.WriteLine("메인 화면으로 돌아갑니다.");
                    // Player 키 입력_아무 키
                    Console.ReadKey();

                    // 무한 반복문 탈출
                    break;
                }
                // Player 가 입력한 값이 '0' 이 아니면 (그 외)
                else
                {
                    // '잘못된 입력 Text' 출력
                    Console.WriteLine("잘못된 입력입니다.");
                    // Player 키 입력_아무 키
                    Console.ReadKey();
                }
            }

            // Console 창 Clear
            Console.Clear();
            // Player 초기화
            CharacterManager.Instance.player = new();
            // CharacterCreate 씬 불러오기
            GameManager.Instance.CurrentState = GameState.CharacterCreate;
        }

        // '전투 결과창(승리) Text' 묶음
        private void BattleResultWinText(int monsterExpsCount)
        {
            Console.Clear();
            Console.WriteLine("Battle!! - Result");
            Console.WriteLine();
            Console.WriteLine("Victory!!");
            Console.WriteLine();
            Console.WriteLine($"던전에서 몬스터 {monsterExpsCount}마리를 잡았습니다.");
            Console.WriteLine($"Lv.{player.Level} {player.Name}");
            Console.WriteLine($"HP {player.Hped} -> {player.Hp}");
            Console.WriteLine();
            Console.WriteLine($"경험치{subExp} 획득 (현재 {player.Exp} / 필요 {player.GetRequiredExp()})");
            Console.WriteLine();

            // Player 가 레벨업 했을 경우 출력할 코드
            if (leveled != leveling)
            {
                Console.WriteLine($"레벨 업! 현재 레벨: {player.Level}");
                Console.WriteLine($"공격력 + {player.CurrentJob.LvUpAttack} -> {player.BaseAttack}, 방어력 + {player.CurrentJob.LvUpDefense} -> {player.BaseDefense}");
            }
        }

        // '전투 결과창(패배) Text' 묶음
        private void BattleResultLoseText()
        {
            Console.Clear();
            Console.WriteLine("Battle!! - Result");
            Console.WriteLine();
            Console.WriteLine("You Lose..");
            Console.WriteLine();
            Console.WriteLine($"Lv.{player.Level} {player.Name}");
            Console.WriteLine($"HP {player.Hped} -> 0");
            Console.WriteLine();
        }

        // 'Player 입력 대기 Text' 묶음
        private void DaumText()
        {
            Console.WriteLine();
            Console.WriteLine("0. 다음");
            Console.WriteLine();
            Console.Write(">> ");
        }
    }
}