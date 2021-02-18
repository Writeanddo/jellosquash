using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
  public NavMeshAgent agent;
  public LayerMask playerLayer;
  [HideInInspector]
  public bool die = false;
  public bool dead = false;
  public float targetSquashSize = 0.1f;
  public AnimationCurve squashedCurve;
  public float squashedDuration;

  private Transform _target;
  private bool _followPlayer = false;
  private float _animationTime;
  private Vector3 _localScale;

  void Start()
  {
    _animationTime = squashedDuration;
    _localScale = transform.localScale;
  }

  void Update()
  {
    if (_followPlayer && !dead)
    {
      agent.SetDestination(_target.position);
    }
    if (die && !dead)
    {
      _animationTime = 0.0f;
      dead = true;
    }

    if (_animationTime < squashedDuration)
    {
      _animationTime += Time.deltaTime;
      transform.localScale = Vector3.Lerp(_localScale, new Vector3(_localScale.x, targetSquashSize, _localScale.z), squashedCurve.Evaluate(_animationTime/squashedDuration));
    }
  }

  void OnTriggerEnter(Collider collider)
  {
    if ((playerLayer & 1 << collider.gameObject.layer) == 1 << collider.gameObject.layer)
    {
      _target = collider.transform;
      _followPlayer = true;
    }
  }

  // void OnTriggerExit(Collider collider)
  // {
  //   if ((playerLayer & 1 << collider.gameObject.layer) == 1 << collider.gameObject.layer)
  //   {
  //     _target = transform;
  //     _followPlayer = false;
  //   }
  // }

}
