using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchPointer : MonoBehaviour
{
  float speed = 7f;
  float initialPosY;
  float endPosY;
  Transform startPosition;
  Transform endPosition;

  void Start()
  {
    startPosition = GameObject.Find("StartPosition").transform;
    endPosition = GameObject.Find("EndPosition").transform;
    InvokeRepeating("ResetPosition", 1.5f, 1.5f);
  }

  void FixedUpdate()
  {
    if (transform.position.y < endPosition.position.y)
    {
      transform.position += new Vector3(0, speed, 0);
    }
  }

  void ResetPosition()
  {
    transform.position = startPosition.position;
  }
}
