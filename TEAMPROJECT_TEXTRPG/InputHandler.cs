using System;
using System.Text.RegularExpressions;

namespace TEAMPROJECT_TEXTRPG
{
    public static class InputHandler
    {
        public static int GetUserActionInput()
        {
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">> ");
            string input = Console.ReadLine() ?? "";    // null을 반환할 경우 빈 문자열을 대신 사용
            string cleanInput = Regex.Replace(input, @"\s+", "");   // 입력받은 문자열에서 공백 제거

            int parsedInput;
            bool parsedSuccess = int.TryParse(cleanInput, out parsedInput);

            if (parsedSuccess)
            {
                return parsedInput;
            }
            else
            {
                // 숫자가 아닌 입력이나 유효하지 않은 입력은 int.MaxValue로 반환하여 씬에서 처리
                return int.MaxValue;
            }
        }


        public static int GetUserActionInputInBattle()
        {
            Console.WriteLine();
            Console.WriteLine("대상을 선택해주세요.");
            Console.Write(">> ");
            string input = Console.ReadLine() ?? "";    // null을 반환할 경우 빈 문자열을 대신 사용
            string cleanInput = Regex.Replace(input, @"\s+", "");   // 입력받은 문자열에서 공백 제거

            int parsedInput;
            bool parsedSuccess = int.TryParse(cleanInput, out parsedInput);

            if (parsedSuccess)
            {
                return parsedInput;
            }
            else
            {
                // 숫자가 아닌 입력이나 유효하지 않은 입력은 int.MaxValue로 반환하여 씬에서 처리
                return int.MaxValue;
            }
        }

    }
}