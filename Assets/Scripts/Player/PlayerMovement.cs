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

  public Transform[] groundChecks;
  public float groundDistance = 0.7f;
  public float jumpHeight = 7.0f;
  private bool _isGrounded;
  public AnimationCurve jumpAnimation;

  public GameObject jellyPrefab;
  public float spawnOffset = 0.1f;
  public float throwVelocity = 1.0f;

  public bool attack = false;

  private void MovementUpdate()
  {
    Cursor.lockState = CursorLockMode.Locked;
    foreach (Transform groundCheck in groundChecks)
    {
      if (Physics.CheckSphere(groundCheck.position, groundDistance))
      {
        _isGrounded = true;
        break;
      }
      _isGrounded = false;
    }
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
    {
      attack = false;
      _velocity.y = Mathf.Sqrt(jumpHeight * -2.0f * gravity);
    }

    if (Input.GetButtonDown("Fire1") && !_isGrounded)
    {
      attack = true;
      _velocity.y = -Mathf.Sqrt(jumpHeight * -4.0f * gravity);
      if (transform.localScale.y > 1.0f)
      {
        GameObject littleJelly = Instantiate(jellyPrefab, transform.position + transform.forward*transform.localScale.z + transform.forward*spawnOffset, transform.rotation);
        littleJelly.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        Rigidbody littleJellyRB = littleJelly.GetComponent<Rigidbody>();
        littleJellyRB.isKinematic = false;
        littleJellyRB.velocity = transform.forward*throwVelocity;

        SphereCollider littleJellySC = littleJelly.GetComponent<SphereCollider>();
        littleJellySC.isTrigger = false;

        _localScale -= new Vector3(0.5f, 0.5f, 0.5f);
        _animationTime = 0.0f;
      }
    }

    _velocity.y += gravity * Time.deltaTime;
      controller.Move(_velocity * Time.deltaTime);
  }
}
