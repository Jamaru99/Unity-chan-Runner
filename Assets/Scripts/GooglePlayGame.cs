using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;
using GooglePlayGames.BasicApi;
using System;

public class GooglePlayGame : MonoBehaviour
{
  public static void Init()
  {
    PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
    PlayGamesPlatform.InitializeInstance(config);
    PlayGamesPlatform.DebugLogEnabled = true;
    PlayGamesPlatform.Activate();
  }

  public static void Login(bool prompt)
  {
    if (IsAuthenticated())
    {
      return;
    }

    SignInInteractivity signInInteractivity = prompt ? SignInInteractivity.CanPromptAlways : SignInInteractivity.NoPrompt;
    PlayGamesPlatform.Instance.Authenticate(signInInteractivity, (result) => { });
  }

  public static bool IsAuthenticated()
  {
    return Social.localUser.authenticated;
  }

  public static void ReportScore(float score)
  {
    long parsedScore = (long)(score * 10);
    Social.ReportScore(parsedScore, "CgkI1Laz28MdEAIQAQ", (bool success) => { });
  }

  public static void ShowLeaderboardsUI()
  {
    PlayGamesPlatform.Instance.ShowLeaderboardUI("CgkI1Laz28MdEAIQAQ");
  }
}

