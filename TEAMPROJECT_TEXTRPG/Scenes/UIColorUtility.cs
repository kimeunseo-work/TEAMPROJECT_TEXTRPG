namespace TEAMPROJECT_TEXTRPG.Scenes
{
    internal static class UIColorUtility
    {
        internal static void WriteColoredLine(string Text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(Text);
            Console.ResetColor();
        }
    }
}
