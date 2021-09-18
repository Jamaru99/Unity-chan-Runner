using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
  bool shouldMove = false;
  public float speed;
  float rotationSpeed = 0.1f;
  float rotationX = 0;

  public static Vector3 initialPosition = new Vector3(0, 7f, -21f);

  void FixedUpdate()
  {
    if (GameManager.gameStatus == GameStatus.MENU)
    {
      transform.Rotate(0, rotationSpeed / 2, 0);
    }
    if (GameManager.gameStatus == GameStatus.MOVING_CAMERA)
    {
      if (transform.position.y < 7f)
      {
        GameManager.gameStatus = GameStatus.PLAYING;
        UIManager.Instance.ShowPauseButton();
      }
      else
      {
        transform.position += new Vector3(0, -speed, 0);
      }
      if (rotationX < 16)
      {
        transform.Rotate(rotationSpeed, 0, 0);
        rotationX += rotationSpeed;
      }
    }
  }

  public void ResetPosition()
  {
    Transform player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    transform.position = initialPosition;
    transform.parent = player;
  }

  public void ResetRotation()
  {
    transform.rotation = Quaternion.identity;
  }
}
