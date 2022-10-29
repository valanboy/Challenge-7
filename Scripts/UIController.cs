using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GooglePlayGames;
using GooglePlayGames.BasicApi;


public class UIController : MonoBehaviour {
    public InputField inputField;
    public Text errorText;

    private void Start()
        {
        errorText.text = "";
        }

    public void onButtonPostToLeaderboard()
        {
        Debug.Log("posting");
        errorText.text = "";

        if (string.IsNullOrEmpty(inputField.text))
            {
            errorText.text = "Please enter a valid score";
            return;
            }
        else
            {
            long scoreToPost;

            if(long.TryParse(inputField.text, out scoreToPost))
                {
                Leaderboard.PostToLeaderboard(scoreToPost);
                }
            else
                {
                errorText.text = "Could not post score";
                }
            }
        }
    
    public void OnButtonShowLeaderboard()
        {
        Debug.Log("Showing Leaderboard");
        Leaderboard.ShowLeaderboard();
        }
    }
  
