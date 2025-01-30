using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator playerAnim;
    public Rigidbody playerRigid;
    public float w_speed, wb_speed, olw_speed, rn_speed, ro_speed;
    public float jumpSpeed;
    public bool walking;
    public Transform playerTrans;

    void FixedUpdate()
    {
        if(Input.GetKey(KeyCode.W))
        {
            playerRigid.velocity = transform.forward * w_speed * Time.deltaTime;
        }
        if(Input.GetKey(KeyCode.S))
        {
            playerRigid.velocity = -transform.forward * wb_speed * Time.deltaTime;
        }
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            playerAnim.SetTrigger("Walk");
            playerAnim.ResetTrigger("Idle");
            walking = true;
        }
        if(Input.GetKeyUp(KeyCode.W))
        {
            playerAnim.ResetTrigger("Walk");
            playerAnim.SetTrigger("Idle");
            walking = false;
        }
        if(Input.GetKeyDown(KeyCode.S))
        {
            playerAnim.SetTrigger("Walk Back");
            playerAnim.ResetTrigger("Idle");
        }
        if(Input.GetKeyUp(KeyCode.S))
        {
            playerAnim.ResetTrigger("Walk Back");
            playerAnim.SetTrigger("Idle");
        }
        if(walking == true)
        {
            if(Input.GetKeyDown(KeyCode.LeftShift))
            {
                w_speed = w_speed + rn_speed;
                playerAnim.SetTrigger("Run");
                playerAnim.ResetTrigger("Walk");
            }
            if(Input.GetKeyUp(KeyCode.LeftShift))
            {
                w_speed = olw_speed;
                playerAnim.ResetTrigger("Run");
                playerAnim.SetTrigger("Walk");
            }
        }
        if (Input.GetKeyDown("space"))
        {
            playerRigid.AddForce(Vector3.up * jumpSpeed);
            playerAnim.SetTrigger("Jump");
            playerAnim.ResetTrigger("Idle");
        }
        if (Input.GetKeyUp("space"))
        {
            playerAnim.ResetTrigger("Jump");
            playerAnim.SetTrigger("Idle");
        }

    }
}
