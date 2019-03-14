using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] public int MaxHealth;
    [SerializeField] private bool StartWithMaxHealth;
    [SerializeField] public int Health;
    [SerializeField] private float CooldownTime;
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

    }

    public void TakeDamage()
    {
        if (Time.time > nextTime)
        {
            //Debug.Log("no");
            nextTime = Time.time + CooldownTime;
            Ouch(1);
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
