using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
	[SerializeField] private float m_JumpForce = 250f;							// Amount of force added when the player jumps.
    [SerializeField] private float m_WallJumpForce = 1000f;
	[Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;			// Amount of maxSpeed applied to crouching movement. 1 = 100%
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;	// How much to smooth out the movement
	[SerializeField] private bool m_AirControl = false;							// Whether or not a player can steer while jumping;
	[SerializeField] private LayerMask m_WhatIsGround;                          // A mask determining what is ground to the character
    [SerializeField] private Transform m_LeftCheck;
    [SerializeField] private Transform m_RightCheck;
    [SerializeField] private Transform m_GroundCheck;							// A position marking where to check if the player is grounded.
	[SerializeField] private Transform m_CeilingCheck;							// A position marking where to check for ceilings
	[SerializeField] private Collider2D m_CrouchDisableCollider;				// A collider that will be disabled when crouching
    [SerializeField] private float m_BunnyhopAccelerationRate = 2000000f;
    [SerializeField] private float m_BunnyhopDeaccelerationRate = 0.999999f;

	const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
	private bool m_Grounded;            // Whether or not the player is grounded.
	const float k_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
    const float k_LeftRadius = .3f;
    private bool m_sideWinded;
    private float m_bunnyHopMultiplier = 1f;
    const float k_RightRadius = .3f;
    private Rigidbody2D m_Rigidbody2D;
	private bool m_FacingRight = true;  // For determining which way the player is currently facing.
	private Vector3 m_Velocity = Vector3.zero;

	[Header("Events")]
	[Space]

	public UnityEvent OnLandEvent;

	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }

	public BoolEvent OnCrouchEvent;
	private bool m_wasCrouching = false;

	private void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();

		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();

		if (OnCrouchEvent == null)
			OnCrouchEvent = new BoolEvent();
	}

	private void FixedUpdate()
	{
		bool wasGrounded = m_Grounded;
        bool wasSided = m_sideWinded;
		m_Grounded = false;
        m_sideWinded = false;

		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        Collider2D[] lColliders = Physics2D.OverlapCircleAll(m_LeftCheck.position, k_LeftRadius, m_WhatIsGround);
        Collider2D[] rColliders = Physics2D.OverlapCircleAll(m_RightCheck.position, k_RightRadius, m_WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				m_Grounded = true;
				if (!wasGrounded)
					OnLandEvent.Invoke();
			}
		}
        for (int i = 0; i < lColliders.Length; i ++)
        {
            if (lColliders[i].gameObject != gameObject)
            {
                m_sideWinded = true;
                if (!wasSided) OnLandEvent.Invoke();
            }
        }
        for (int i = 0; i < rColliders.Length; i ++)
        {
            if (rColliders[i].gameObject != gameObject)
            {
                m_sideWinded = true;
                if (!wasSided) OnLandEvent.Invoke();
            }
        }
        /*if (m_Grounded)
        {
           Deaccelerate();
        }
        if (!wasGrounded && m_Grounded)
        {
            Accelerate();
        }*/
    }

    private void Accelerate()
    {
        m_bunnyHopMultiplier *= m_BunnyhopAccelerationRate;
    }

    private void Deaccelerate()
    {
        m_bunnyHopMultiplier *= m_BunnyhopDeaccelerationRate;
        if (m_bunnyHopMultiplier < 1f)
        {
            m_bunnyHopMultiplier = 1f;
        }
    }


	public void Move(float move, bool crouch, bool jump)
	{
		// If crouching, check to see if the character can stand up
		if (!crouch)
		{
			// If the character has a ceiling preventing them from standing up, keep them crouching
			if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
			{
				crouch = true;
			}
		}

		//only control the player if grounded or airControl is turned on
		if (m_Grounded || m_AirControl || m_sideWinded)
		{

			// If crouching
			if (crouch)
			{
				if (!m_wasCrouching)
				{
					m_wasCrouching = true;
					OnCrouchEvent.Invoke(true);
				}

				// Reduce the speed by the crouchSpeed multiplier
				move *= m_CrouchSpeed;

				// Disable one of the colliders when crouching
				if (m_CrouchDisableCollider != null)
					m_CrouchDisableCollider.enabled = false;
			} else
			{
				// Enable the collider when not crouching
				if (m_CrouchDisableCollider != null)
					m_CrouchDisableCollider.enabled = true;

				if (m_wasCrouching)
				{
					m_wasCrouching = false;
					OnCrouchEvent.Invoke(false);
				}
			}

			// Move the character by finding the target velocity
			Vector3 targetVelocity = new Vector2(move * 10f * m_bunnyHopMultiplier, m_Rigidbody2D.velocity.y);
			// And then smoothing it out and applying it to the character
			m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

			// If the input is moving the player right and the player is facing left...
			if (move > 0 && !m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}
			// Otherwise if the input is moving the player left and the player is facing right...
			else if (move < 0 && m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}
		}
        // If the player should jump...
        if (m_sideWinded && move != 0 && !m_Grounded)
        {
            if (jump)
            {
                if (m_FacingRight)
                {
                    m_sideWinded = false;
                    m_Rigidbody2D.AddForce(new Vector2(-m_WallJumpForce, m_JumpForce));
                }
                else
                {
                    m_sideWinded = false;
                    m_Rigidbody2D.AddForce(new Vector2(m_WallJumpForce, m_JumpForce));
                }
            }
            m_Rigidbody2D.AddForce(new Vector2(0f, 50f));
        }
        else if (m_Grounded && jump && (!m_sideWinded || move == 0))
		{
			// Add a vertical force to the player.
			m_Grounded = false;
			m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
		}
	}


	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
