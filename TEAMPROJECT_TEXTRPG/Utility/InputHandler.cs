using System.Text.RegularExpressions;

namespace TEAMPROJECT_TEXTRPG.Utility
{
    public static class InputHandler
    {
        // 사용자가 입력한 내용을 int화하고 빈 문자열 혹은 공백 입력 시 제거하는 함수 (유효성 검사)
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

        // 위의 함수를 전투씬에서 사용할 함수
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

        // 사용자가 입력한 내용의 공백(띄어쓰기)을 제거하는 함수
        public static string GetUserCharacterName()
        {
            Console.WriteLine("원하시는 이름을 설정해주세요.");
            Console.Write(">> ");
            string input = Console.ReadLine() ?? "";
            string cleanInput = Regex.Replace(input, @"\s+", "");

            return cleanInput;
        }

        public static int GetInputToInt()
        {
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

        // UI 정렬을 위해 한글이면 2칸 그 외 문자면 1칸을 리턴하는 함수
        public static int GetPrintableLength(string str)
        {
            int length = 0;
            foreach (char c in str)
            {
                // 유니코드 카테고리가 'OtherLetter'(주로 한글 등 전각 문자)이면 2칸으로 간주
                if (System.Globalization.CharUnicodeInfo.GetUnicodeCategory(c) == System.Globalization.UnicodeCategory.OtherLetter)
                {
                    length += 2;
                }
                else
                {
                    length += 1; // 그 외(영문, 숫자, 기호 등)는 1칸
                }
            }
            return length;
        }
    }
}