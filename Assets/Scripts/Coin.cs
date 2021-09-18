using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
  void FixedUpdate()
  {
    transform.Rotate(0, 5, 0);
  }
}
