using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float MaxHealth;
    [SerializeField] private bool StartWithMaxHealth;
    [SerializeField] public float Health;

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
        
    }
}
