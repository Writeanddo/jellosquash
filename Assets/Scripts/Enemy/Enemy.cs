using UnityEngine;

public class Enemy : MonoBehaviour
{
  public Rigidbody rb;
  public Transform player;
  public float detectRange;

  void Update()
  {
    Vector3 distance = transform.position - player.position;
    float degree = player.eulerAngles.y - transform.eulerAngles.y;
    print(distance);
    print(degree);
  }

}
