using UnityEngine;
using UnityEngine.VFX;
using System.Collections;

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
  public LayerMask groundLayer;
  public float groundDistance = 0.7f;
  public float jumpHeight = 7.0f;
  private bool _isGrounded;
  public AnimationCurve jumpAnimation;
  public GameObject squashVFXPrefab;

  public GameObject jellyPrefab;
  public float spawnOffset = 0.1f;
  public float throwVelocity = 1.0f;

  public bool attack = false;

  public AudioClip smash;
  public AudioClip jump;
  public AudioClip takenDamage;
  public AudioClip pickup;
  public AudioClip gainMass;

  private void MovementInit(){}

  private void MovementUpdate()
  {
    foreach (Transform groundCheck in groundChecks)
    {
      if (Physics.CheckSphere(groundCheck.position, groundDistance+transform.localScale.x/10.0f, groundLayer))
      {
        _isGrounded = true;
        if (attack)
        {
          StartCoroutine(SquashVFX());
          DropJelly(transform.forward);
          SoundFX.source.PlayOneShot(smash);
        }

        attack = false;
        break;
      }
      _isGrounded = false;
    }
    if (_isGrounded && _velocity.y < 0) _velocity.y = 0.1f * gravity;

    float horizontal = Input.GetAxisRaw("Horizontal");
    float vertical = Input.GetAxisRaw("Vertical");

    Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

    if (direction.magnitude >= 0.1 && !dead)
    {
      float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
      float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, turnSmoothTime);
      transform.rotation = Quaternion.Euler(0.0f, angle, 0.0f);

      Vector3 moveDirection = Quaternion.Euler(0.0f, targetAngle, 0.0f) * Vector3.forward;
      controller.Move(moveDirection.normalized * speed * Time.deltaTime);
    }

    if (Input.GetButtonDown("Jump") && _isGrounded && !dead)
    {
      attack = false;
      _velocity.y = Mathf.Sqrt(jumpHeight * -2.0f * gravity);
      SoundFX.source.PlayOneShot(jump);
    }

    if (Input.GetButtonDown("Fire1") && !_isGrounded && !dead)
    {
      attack = true;
      _velocity.y = -Mathf.Sqrt(jumpHeight * -4.0f * gravity);
    }

    _velocity.y += gravity * Time.deltaTime;
      controller.Move(_velocity * Time.deltaTime);

    _velocity *= 0.98f;
  }

  public IEnumerator SquashVFX()
  {
    VisualEffect squashVFX = Instantiate(squashVFXPrefab, transform.position, transform.rotation).GetComponent<VisualEffect>();
    squashVFX.Play();
    yield return new WaitForSeconds(0.2f);
    squashVFX.Stop();
    yield return new WaitForSeconds(3.0f);
    Destroy(squashVFX.gameObject);
  }
}
