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
    private int x = 0;

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
        
    }
    public void Ouch(int dmg)
    {
        // Flinch effect
        damage += dmg;
        GetComponent<SpriteRenderer>().material.SetFloat("_FlashAmount", 0.8f);
        if (x > 5)
        {
            x = 0;
            GetComponent<SpriteRenderer>().material.SetFloat("_FlashAmount", 0);
            Debug.Log("yeah");
        }
    }
}
