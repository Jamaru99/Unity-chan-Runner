using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
  public float jumpForce;
  public float speed = 0.21f;
  bool onFloor;
  bool jumpAllowed = false;
  bool swiped = false;

  Animator anim;
  Rigidbody rigidBody;
  AudioSource audioSource;

  Vector2 startTouchPosition, endTouchPosition;

  public static Vector3 initialPosition = new Vector3(0, -0.8f, -7f);

  void Start()
  {
    anim = GetComponent<Animator>();
    rigidBody = GetComponent<Rigidbody>();
    audioSource = GetComponent<AudioSource>();
  }

  void Update()
  {
    SwipeCheck();
  }

  void FixedUpdate()
  {
    if (GameManager.gameStatus == GameStatus.PLAYING)
    {
      anim.SetFloat("Speed", 1);
      //Move();
      //Jump();
      MoveMobile();
      JumpMobile();
    }
    else
    {
      anim.SetFloat("Speed", 0);
    }
    HandleFall();
  }

  /*
  void Move()
  {
    speed += 0.00002f;
    transform.position += transform.forward * speed;
    if (Input.GetKey("d"))
    {
      transform.position += new Vector3(speed, 0, 0);
    }
    if (Input.GetKey("a"))
    {
      transform.position += new Vector3(-speed, 0, 0);
    }
  }*/

  void SwipeCheck()
  {
    if (Input.touchCount > 0)
    {
      Touch touch = Input.GetTouch(0);
      if (touch.phase == TouchPhase.Began)
      {
        startTouchPosition = touch.position;
      }
      if (touch.phase == TouchPhase.Moved)
      {
        swiped = true;
      }
      if (touch.phase == TouchPhase.Ended)
      {
        endTouchPosition = touch.position;
        if (endTouchPosition.y > startTouchPosition.y + 1 && swiped)
        {
          jumpAllowed = true;
        }
      }
    }
  }

  void MoveMobile()
  {
    speed += 0.00002f;
    transform.position += transform.forward * speed;
    for (int i = 0; i < Input.touchCount; i++)
    {
      Touch touch = Input.GetTouch(i);
      if (touch.phase == TouchPhase.Stationary)
      {
        if (touch.position.x > Screen.width / 2)
        {
          transform.position += new Vector3(speed, 0, 0);
        }
        if (touch.position.x < Screen.width / 2)
        {
          transform.position += new Vector3(-speed, 0, 0);
        }
      }
    }
  }

  /*
  void Jump()
  {
    if (Input.GetKey("space") && onFloor)
    {
      anim.SetBool("Jump", true);
      rigidBody.AddForce(0, jumpForce, 0);
      onFloor = false;
    }
  }*/

  void JumpMobile()
  {
    if (onFloor && jumpAllowed)
    {
      anim.SetBool("Jump", true);
      rigidBody.AddForce(0, jumpForce, 0);
      onFloor = false;
      jumpAllowed = false;
    }
  }

  void HandleFall()
  {
    if (transform.position.y < -5)
    {
      if (GameManager.gameStatus != GameStatus.GAMEOVER)
      {
        GameManager.Instance.SetGameOver();
      }
      transform.GetChild(2).parent = null;
      rigidBody.useGravity = false;
    }
    else
    {
      rigidBody.useGravity = true;
    }
  }

  public void Reset()
  {
    transform.position = initialPosition;
    anim.SetFloat("Speed", 1);
    speed = 0.21f;
  }

  void OnCollisionEnter(Collision other)
  {
    if (other.gameObject.tag == "Floor")
    {
      onFloor = true;
      swiped = false;
      anim.SetBool("Jump", false);
    }
    else
    {
      if (GameManager.gameStatus == GameStatus.PLAYING)
      {
        audioSource.Play();
        GameManager.Instance.SetGameOver();
        anim.SetFloat("Speed", 0);
      }
    }
  }

  void OnTriggerEnter(Collider other)
  {
    if (other.tag == "Respawn")
    {
      GameManager.Instance.SpawnChallenge(transform.position.z + 145);
    }
    if (other.tag == "Destroyer")
    {
      Destroy(other.transform.parent.gameObject);
    }
  }
}
