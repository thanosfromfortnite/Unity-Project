using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] public int MaxHealth;
    [SerializeField] private bool StartWithMaxHealth;
    [SerializeField] public int Health;
    [SerializeField] private float CooldownTime;
    [SerializeField] public int InstantDeathHeight;
    private float nextTime;
    public Animator animator;

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
        if (gameObject.transform.position.y < InstantDeathHeight)
        {
            TakeDamage(false, 5);
        }
    }

    public void TakeDamage(bool direction, int dmg = 1, float knockbackForceX = 600f, float knockbackforceY = 400f)
    {
        if (Time.time > nextTime)
        {
            //Debug.Log("no");
            nextTime = Time.time + CooldownTime;
            Ouch(dmg);
            if (direction)
            {
                Knockback(knockbackForceX, knockbackforceY);
            }
            else
            {
                Knockback(-knockbackForceX, knockbackforceY);
            }
        }
        if (Health > 5)
        {
            Health = 5;
        }
        if (Health <= 0)
        {
            SceneManager.LoadScene("oof");
        }
    }

    void Ouch(int dmg)
    {
        Health -= dmg;
        //Debug.Log("yeah");
        StartCoroutine(Waiter());
        Debug.Log(Health);
        
    }
    
    void Knockback(float x, float y)
    {
        gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(x, y));
    }

    IEnumerator Waiter()
    {
        for (int i = 0; i < 7; i++)
        {
            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.3f);
            //Wait for 1
            yield return new WaitForSecondsRealtime(0.05f);
            GetComponent<SpriteRenderer>().color = Color.white;
            yield return new WaitForSecondsRealtime(0.05f);
        }
        GetComponent<SpriteRenderer>().color = Color.white;
    }
    IEnumerator Cooldown(float time)
    {
        yield return new WaitForSecondsRealtime(time);
    }
}
