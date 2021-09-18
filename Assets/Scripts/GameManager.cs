using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
  private static GameManager instance;
  public static GameManager Instance
  {
    get
    {
      return instance;
    }
  }

  public static GameStatus gameStatus = GameStatus.MENU;
  public static bool hasSecondChance = true;

  float score = 0;
  float scoreIncrease = 0.1f;

  Player player;
  MainCamera camera;
  AudioSource music;

  public GameObject challenge1Prefab;
  public GameObject challenge2Prefab;
  public GameObject challenge3Prefab;
  public GameObject challenge4Prefab;
  public GameObject challenge5Prefab;
  public GameObject challenge6Prefab;
  public GameObject challenge7Prefab;
  public GameObject challenge8Prefab;
  public GameObject challenge9Prefab;

  static int numberOfChallenges = 9;
  public static float highscore = 0;

  void Start()
  {
    instance = this;
    GooglePlayGame.Init();
    GooglePlayGame.Login(false);
    if (PlayerPrefs.HasKey("Highscore"))
    {
      highscore = PlayerPrefs.GetFloat("Highscore");
    }

    player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MainCamera>();
    music = GameObject.Find("Music").GetComponent<AudioSource>();

    InvokeRepeating("IncreaseScore", scoreIncrease, scoreIncrease);
    SpawnChallenge(82);
  }

  public void SetGameOver()
  {
    gameStatus = GameStatus.GAMEOVER;
    AdManager.Instance.ShowBanner();
    GooglePlayGame.ReportScore(score);
    music.Pause();
    SetHighscore();
    UIManager.Instance.HidePauseButton();
    if (hasSecondChance)
    {
      UIManager.Instance.ShowGameOverVideoUI();
    }
    else
    {
      UIManager.Instance.ShowGameOverUI();
    }
  }

  public void InitGame()
  {
    UIManager.Instance.HideMenuUI();
    gameStatus = GameStatus.MOVING_CAMERA;
    AdManager.Instance.HideBanner();
    camera.ResetRotation();
  }

  public void ReloadGame()
  {
    hasSecondChance = true;
    player.Reset();
    camera.ResetPosition();
    gameStatus = GameStatus.PLAYING;
    score = 0;
    UIManager.Instance.HideGameOverUI();
    UIManager.Instance.HideGameOverVideoUI();
    UIManager.Instance.HidePauseUI();
    UIManager.Instance.ShowPauseButton();
    AdManager.Instance.HideBanner();

    music.Play();
    DestroyChallenges();
    SpawnChallenge(24.58f);
    SpawnChallenge(82);
  }

  public void ReloadGameAfterVideo()
  {
    hasSecondChance = false;
    player.Reset();
    camera.ResetPosition();
    gameStatus = GameStatus.PLAYING;
    UIManager.Instance.HideGameOverVideoUI();
    UIManager.Instance.HidePauseUI();
    UIManager.Instance.ShowPauseButton();
    AdManager.Instance.HideBanner();

    music.Play();
    DestroyChallenges();
    Instantiate(challenge6Prefab, new Vector3(3.23f, -42f, 24.58f), Quaternion.identity);
    SpawnChallenge(82);
  }

  public void PauseGame()
  {
    gameStatus = GameStatus.PAUSED;
    AdManager.Instance.ShowBanner();
    UIManager.Instance.ShowPauseUI();
  }

  public void ResumeGame()
  {
    gameStatus = GameStatus.PLAYING;
    AdManager.Instance.HideBanner();
    UIManager.Instance.HidePauseUI();
  }

  void DestroyChallenges()
  {
    GameObject[] challenges = GameObject.FindGameObjectsWithTag("Challenge");
    foreach (GameObject challenge in challenges)
    {
      Destroy(challenge);
    }
  }

  void IncreaseScore()
  {
    if (gameStatus == GameStatus.PLAYING)
    {
      score += scoreIncrease;
      UIManager.Instance.SetScoreText(score);
    }
  }

  void SetHighscore()
  {
    if (PlayerPrefs.HasKey("Highscore"))
    {
      if (score > highscore)
      {
        highscore = score;
        PlayerPrefs.SetFloat("Highscore", highscore);
        UIManager.Instance.SetHighscoreScoreText(highscore);
      }
    }
    else
    {
      highscore = score;
      PlayerPrefs.SetFloat("Highscore", highscore);
      UIManager.Instance.SetHighscoreScoreText(highscore);
    }
  }

  public void SpawnChallenge(float posZ)
  {
    int randomNumber = Random.Range(1, numberOfChallenges + 1);
    GameObject prefab = challenge1Prefab;
    switch (randomNumber)
    {
      case 2:
        prefab = challenge2Prefab;
        break;
      case 3:
        prefab = challenge3Prefab;
        break;
      case 4:
        prefab = challenge4Prefab;
        break;
      case 5:
        prefab = challenge5Prefab;
        break;
      case 6:
        prefab = challenge6Prefab;
        break;
      case 7:
        prefab = challenge7Prefab;
        break;
      case 8:
        prefab = challenge8Prefab;
        break;
      case 9:
        prefab = challenge9Prefab;
        break;
    }
    Instantiate(prefab, new Vector3(3.23f, -42f, posZ), Quaternion.identity);
  }
}

public enum GameStatus
{
  MENU,
  MOVING_CAMERA,
  PLAYING,
  PAUSED,
  GAMEOVER
}