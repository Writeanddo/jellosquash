using UnityEngine;

public partial class Player
{
  [Header("Movement")]
  public CharacterController controller;
  public float speed = 40.0f;

  public Transform cameraTransform;
  public float turnSmoothTime = 0.1f;
  private float _turnSmoothVelocity;

  private Vector3 _velocity;
  public float gravity = -49.05f;

  public Transform groundCheck;
  public float groundDistance = 0.7f;
  public float jumpHeight = 7.0f;
  private bool _isGrounded;
  public AnimationCurve jumpAnimation;

  void MovementUpdate()
  {
    Cursor.lockState = CursorLockMode.Locked;
    _isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance);
    if (_isGrounded && _velocity.y < 0) _velocity.y = 0.1f * gravity;

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

    if (Input.GetButtonDown("Jump") && _isGrounded)
      _velocity.y = Mathf.Sqrt(jumpHeight * -2.0f * gravity);

    _velocity.y += gravity * Time.deltaTime;
      controller.Move(_velocity * Time.deltaTime);
  }
}