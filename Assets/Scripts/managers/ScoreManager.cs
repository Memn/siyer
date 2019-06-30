using UnityEngine;

namespace managers
{
    public class ScoreManager : MonoBehaviour, IScoreManager
    {
        public static IScoreManager Instance
        {
            get { return UserManagement.Instance.GetComponent<ScoreManager>(); }
        }

        public int Score
        {
            get { return UserManagement.Instance.User.Score; }

            set { UserManagement.Instance.User.Score = value; }
        }

        public int Level
        {
            get { return UserManagement.Instance.User.Level; }
            set { UserManagement.Instance.User.Level = value; }
        }

        public void UpdateScore(int score)
        {
            Debug.Log("score updated to : " + score);
        }
    }
}