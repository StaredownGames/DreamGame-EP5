using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public float speed;
    public float turnSpeed;
    public float driftFactor;
    public float currentDriftFactor;

    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity.y > 10f || Input.GetKey(KeyCode.LeftShift))
        {
            currentDriftFactor = 1f;
        }
        else 
        {
            if (currentDriftFactor > driftFactor)
            {
                currentDriftFactor -= .001f;
            }
            else
            {
                currentDriftFactor = driftFactor;
            }
        }
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(transform.up * speed);
        }

        if (Input.GetKey(KeyCode.S) && rb.velocity.y > 0f)
        {
            rb.AddForce(-transform.up * speed);
        }

        if (rb.velocity != Vector2.zero)
        {
            rb.AddTorque(-Input.GetAxisRaw("Horizontal") * turnSpeed);
        }

        rb.velocity = ForwardVelocity() + RightwardVelocity() * currentDriftFactor;
    }

    Vector2 ForwardVelocity()
    {
        return transform.up * Vector2.Dot(rb.velocity, transform.up);
    }

    Vector2 RightwardVelocity()
    {
        return transform.right * Vector2.Dot(rb.velocity, transform.right);
    }
}
