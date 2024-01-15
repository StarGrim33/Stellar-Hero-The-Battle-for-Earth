using Agava.YandexGames;
using UnityEngine;
using Utils;

namespace SDK
{
    public class LeaderboardSaver : MonoBehaviour
    {
        public void UpdateOrCreatePlayerScore(int score)
        {
            Leaderboard.GetPlayerEntry(Constants.LeaderboardName, (result) =>
            {
                if (result == null)
                {
                    Leaderboard.SetScore(Constants.LeaderboardName, score);
                }
                else
                {
                    if (result.score < score)
                    {
                        Leaderboard.SetScore(Constants.LeaderboardName, score);
                    }
                }
            });
        }
    }
}