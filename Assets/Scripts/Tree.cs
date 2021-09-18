using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
  Transform player;

  void Start()
  {
    SetRandomXPosition();
    player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
  }

  void FixedUpdate()
  {
    if (player.position.z > transform.position.z + 5)
    {
      Destroy(gameObject);
    }
  }

  void SetRandomXPosition()
  {
    float posX = Random.Range(-4.5f, 4.5f);
    transform.position = new Vector3(posX, transform.position.y, transform.position.z);
  }
}
