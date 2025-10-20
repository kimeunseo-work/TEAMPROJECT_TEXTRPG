using System.Text;

namespace TEAMPROJECT_TEXTRPG.Utility
{
    internal static class ConsoleUtility
    {
        /// <summary>
        /// 콘솔 라인 지우는 메서드
        /// </summary>
        /// <param name="top">지우기 시작할 행</param>
        /// <param name="count">행 부터 몇 줄?</param>
        internal static void ClearLine(int top, int count)
        {
            var text = new StringBuilder();
            for (int i = 0; i < 120; i++)
            {
                text.Append(' ');
            }

            for (int i = 0; i < count; i++)
            {
                Console.SetCursorPosition(0, top - i);
                Console.Write(text);
            }

            Console.SetCursorPosition(0, Console.CursorTop - 1);
        }
    }
}
