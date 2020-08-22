using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
  [SerializeField]
  private float speed = 10f;

  private void Update()
  {
    if (Input.GetKey(KeyCode.W))
      transform.Translate(Vector2.up * Time.deltaTime * speed);
    else if (Input.GetKey(KeyCode.S))
      transform.Translate(Vector2.down * Time.deltaTime * speed);
    if (Input.GetKey(KeyCode.A))
      transform.Translate(Vector2.left * Time.deltaTime * speed);
    else if (Input.GetKey(KeyCode.D))
      transform.Translate(Vector2.right * Time.deltaTime * speed);
  }
}