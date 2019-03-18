using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootScript : AttackScript
{
    private float nextTime;
    [SerializeField] public float AttackCooldown;
    [SerializeField] public float AttackSpeed;
    [SerializeField] public int Damage = 25;
    [SerializeField] public float horizontalVelocity = 0.5f;
    [SerializeField] public float verticalVelocity = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        nextTime = Time.time;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void Attack() {

        if (Time.time > nextTime)
        {
            nextTime = Time.time + AttackCooldown;
            Projectile x = gameObject.AddComponent<Projectile>();
            x.xForce = horizontalVelocity;
            x.yForce = verticalVelocity;
        }
    }

    /*IEnumerator Cooldown(float cooldown, float speed = 0)
    {
        yield return new WaitForSecondsRealtime(speed);
        yield return new WaitForSecondsRealtime(cooldown);
    }*/
}
