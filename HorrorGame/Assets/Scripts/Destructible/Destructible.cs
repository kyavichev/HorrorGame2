using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// a destructible is something that can take damage and be destroyed.
// we can subclass this to different sorts of behaviors when we are
// destroyed or take damage, like play particles or animations.
// destructibles can also be healed, up to their maximum hit points.

public class Destructible : MonoBehaviour
{
    private int currentHealthPoints;
    public int maxHealthPoints = 3;

    public float invincibleDuration = 0.25f;    // Seconds
    private float invincibleTimer = 0;          // Seconds

    public Animator animator;

    public Team team;


    // Start is called before the first frame update
    void Start()
    {
        currentHealthPoints = maxHealthPoints;
    }

    // Update is called once per frame
    void Update()
    {
        if (invincibleTimer > 0)
        {
            invincibleTimer -= Time.deltaTime;
            invincibleTimer = Mathf.Max(invincibleTimer, 0);
        }
    }


    public void TakeDamage(int damagePoints)
    {
        if(invincibleTimer > 0)
        {
            return;
        }

        currentHealthPoints -= damagePoints;
        currentHealthPoints = Mathf.Max(currentHealthPoints, 0); // Making sure current health points are not less than 0
        // Same as the live above
        //currentHealthPoints = Mathf.Clamp(currentHealthPoints, 0, maxHealthPoints);

        invincibleTimer = invincibleDuration;

        if (animator)
        {
            animator.SetTrigger("Damage");
        }

        if (currentHealthPoints == 0)
        {
            Die();
        }
    }


    public void Heal(int healPoints)
    {
        currentHealthPoints += healPoints;
        currentHealthPoints = Mathf.Min(currentHealthPoints, maxHealthPoints);
    }


    public void Die()
    {
        Destroy(gameObject);
    }


    public int GetCurrentHealthPoints()
    {
        return currentHealthPoints;
    }


    public bool IsHurt()
    {
        if(currentHealthPoints < maxHealthPoints)
        {
            return true;
        }

        return false;
    }
}
