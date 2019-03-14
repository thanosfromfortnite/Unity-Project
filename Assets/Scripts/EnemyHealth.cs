using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] public int MaxHealth;
    [SerializeField] private bool StartWithMaxHealth;
    [SerializeField] public int Health;
    [SerializeField] private float CooldownTime;
    public Animator animator;
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

    public void TakeDamage(int damage)
    {
        if (Time.time > nextTime)
        {
            //Debug.Log("no");
            nextTime = Time.time + CooldownTime;
            Ouch(damage);
        }
    }

    void Ouch(int dmg)
    {
        Health -= dmg;
        //Debug.Log("yeah");
        StartCoroutine(Waiter());
        Debug.Log(Health);

    }

    IEnumerator Waiter()
    {
        GetComponent<SpriteRenderer>().color = new Color(2f, 2f, 2f);
        yield return new WaitForSecondsRealtime(0.5f);
        GetComponent<SpriteRenderer>().color = Color.white;
    }
    IEnumerator Cooldown(float time)
    {
        yield return new WaitForSecondsRealtime(time);
    }
}
