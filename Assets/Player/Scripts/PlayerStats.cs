using UnityEngine;

namespace Player
{
  public class PlayerStats : MonoBehaviour
  {
    PlayerController controller;

    public float health = 100f;

    public float stamina = 100f;
    public float tiredThreshold = 10f;
    bool isTired = false;

    private void Awake()
    {
      controller = GetComponent<PlayerController>();
    }

    public void UpdateCurrentStamValue(float amt)
    {
      stamina += amt;
      Debug.Log($"Stamina: {stamina}");

      if ( controller != null )
      {
        if ( stamina <= tiredThreshold && !isTired)
        {
          controller.ChangeSpeed(true);
          isTired = true;
        }
        else if ( stamina > tiredThreshold && isTired)
        {
          controller.ChangeSpeed(false);
          isTired = false;
        }
      }
    }

    public void UpdateCurrentHealth(float amt)
    {
      health += amt;
      Debug.Log($"Health: {health}");
    }

  }
}
