using TEAMPROJECT_TEXTRPG.Core;

namespace TEAMPROJECT_TEXTRPG.Managers
{
    internal class JobManager
    {
        public static List<Job> AllJobs { get; private set; }

        static JobManager()
        {
            AllJobs = new List<Job>()
            {
                new Job("전사", 150, 50, 5, 10, 20, 5, 0.5, 1),
                new Job("마법사", 100, 100, 8, 7, 10, 10, 1, 0.5),
                new Job("도적", 120, 70, 10, 5, 10, 10, 0.5, 0.5)
            };
        }

        public static Job GetJob(int index)
        {
            if (index > 0 && index < AllJobs.Count)
            {
                return AllJobs[index];
            }
            else
            {
                return null;
            }
        }
    }
}
