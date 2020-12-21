using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PaladinAnimatorController : MonoBehaviourPun
{
    public Animator paladinAnimator;
    private float movespeed;
    private enum Direction { Forward, Backward, Stopped };

    // Start is called before the first frame update
    void Start()
    {

        paladinAnimator.SetFloat("Health", 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            CheckMovement();
            CheckAttack();
        }
    }
//+----------------------------------------+//
//            Attack Methods                //
//+----------------------------------------+//
    private void CheckAttack()
    {
        if (ShouldAttack())
        {
            triggerAttack(2);
        }
    }
    
    private void triggerAttack(int attackNumber)
    {
        Debug.Log(attackNumber);
        Transform handObject = transform.Find("hips/spine/chest/R_shoulder/R_arm/R_elbow/R_wrist/R_middle1");
        PhotonNetwork.Instantiate(Path.Combine("Prefabs", "Weapons", "ThrowingDagger"), handObject.position, transform.rotation);
        paladinAnimator.SetTrigger("Attack");
        paladinAnimator.SetInteger("AttackNumber", attackNumber);
    }

    private bool ShouldAttack()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(!paladinAnimator.GetBool("IsRetreating"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

//+----------------------------------------+//
//            Movement Methods              //
//+----------------------------------------+//
    private void CheckMovement()
    {
        if (ShouldAdvance())
        {
            Move(Direction.Forward);

        }
        else if (ShouldRetreat())
        {
            Move(Direction.Backward);
        }
        else
        {
            Move(Direction.Stopped);
        }
    }

    private void Move(Direction direction)
    {
        switch(direction)
        {
            case Direction.Forward:
                paladinAnimator.SetBool("IsAdvancing", true);
                movespeed = 3.5f;
                transform.position += transform.forward * movespeed * Time.deltaTime;

                break;
            case Direction.Backward:
                paladinAnimator.SetBool("IsRetreating", true);
                movespeed = 2.6f;
                transform.position -= transform.forward * movespeed * Time.deltaTime;

                break;
            case Direction.Stopped:
                paladinAnimator.SetBool("IsAdvancing", false);
                paladinAnimator.SetBool("IsRetreating", false);
                break;
        }


    }

    private bool ShouldAdvance()
    {
        if(Input.GetKey(KeyCode.D))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool ShouldRetreat()
    {
        if (Input.GetKey(KeyCode.A))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
