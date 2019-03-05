using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int MaxHealth;
    [SerializeField] private bool StartWithMaxHealth;
    [SerializeField] private int Health;
    private int damage = 0;
    public Animator animator;
    private bool x = false;

    // Start is called before the first frame update
    void Start()
    {
        if (StartWithMaxHealth)
        {
            Health = MaxHealth;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Health -= damage;
        damage = 0;
        // If an outside source sets or adds damage, take that damage immediately and wait for another update to it
        if (Input.GetButtonDown("Jump") && !x)
        {
            x = true;
            Debug.Log("no");
            Ouch(5);
        }


    }
    public void Ouch(int dmg)
    {
        // Flinch effect
        damage += dmg;
        Debug.Log("yeah");
        StartCoroutine(waiter());

        Debug.Log("yeah");
        x = false;
    }

    IEnumerator waiter()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        //Wait for 1
        yield return new WaitForSecondsRealtime(1);
        GetComponent<SpriteRenderer>().color = Color.white;
    }
}
