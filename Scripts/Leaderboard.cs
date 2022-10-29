using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SocialPlatforms;
using UnityEngine.UI;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class Leaderboard : MonoBehaviour
{
    public bool connectedToGooglePlay;

    private void Start()
        {
        AuthenticateUser();
        }

    void AuthenticateUser()
        {
        PlayGamesPlatform.Activate();
        Social.localUser.Authenticate((bool success) =>
        {
            if (success)
                {
                Debug.Log("Logged In");

                }
            else
                {
                Debug.Log("Unable to log in");
                }
        });
        }
   
    public static void PostToLeaderboard(long newScore)
        {
        Social.ReportScore(newScore, GPGSIds.leaderboard_bounce, UpdateLeaderboard);
        }

    static void UpdateLeaderboard(bool success)
        {
        if (success) Debug.Log("Updated Leaderboard");
        else Debug.Log("Unable to update");
        }

    public static void ShowLeaderboard()
        {
        PlayGamesPlatform.Instance.ShowLeaderboardUI(GPGSIds.leaderboard_bounce);
        }
    }
