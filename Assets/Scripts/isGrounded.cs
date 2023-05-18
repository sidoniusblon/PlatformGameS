using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isGrounded : MonoBehaviour
{
    [SerializeField] LayerMask layer;
    public static bool isGround;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float JumpPower=300;
    [SerializeField] Animator anim;
    [SerializeField] AudioSource jump;
    private void Awake()
    {
        jump.volume=  AudioTest.Instance.vol;
    }
    private void FixedUpdate()
    {if (!GameManager.Instance.isPlaying) return;
        Raycast();
        Jump();
    }
    void Raycast()
    {
        RaycastHit2D raycast = Physics2D.CircleCast(transform.position,0.5f, Vector2.down, 0.015f, layer); 
        if (raycast.collider) { isGround = true; }
        else isGround = false;
    }
    void Jump()
    {
        if (isGround && Input.GetKey(KeyCode.Space))
        {
            jump.Play();

            rb.AddForce(Vector2.up * JumpPower);
            anim.SetBool("Jumping", true);
        }
        else if (isGround) { anim.SetBool("Falling", false); }
        else if (rb.velocity.y < 0)
        {
            anim.SetBool("Falling", true);
            anim.SetBool("Jumping", false);
        }
       

        
    }
}
