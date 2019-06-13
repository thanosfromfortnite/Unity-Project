using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] public int MaxHealth;
    [SerializeField] private bool StartWithMaxHealth;
    [SerializeField] public int Health;
    [SerializeField] private float CooldownTime;
    private float nextTime;
    // Start is called before the first frame update
    void Start()
    {
        nextTime = Time.time;
        if (StartWithMaxHealth)
        {
            Health = MaxHealth;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(bool direction, int damage, float knockbackForceX = 300f, float knockbackforceY = 150f)
    {
        if (Time.time > nextTime)
        {
            //Debug.Log("no");
            nextTime = Time.time + CooldownTime;
            Ouch(damage);
            if (direction)
            {
                Knockback(-knockbackForceX, knockbackforceY);
            }
            else
            {
                Knockback(knockbackForceX, knockbackforceY);
            }
        }
    }

    void Ouch(int dmg)
    {
        Health -= dmg;
        //Debug.Log("yeah");
        StartCoroutine(Waiter());

    }

    void Knockback(float x, float y)
    {
        gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(x, y));
    }

    IEnumerator Waiter()
    {
        GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
        yield return new WaitForSecondsRealtime(0.05f);
        GetComponent<SpriteRenderer>().color = Color.white;
    }
    IEnumerator Cooldown(float time)
    {
        yield return new WaitForSecondsRealtime(time);
    }
}
