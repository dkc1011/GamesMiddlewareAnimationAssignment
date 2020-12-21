using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightAnimController : MonoBehaviourPun
{
    public Animator knightAnimator;
    private enum Direction { Forward, Backward, Stopped };
    private KnightIKControl knightIKControl;
    private float movespeed;
    private bool alive;

    // Start is called before the first frame update
    void Start()
    {
        knightIKControl = GetComponent<KnightIKControl>();
        knightAnimator.SetFloat("Health", 1.0f);
        alive = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            CheckMovement();
            CheckGetHit();
            CheckToggleArmed();


            if (Input.GetKeyDown(KeyCode.H))
            {
                knightAnimator.SetFloat("Health", 1.0f);
                alive = true;
            }
        }
    }

    //+----------------------------------------+//
    //            Arming Methods                //
    //+----------------------------------------+//
    private void CheckToggleArmed()
    {
        if (ShouldToggleArmed() && !ShouldAdvance() && !ShouldRetreat() && !ShouldGetHit())
        {
            ToggleArmed();
        }
    }
    private void ToggleArmed()
    {
        if (knightAnimator.GetBool("isArmed"))
        {
            knightAnimator.SetBool("isArmed", false);
        }
        else
        {
            knightAnimator.SetBool("isArmed", true);
        }
        
    }
    
    private bool ShouldToggleArmed()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    //+----------------------------------------+//
    //            Damage Methods                //
    //+----------------------------------------+//
    private void CheckGetHit()
    {
        if (ShouldGetHit())
        {
            triggerGetHit();
        }
    }
    
    private void triggerGetHit()
    {
        if (knightAnimator.GetFloat("Health") > 0)
        {
            knightAnimator.SetFloat("Health", knightAnimator.GetFloat("Health") - 0.2f);
            knightAnimator.SetTrigger("getHit");
        }
        else
        {
            alive = false;
        }


    }
    
    private bool ShouldGetHit()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            return true;
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
        if(alive)
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
    }
    private void Move(Direction direction)
    {
        switch (direction)
        {
            case Direction.Forward:
                knightAnimator.SetBool("IsAdvancing", true);
                movespeed = CalculateMoveSpeed(direction);
                transform.position += transform.forward * movespeed * Time.deltaTime;
                break;
            case Direction.Backward:
                knightAnimator.SetBool("IsRetreating", true);
                movespeed = CalculateMoveSpeed(direction);
                transform.position -= transform.forward * movespeed * Time.deltaTime;
                break;
            case Direction.Stopped:
                knightAnimator.SetBool("IsAdvancing", false);
                knightAnimator.SetBool("IsRetreating", false);
                break;
        }


    }

    private float CalculateMoveSpeed(Direction direction)
    {
        if(direction == Direction.Forward)
        {
            if(knightAnimator.GetFloat("Health") >= 0.8f || knightAnimator.GetFloat("Health") <= 1.0f)
            {
                return 3.6f;
            }
            else if(knightAnimator.GetFloat("Health") >= 0.5f && knightAnimator.GetFloat("Health") < 0.8f)
            {
                return 2.8f;
            }
            else if(knightAnimator.GetFloat("Health") > 0f && knightAnimator.GetFloat("Health") < 0.5f)
            {
                return 2.4f;
            }
            else
            {
                return 0;
            }
        }
        else
        {
            return 2.8f;
        }
    }

    private bool ShouldAdvance()
    {
        if(Input.GetKey(KeyCode.A))
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
        if (Input.GetKey(KeyCode.D))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
