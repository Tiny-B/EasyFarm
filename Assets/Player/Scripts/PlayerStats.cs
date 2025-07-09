using UnityEngine;

namespace Player
{
  public class PlayerStats : MonoBehaviour
  {
    public float health = 100f;
    public float stamina = 100f;

    public void UpdateCurrentStamValue(float amt)
    {
      stamina += amt;
      Debug.Log($"Stamina: {stamina}");
    }

    public void UpdateCurrentHealth(float amt)
    {
      health += amt;
      Debug.Log($"Health: {health}");
    }

  }
}
