using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cliff : MonoBehaviour
{
  Transform player;
  bool canMove = false;
  float speed = 0.2f;
  float playerSpeed = 0.2f;
  public bool hasFixedPosition;

  void Start()
  {
    if (!hasFixedPosition)
    {
      SetPosition();
    }
    player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    playerSpeed = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().speed;
  }

  void FixedUpdate()
  {
    if (transform.position.z - player.position.z < 6f)
    {
      canMove = true;
    }
    if (canMove)
    {
      Move();
    }
    speed = transform.position.x > player.position.x ? -playerSpeed : playerSpeed;
  }

  void Move()
  {
    if (GameManager.gameStatus == GameStatus.PLAYING && transform.position.z - player.position.z < 6f)
    {
      transform.position += transform.right * speed;
    }
  }

  void SetPosition()
  {
    float posZ = Random.Range(transform.position.z - 5f, transform.position.z + 5f);
    transform.position = new Vector3(transform.position.x, transform.position.y, posZ);
  }
}
