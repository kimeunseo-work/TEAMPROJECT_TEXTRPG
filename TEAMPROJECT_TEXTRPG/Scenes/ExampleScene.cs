namespace TEAMPROJECT_TEXTRPG.Scenes
{
    internal class ExampleScene : Scene
    {
        internal override void Show()
        {
            Console.Clear();

            Console.WriteLine("9조 화이팅~\n");

            Console.WriteLine("1. 예제 2번 씬으로 이동");

            var key = int.Parse(Console.ReadLine());
            InputHandler(key);
        }

        private void InputHandler(int key)
        {
            switch (key)
            {
                case 1:
                    GameManager.Instance.currentState = GameState.Example2;
                    break;
            }
        }
    }
}
