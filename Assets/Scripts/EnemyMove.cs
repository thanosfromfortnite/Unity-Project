using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    private Rigidbody2D m_Rigidbody2D;
    [SerializeField] private float JumpForce = 7f;
    [SerializeField] private float HorizontalJumpForce = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveYouIdiot(bool right)
    {
        if (right)
            Move(HorizontalJumpForce, 0f, false);
        else
            Move(-HorizontalJumpForce, 0f, false);
    }

    public void JumpYouIdiot(bool right)
    {
        if (right)
        {
            Move(HorizontalJumpForce, JumpForce, true);
        }
        else
        {
            Move(-HorizontalJumpForce, JumpForce, true);
        }
    }

    private void Move(float xForce, float yForce, bool jump)
    {
        if (jump)
        {
            m_Rigidbody2D.velocity = new Vector3(xForce, yForce);
        }
        else
        {
            m_Rigidbody2D.velocity = new Vector3(xForce, m_Rigidbody2D.velocity.y);
        }
        m_Rigidbody2D.AddForce(new Vector2(xForce, yForce));
    }
}
