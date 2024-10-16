using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class movement : MonoBehaviour
{
    private bool candash;
    private bool isdashing;
    private float dashpower = 24f;
    private float dashtime = 0.2f;
    private float dashcooldown = 1f;






    private float horizontal;
    private float speed = 8f;
    private float jumpingpower = 16f;
    private bool isfacingright;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundcheck;
    [SerializeField] private LayerMask groundlayer;
    [SerializeField] private TrailRenderer tr;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isdashing)
        {
            return;
        }


        horizontal = Input.GetAxisRaw("Horizontal");

        flip();

        if(Input.GetButtonDown("Jump") && isgrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingpower);
        }
        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);

        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && candash) {
        
            StartCoroutine(DASH());




        }


    }

    private void FixedUpdate()
    {

        if (isdashing)
        {
            return;
        }



        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private bool isgrounded()
    {
        return Physics2D.OverlapCircle(groundcheck.position,0.2f,groundlayer);
    }
    


    private void flip()
    {
        if (isfacingright && horizontal < 0f || !isfacingright && horizontal > 0f) 
        {

            isfacingright = !isfacingright;
            Vector3 localscale = transform.localScale;
            localscale.x *= -1f;
            transform.localScale = localscale;




        }
    }

    private IEnumerator DASH()
    {
        candash = false;
        isdashing = true;
        float originalgravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashpower, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashtime);
        tr.emitting = false;
        rb.gravityScale = originalgravity;
        isdashing = false;
        yield return new WaitForSeconds(dashcooldown);
        candash = true;
    }

}


