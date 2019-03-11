using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogJump : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D m_Rigidbody2D;
    [SerializeField] private float JumpForce = 7f;
    [SerializeField] private float HorizontalJumpForce = 5f;

    void Start()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void JumpYouIdiot(bool right)
    {
        if (right)
        {
            Move(HorizontalJumpForce, JumpForce);
        }
        else
        {
            Move(-HorizontalJumpForce, JumpForce);
        }
    }

    private void Move(float xForce, float yForce)
    {
        m_Rigidbody2D.velocity = new Vector3(xForce, yForce);
        m_Rigidbody2D.AddForce(new Vector2(xForce, yForce));
    }
}
