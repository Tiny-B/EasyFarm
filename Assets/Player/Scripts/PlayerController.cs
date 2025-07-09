using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Animations;

namespace Player
{
  public class PlayerController : MonoBehaviour
  {
    #region References
    Animator animator;
    Rigidbody rb;
    Camera cam;
    PlayerStats stats;
    #endregion

    #region PlayerMovement
    [Header("PlayerMovement")]
    public float speed = 5f;
    public float rotationSpeed = 180f;  // degrees per second
    public float turnSmoothTime = 0.1f;
    #endregion

    #region Attack
    [Header("Attack")]
    bool isAttacking = false;
    public float basicAttackCooldown = 0.5f;
    public float basicAttackStaminaDrainAmount = -2f;
    public bool canTakeDamage = true;
    public float damageCooldown = 0.2f;
    #endregion

    #region Death
    [Header("Death")]
    bool isAlive = true;
    #endregion

    public AudioSource[] swordWooshes;

    //public MultiAimConstraint headAim;

    private void Awake()
    {
      animator = GetComponent<Animator>();
      rb = GetComponent<Rigidbody>();
      cam = Camera.main;
      stats = GetComponent<PlayerStats>();
    }

    void Update()
    {
      if (isAlive)
      {
        PlayerMovement();
        Attack();
        Interact();

        if (Input.GetKeyDown(KeyCode.T)) // test damage
        {
          TakeDamage(-5f);
        }
      }

      if (Input.GetKeyDown(KeyCode.Escape))
      {
        //UIManager.instance.ToggleOptionsMenu();
      }
    }

    void TakeDamage(float amount)
    {
      if (canTakeDamage)
      {
        stats.UpdateCurrentHealth(amount);
        canTakeDamage = false;
        StartCoroutine(DamageCooldown());

        if (stats.health <= 0)
        {
          isAlive = false;
        }
      }
    }
    IEnumerator DamageCooldown()
    {
      yield return new WaitForSeconds(damageCooldown);
      canTakeDamage = true;
    }

    #region PlayerControls
    void Interact()
    {
      if (Input.GetKeyDown(KeyCode.E))
      {
        Debug.Log("Fire1 pressed");
        //animator.SetTrigger("interact");
      }
    }

    void Attack()
    {
      if (!isAttacking)
      {
        if (Input.GetMouseButtonDown(0))
        {
          //int r = Random.Range(0, swordWooshes.Length);
          //swordWooshes[r].Play();

          isAttacking = true;
          StartCoroutine(AttackCooldown());
          animator.SetTrigger("attack");

          stats.UpdateCurrentStamValue(basicAttackStaminaDrainAmount);
        }
      }
    }
    private IEnumerator AttackCooldown()
    {
      yield return new WaitForSeconds(basicAttackCooldown);
      isAttacking = false;
    }

    void PlayerMovement()
    {
      Vector3 moveDir = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")).normalized;

      transform.position += moveDir * speed * Time.deltaTime;

      if (moveDir.sqrMagnitude > 0.001f)
      {
        Quaternion targetRot = Quaternion.LookRotation(moveDir, Vector3.up);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, rotationSpeed * Time.deltaTime);

        animator.SetBool("isMoving", true);
      }
      else
      {
        animator.SetBool("isMoving", false);
      }
    }
    #endregion
  }
}
