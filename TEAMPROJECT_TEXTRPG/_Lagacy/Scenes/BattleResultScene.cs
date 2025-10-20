#if false
using TEAMPROJECT_TEXTRPG.Managers;
using TEAMPROJECT_TEXTRPG.Scenes;

namespace TEAMPROJECT_TEXTRPG.Lagacy.Scenes
{
    internal class BattleResultScene : Scene
    {
        string input;           // Player 의 입력을 받아올 값에 대한 선언 (= 입력 좀 해줄래?)
        int subExp;             // 랜덤으로 생성한 몬스터들의 경험치의 합 연산 값을 정의하기 위한 선언 (= 경험치 {subExp} 획득!)
        int leveled;            // 레벨업 전 레벨 값 정의를 위한 선언 (= 레벨_이었던 것)
        int leveling;           // 레벨업 후 레벨 값 정의를 위한 선언 (= 지금 레벨인 것)

        // 전투 결과창(승리)   ==>>   전투에서 승리했을 경우 출력하는 씬
        internal void BattleResultWin()
        {
            // 랜덤 몬스터 경험치 합 초기화
            subExp = 0;

            // 랜덤 몬스터 경험치 합 연산 (반복문)
            for (int i = 0; i < GameManager.Instance.monsters.Count; i++)
            {
                subExp += GameManager.Instance.monsters[i].MonExp;
            }

            // 레벨업 전 레벨 값 정의
            leveled = CharacterManager.Instance.player.Level;

            // Player 에게 경험치 연산
            for (int i = 0; i < GameManager.Instance.monsters.Count; i++)
            {
                // <<-- 경험치 획득량 출력 빼야 하고
                CharacterManager.Instance.player.AddExp(GameManager.Instance.monsters[i].MonExp);
            }

            // 레벨업 후 레벨 값 정의
            leveling = CharacterManager.Instance.player.Level;

            // Console 창 Clear
            Console.Clear();

            // '전투 결과창(승리) Text' 묶음 출력하고
            BattleResultWinText();

            // 'Player 입력 대기 Text' 묶음 출력하고
            DaumText();

            // Player 입력 값 체크하고
            input = Console.ReadLine();

            // Player 가 입력한 값이 '0' 이 맞으면
            if (input == "0")
            {
                // Home 씬 불러오기
                Console.Clear();
                GameManager.Instance.CurrentState = GameState.Home;
            }
            // Player 가 입력한 값이 '0' 이 아니면
            else
            {
                // 무한 반복 <<-- 잘못된 입력 문구
                while (true)
                {
                    // Console 창 Clear
                    Console.Clear();

                    // '전투 결과창(승리) Text' 묶음 출력하고
                    BattleResultWinText();

                    // 'Player 입력 대기 Text' 묶음 출력하고
                    DaumText();

                    // 한 칸 띄워주고
                    Console.WriteLine();

                    // '잘못된 입력 Text' 묶음 출력하고
                    WrongText();

                    // Player 입력 값 체크하고
                    input = Console.ReadLine();

                    // Player 가 입력한 값이 '0' 이 맞으면
                    if (input == "0")
                    {
                        // Home 씬 불러오기 (+ 무한 반복문 탈출)
                        Console.Clear();
                        GameManager.Instance.CurrentState = GameState.Home;
                        break;
                    }
                    // Player 가 입력한 값이 '0' 이 아니면 
                    // 아래 내용이 없으니 무한 반복문 최상위로 되돌아감
                }
            }
        }

        // 전투 결과창(패배)   ==>>   전투에서 패배했을 경우 출력하는 씬
        internal void BattleResultLose()
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
                // Home 씬 불러오기
                Console.Clear();
                GameManager.Instance.CurrentState = GameState.Home;
            }
            // Player 가 입력한 값이 '0' 이 아니면 (그 외)
            else
            {
                // 무한 반복
                while (true)
                {
                    // Console 창 Clear
                    Console.Clear();

                    // '전투 결과창(패배) Text' 묶음 출력하고
                    BattleResultLoseText();

                    // 'Player 입력 대기 Text' 묶음 출력하고
                    DaumText();

                    // 한 칸 띄워주고
                    Console.WriteLine();

                    // '잘못된 입력 Text' 묶음 출력하고
                    WrongText();

                    // Player 입력 값 체크하고
                    input = Console.ReadLine();

                    // Player 가 입력한 값이 '0' 이 맞으면
                    if (input == "0")
                    {
                        // Home 씬 불러오기 (+ 무한 반복문 탈출)
                        Console.Clear();
                        GameManager.Instance.CurrentState = GameState.Home;
                        break;
                    }
                    // Player 가 입력한 값이 '0' 이 아니면 
                    // 아래 내용이 없으니 무한 반복문 최상위로 되돌아감
                }
            }
        }

        // '전투 결과창(승리) Text' 묶음
        private void BattleResultWinText()
        {
            Console.Clear();
            Console.WriteLine("Battle!! - Result");
            Console.WriteLine();
            Console.WriteLine("Victory!!");
            Console.WriteLine();
            Console.WriteLine($"던전에서 몬스터 {GameManager.Instance.monsters.Count}마리를 잡았습니다.");
            Console.WriteLine($"Lv.{CharacterManager.Instance.player.Level} {CharacterManager.Instance.player.Name}");
            Console.WriteLine($"HP {CharacterManager.Instance.player.Hped} -> {CharacterManager.Instance.player.Hp}");
            Console.WriteLine();
            Console.WriteLine($"경험치{subExp} 획득 (현재 {CharacterManager.Instance.player.Exp} / 필요 {CharacterManager.Instance.player.GetRequiredExp()})");
            Console.WriteLine();

            // Player 가 레벨업 했을 경우 출력할 코드
            if (leveled != leveling)
            {
                Console.WriteLine($"레벨 업! 현재 레벨: {CharacterManager.Instance.player.Level}");
                Console.WriteLine($"공격력 + {CharacterManager.Instance.player.CurrentJob.LvUpAttack} -> {CharacterManager.Instance.player.BaseAttack}, 방어력 + {CharacterManager.Instance.player.CurrentJob.LvUpDefense} -> {CharacterManager.Instance.player.BaseDefense}");
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
            Console.WriteLine($"Lv.{CharacterManager.Instance.player.Level} {CharacterManager.Instance.player.Name}");
            Console.WriteLine($"HP {CharacterManager.Instance.player.Hped} -> 0");
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

        // '잘못된 입력 Text' 묶음
        private void WrongText()
        {
            Console.WriteLine();
            Console.WriteLine("잘못된 입력입니다.");
            Console.WriteLine();
            Console.Write(">> ");
        }

        // GameManager에서 요구한 씬을 출력하는 메서드
        internal override void Show()
        {
            // GameManager 내 배틀 상태가 '승리' 일 경우
            if (GameManager.Instance.currentBattleState == BattleState.Victory)
            {
                // '전투 결과창(승리)' 출력
                BattleResultWin();
            }
            // GameManager 내 배틀 상태가 '패배' 일 경우
            else if (GameManager.Instance.currentBattleState == BattleState.Defeat)
            {
                // '전투 결과창(패배)' 출력
                BattleResultLose();
            }
        }
    }
}
#endif