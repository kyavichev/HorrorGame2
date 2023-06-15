using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// a destructible is something that can take damage and be destroyed.
// we can subclass this to different sorts of behaviors when we are
// destroyed or take damage, like play particles or animations.
// destructibles can also be healed, up to their maximum hit points.
public class Destructible : MonoBehaviour
{
    public int maxHealthPoints = 3;
    private int _currentHealthPoints;

    public float invincibleDuration = 0.25f;    // Seconds
    private float _invincibleTimer = 0;          // Seconds

    public Animator animator;
    public AudioSource takeDamageAudioSource;

    public Team team;


    // Start is called before the first frame update
    void Start()
    {
        _currentHealthPoints = maxHealthPoints;
    }

    // Update is called once per frame
    void Update()
    {
        if (_invincibleTimer > 0)
        {
            _invincibleTimer -= Time.deltaTime;
            _invincibleTimer = Mathf.Max(_invincibleTimer, 0);
        }
    }


    public void TakeDamage(int damagePoints)
    {
        if (_invincibleTimer > 0)
        {
            return;
        }

        _currentHealthPoints -= damagePoints;
        _currentHealthPoints = Mathf.Max(_currentHealthPoints, 0); // Making sure current health points are not less than 0
        // Same as the line above
        //_currentHealthPoints = Mathf.Clamp(_currentHealthPoints, 0, maxHealthPoints);

        // Reset invincible timer
        _invincibleTimer = invincibleDuration;

        // current health points reached 0, then this game object dies
        if (_currentHealthPoints == 0)
        {
            Die();
        }
        else
        {
            // if there is animator attached, trigger taking damage animation
            if (animator)
            {
                animator.SetTrigger("Damage");
            }
            
            // Play taking damage audio
            takeDamageAudioSource.Play();
        }
    }


    public void Heal(int healPoints)
    {
        _currentHealthPoints += healPoints;
        _currentHealthPoints = Mathf.Min(_currentHealthPoints, maxHealthPoints);
    }


    // This function kills the game object
    public void Die()
    {
        //Destroy(gameObject);
    }


    // Returns current health point count
    public int GetCurrentHealthPoints()
    {
        return _currentHealthPoints;
    }

    // Helper function that returns true if current health points are less than maximum
    public bool IsHurt()
    {
        if(_currentHealthPoints < maxHealthPoints)
        {
            return true;
        }

        return false;
    }

    public bool IsAlive()
    {
        if(_currentHealthPoints > 0)
        {
            return true;
        }

        return false;
    }
}
