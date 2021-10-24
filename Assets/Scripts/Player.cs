using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed;
    public float jumpheight;
    public Transform Groundcheck;
    bool IsGrounded;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckGround();
        if (Input.GetAxis("Horizontal") == 0 && IsGrounded)
            anim.SetInteger("State", 1);
        else 
        {
            Flip();
            if (IsGrounded)
                anim.SetInteger("State", 2);
        }
        
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, rb.velocity.y);
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded)
            rb.AddForce(transform.up * jumpheight, ForceMode2D.Impulse);
    }
    void Flip() 
    {
        if (Input.GetAxis("Horizontal") > 0)
            transform.localRotation = Quaternion.Euler(0,0,0);
        if (Input.GetAxis("Horizontal") < 0)
            transform.localRotation = Quaternion.Euler(0, 180, 0);
    }
    void CheckGround() 
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(Groundcheck.position, 0.2f);
        IsGrounded = colliders.Length > 1;
        if (!IsGrounded)
            anim.SetInteger("State",3);
    }
}
