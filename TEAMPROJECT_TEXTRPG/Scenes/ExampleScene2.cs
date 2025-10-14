namespace TEAMPROJECT_TEXTRPG.Scenes
{
    internal class ExampleScene2 : Scene
    {
        internal override void Show()
        {
            Console.Clear();

            Console.WriteLine("프로젝트 화이팅~\n");

            Console.WriteLine("1. 예제 1번 씬으로 이동");

            var key = int.Parse(Console.ReadLine());
            InputHandler(key);
        }

        private void InputHandler(int key)
        {
            switch (key)
            {
                case 1:
                    GameManager.Instance.currentState = GameState.Example1;
                    break;
            }
        }
    }
}
