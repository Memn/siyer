namespace managers
{
    public interface IProgressManager
    {
        void UnlockAchievement(string buildingId, int score);
        void CheckLevelUp(bool b);
        void Reward(CommonResources.Building building, int score);
        void ReportScore(int score);
    }
}