using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

  public CharacterController controller;
  public float speed = 6.0f;
  public float turnSmoothTime = 0.1f;
  private float _turnSmoothVelocity;

  public Transform cameraTransform;

  void Update()
  {
    Cursor.lockState = CursorLockMode.Locked;

    float horizontal = Input.GetAxisRaw("Horizontal");
    float vertical = Input.GetAxisRaw("Vertical");

    Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

    if (direction.magnitude >= 0.1)
    {
      float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
      float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, turnSmoothTime);
      transform.rotation = Quaternion.Euler(0.0f, angle, 0.0f);

      Vector3 moveDirection = Quaternion.Euler(0.0f, targetAngle, 0.0f) * Vector3.forward;
      controller.Move(moveDirection.normalized * speed * Time.deltaTime);
    }
  }
}
