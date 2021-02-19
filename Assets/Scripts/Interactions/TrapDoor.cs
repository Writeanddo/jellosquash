using UnityEngine;
using TMPro;

public class TrapDoor : MonoBehaviour
{
  public GameObject trapDoor;
  public GameObject ruptured;
  public LayerMask playerLayer;
  [Tooltip("This value differs in different levels.")]
  public float minimumMass;
  [Tooltip("This value differs in different levels.")]
  public int dropJellyCount;
  public BoxCollider disableCollider;

  public TextMeshPro massText;

  void Start() => massText.text = minimumMass.ToString("f1");
  void Update() => massText.gameObject.transform.parent.transform.LookAt(Camera.main.transform);

  void OnTriggerEnter(Collider collider)
  {
    if ((playerLayer & 1 << collider.gameObject.layer) == 1 << collider.gameObject.layer)
    {
      Player player = collider.GetComponent<Player>();
      if (player.attack)
      {
        if (player.transform.localScale.x >= minimumMass)
        {
          DestroyTrapDoor();
          player.RuptureDropJelly(dropJellyCount);
          player.DestroyItem();
        }
      }
    }
  }

  private void DestroyTrapDoor()
  {
    trapDoor.GetComponent<MeshRenderer>().enabled = false;
    disableCollider.enabled = false;
    // GameObject fragments = Instantiate(ruptured, transform.position + transform.localScale.x * transform.right *offset, transform.rotation);
    GameObject fragments = Instantiate(ruptured, trapDoor.transform.position, trapDoor.transform.rotation);
    fragments.transform.localScale = transform.localScale;
  }
}
