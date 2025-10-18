using TEAMPROJECT_TEXTRPG.Core;

namespace TEAMPROJECT_TEXTRPG.Managers
{
    internal class CharacterManager
    {
        /* 싱글톤 */
        //============================================================//
        private static CharacterManager instance;
        internal static CharacterManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CharacterManager();
                }
                return instance;
            }
        }
        internal Player player = new();
    }
}
