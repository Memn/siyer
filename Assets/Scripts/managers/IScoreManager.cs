namespace managers
{
    public interface IScoreManager
    {
        int Score { get; set; }
        int Level { get; set; }
        void UpdateScore(int score);
    }
}