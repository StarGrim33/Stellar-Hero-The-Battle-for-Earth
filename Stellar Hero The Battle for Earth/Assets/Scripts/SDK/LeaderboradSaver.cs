using Agava.YandexGames;
using UnityEngine;

public class LeaderboradSaver : MonoBehaviour
{
    public void GetLeaderboardPlayerEntryButtonClick(int score)
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
