using UnityEngine;
using UnityEngine.VFX;

public class Celebration : MonoBehaviour
{
  // Start is called before the first frame update
  public Cinemachine.CinemachineVirtualCamera winCam;
  public Cinemachine.CinemachineFreeLook playerCam;
  public ParticleSystem TorchParticleLeft;
  public ParticleSystem TorchParticleRight;
  public VisualEffect firework;

  [ColorUsage(false, true)]
  public Color unpressed;
  [ColorUsage(false, true)]
  public Color pressed;

  public Material finalPlate;
  public Transform spawn;
  public AudioClip pressurePlate;
  public AudioClip torchAudio;
  public float animationDuration = 3.0f;
  public AnimationCurve animationCurve;

  private float _animationTime;
  private Vector3 _originalPos;
  private bool _trigger = false;
  private int colorProperty;

  void Start()
  {
    _animationTime = 0.0f;
    _originalPos = transform.position;
    colorProperty = Shader.PropertyToID("_PressurePlateColor");
    finalPlate.SetColor(colorProperty, unpressed);
    TorchParticleLeft.Stop();
    TorchParticleRight.Stop();
    firework.Stop();
  }
  
  void Update()
  {
    if ((_animationTime < animationDuration && _trigger) || (_animationTime > 0.0f && !_trigger))
    {
      float evaluatedTime = animationCurve.Evaluate(_animationTime);

      if (_trigger) _animationTime += Time.deltaTime;
      else _animationTime -= Time.deltaTime;
      finalPlate.SetColor(colorProperty, Color.Lerp(unpressed, pressed, evaluatedTime));
      transform.position = Vector3.Lerp(_originalPos, _originalPos + Vector3.down*0.5f, evaluatedTime);

    }
  }

  void OnTriggerEnter(Collider collider)
  {
    _trigger = true;
    firework.Play();
    TorchParticleLeft.Play();
    TorchParticleRight.Play();

    winCam.Priority = 10;
    winCam.MoveToTopOfPrioritySubqueue();

    SoundFX.source.PlayOneShot(torchAudio);
    SoundFX.source.PlayOneShot(pressurePlate);
  }

  void OnTriggerExit(Collider collider)
  {
    _trigger = false;
    firework.Stop();
    TorchParticleLeft.Stop();
    TorchParticleRight.Stop();

    winCam.Priority = 6;
    playerCam.MoveToTopOfPrioritySubqueue();
  }
}
