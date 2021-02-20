using UnityEngine;
using UnityEngine.VFX;

public class Celebration : MonoBehaviour
{
	// Start is called before the first frame update
	public GameObject fireWorksPerfab;
	public ParticleSystem TorchParticleLeft;
	public ParticleSystem TorchParticleRight;
	VisualEffect firework;

	[ColorUsage(false, true)]
  public Color unpressed;
  [ColorUsage(false, true)]
  public Color pressed;
	public Material finalPlate;
	private int colorProperty;
	public Transform spawn;
	public AudioClip pressurePlate;
	float duration = 3.0f;
	float timePassed;
	float lerpSpeed;
	Vector3 oriPos;
  bool down = false;

	void Start()
	{
    oriPos = transform.position;
		colorProperty = Shader.PropertyToID("_PressurePlateColor");
		finalPlate.SetColor(colorProperty, unpressed);
		firework = Instantiate(fireWorksPerfab, spawn.position, transform.rotation).GetComponent<VisualEffect>();
		TorchParticleLeft.Stop();
		TorchParticleRight.Stop();
		firework.Stop();
	}
	
	void Update()
	{
		if(timePassed < duration)
		{
			lerpSpeed = Mathf.Lerp(0, 1, timePassed/duration);
			timePassed += Time.deltaTime;
		}else timePassed = 0.0f;
	}
  void moveDown()
  {
    down = true;
    transform.position = Vector3.Lerp(oriPos, oriPos + Vector3.down*1f, lerpSpeed);
    finalPlate.SetColor(colorProperty, Color.Lerp(unpressed, pressed, lerpSpeed));
  }

  void moveUp()
  {
    transform.position = Vector3.Lerp(oriPos + Vector3.down, oriPos, lerpSpeed);
    finalPlate.SetColor(colorProperty, Color.Lerp(pressed, unpressed, lerpSpeed));
  }
	void OnTriggerenter()
	{
    down = true;
    if(down)
    {
      moveDown();
      down = false;
    }
		firework.Play();
		TorchParticleLeft.Play();
		TorchParticleRight.Play();
	}
	void OnTriggerExit()
	{
    down = false;
    if(!down)
    {
      moveUp();
      down = true;
    }
		firework.Stop();
		TorchParticleLeft.Stop();
		TorchParticleRight.Stop();
	}
}
