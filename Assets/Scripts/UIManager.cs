using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
  private static UIManager instance;
  public static UIManager Instance
  {
    get
    {
      return instance;
    }
  }

  GameObject gameOverUI;
  GameObject gameOverVideoUI;
  GameObject menuUI;
  GameObject pauseUI;
  GameObject howToPlayUI;
  GameObject pauseButton;
  Text scoreText;
  Text highScoreMenuText;
  Text highScorePauseText;

  void Start()
  {
    instance = this;
    gameOverUI = GameObject.Find("GameOver");
    gameOverVideoUI = GameObject.Find("GameOverVideo");
    menuUI = GameObject.Find("Menu");
    pauseUI = GameObject.Find("Pause");
    howToPlayUI = GameObject.Find("HowToPlay");
    pauseButton = GameObject.Find("ButtonPause");
    scoreText = GameObject.Find("Score").GetComponent<Text>();
    highScoreMenuText = GameObject.Find("HighscoreMenu").GetComponent<Text>();
    highScorePauseText = GameObject.Find("HighscorePause").GetComponent<Text>();
    HideGameOverUI();
    HideGameOverVideoUI();
    HidePauseButton();
    HidePauseUI();
    HideHowToPlayUI();
    SetHighscoreScoreText(GameManager.highscore);
  }

  public void ShowGameOverUI()
  {
    gameOverUI.SetActive(true);
  }

  public void HideGameOverUI()
  {
    gameOverUI.SetActive(false);
  }

  public void ShowGameOverVideoUI()
  {
    gameOverVideoUI.SetActive(true);
  }

  public void HideGameOverVideoUI()
  {
    gameOverVideoUI.SetActive(false);
  }

  public void ShowPauseUI()
  {
    pauseUI.SetActive(true);
  }

  public void HidePauseUI()
  {
    pauseUI.SetActive(false);
  }

  public void ShowHowToPlayUI()
  {
    howToPlayUI.SetActive(true);
    if (GameManager.gameStatus == GameStatus.MENU)
    {
      menuUI.SetActive(false);
    }
    else
    {
      pauseUI.SetActive(false);
    }
  }

  public void HideHowToPlayUI()
  {
    howToPlayUI.SetActive(false);
    if (GameManager.gameStatus == GameStatus.MENU)
    {
      menuUI.SetActive(true);
    }
    else
    {
      pauseUI.SetActive(true);
    }
  }

  public void ShowPauseButton()
  {
    pauseButton.SetActive(true);
  }

  public void HidePauseButton()
  {
    pauseButton.SetActive(false);
  }

  public void HideMenuUI()
  {
    menuUI.SetActive(false);
  }

  public void SetScoreText(float value)
  {
    scoreText.text = value.ToString("N1");
  }

  public void SetHighscoreScoreText(float value)
  {
    highScoreMenuText.text = "Highscore: " + value.ToString("N1");
    highScorePauseText.text = "Highscore: " + value.ToString("N1");
  }

  public void ShowLeaderboardsUI()
  {
    GooglePlayGame.Login(true);
    GooglePlayGame.ShowLeaderboardsUI();
  }
}
