using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
  public Transform[] gates;
  public float animationDuration = 1.0f;
  public AnimationCurve animationCurve;
  public float timeOffset = 0.2f;
  public Vector3 positionOffset = new Vector3(0.0f, -1.0f, 0.0f);
  public LayerMask acceptedLayer;

  public BoxCollider disableCollider;

  [ColorUsage(false, true)]
  public Color untriggeredColor;
  [ColorUsage(false, true)]
  public Color triggeredColor;

  private Vector3 _localPos;
  private Vector3[] _originalPos;
  private float _animationTime;
  private bool _trigger = false;

  private Material _pressurePlateMat;
  private int colorProperty;

  // Start is called before the first frame update
  void Start()
  {
    _animationTime = 0.0f;
    _originalPos = new Vector3[gates.Length];
    for (int g=0; g < gates.Length; g++)
      _originalPos[g] = gates[g].localPosition;

    _localPos = transform.localPosition;
    _pressurePlateMat = GetComponent<MeshRenderer>().material;
    colorProperty = Shader.PropertyToID("_PressurePlateColor");
  }

  // Update is called once per frame
  void Update()
  {
    if ((_animationTime < animationDuration && _trigger) || (_animationTime > 0.0f && !_trigger))
    {
      float evaluatedTime = animationCurve.Evaluate(_animationTime);

      if (_trigger) _animationTime += Time.deltaTime;
      else _animationTime -= Time.deltaTime;
      _pressurePlateMat.SetColor(colorProperty, Color.Lerp(untriggeredColor, triggeredColor, evaluatedTime));
      transform.localPosition = Vector3.Lerp(_localPos, _localPos + Vector3.down*0.5f, evaluatedTime);

      for (int g=0; g < gates.Length; g++)
        gates[g].localPosition = Vector3.Lerp(_originalPos[g], _originalPos[g] + positionOffset, evaluatedTime);

    }
  }

  void OnTriggerEnter(Collider collider)
  {
    if ((acceptedLayer & 1 << collider.gameObject.layer) == 1 << collider.gameObject.layer && !_trigger)
    {
      _trigger = true;
      disableCollider.enabled = false;
    }
  }

  void OnTriggerExit(Collider collider)
  {
    if ((acceptedLayer & 1 << collider.gameObject.layer) == 1 << collider.gameObject.layer && _trigger)
    {
      _trigger = false;
      disableCollider.enabled = true;
    }
  }
}