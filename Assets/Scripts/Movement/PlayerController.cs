using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : EntityMovement {

    public Timer fallthroughTimer;
    public float disableDuration;

    private bool isJumping = false;
    private bool isFallingThrough = false;

    public float lvlObjRadius;
    private LevelObject currLvlObj;

    public override void Initialize() {
        base.Initialize();
        fallthroughTimer = new Timer("fallthroughTimer", disableDuration, true, DisableFallthrough);

        if (Platformed && Input.GetAxisRaw("Vertical") < 0) {
            isFallingThrough = true;
        }
    }


    private void Update() {
        if (!isClimbing)
        {
            currentSpeed = Input.GetAxisRaw("Horizontal") * maxSpeed;
            if (currentSpeed != 0f && !owner.MyAnimator.GetBool("Walking"))
            {
                owner.MyAnimator.SetBool("Walking", true);
            }
            else if (currentSpeed == 0f && owner.MyAnimator.GetBool("Walking"))
            {
                owner.MyAnimator.SetBool("Walking", false);
            }

            if (Platformed && Input.GetAxisRaw("Vertical") < 0)
            {
                isFallingThrough = true;
            }
            if (canClimb && Input.GetAxisRaw("Vertical") > 0 && climbPoint.position.y <= (myLadder.ladderTop - 0.01f))
            {
                myLadder.GrabLadder(gameObject);
            }
            if (canClimb && Input.GetAxisRaw("Vertical") < 0 && climbPoint.position.y >= (myLadder.ladderBot + 0.01f))
            {
                myLadder.GrabLadder(gameObject);
            }
        }
        if (isClimbing)
        {
            transform.position = new Vector2(myLadder.transform.position.x, (transform.position.y + myBody.velocity.y * Time.deltaTime));
            if (Input.GetAxisRaw("Vertical") > 0)
            {
                if (climbPoint.position.y > (myLadder.ladderTop - 0.01f))
                {
                    myLadder.LetGoLadder(gameObject);
                }
                else
                {
                    Debug.Log("Climbing Up");
                    myLadder.ClimbUp(myClimber);
                    myBody.velocity = new Vector2(0f, ascendSpeed);
                }
            }
            if (Input.GetAxisRaw("Vertical") < 0)
            {
                if (climbPoint.position.y < (myLadder.ladderBot + 0.01f))
                {
                    myLadder.LetGoLadder(gameObject);
                }
                else
                {
                    Debug.Log("Climbing Down");
                    myLadder.ClimbDown(myClimber);
                    myBody.velocity = new Vector2(0f, -descendSpeed);
                }
            }
            if (Input.GetAxisRaw("Vertical") == 0)
            {
                //Debug.Log("Clinging On");
                myLadder.ClimbHold(myClimber);
                myBody.velocity = Vector2.zero;
            }
        }


        CheckFacing();
        TryJump();
        Fallthrough(isFallingThrough);
        fallthroughTimer.UpdateClock();
        
        if(!Grounded && !Platformed)
        {
            owner.MyAnimator.SetBool("InAir", true);
        }
        if(Grounded || Platformed)
        {
            owner.MyAnimator.SetBool("InAir", false);
        }

        //Debug.Log(Grounded + " is the status of Grounded");
        //Debug.Log(Platformed + " is the status of platformed");

        FindNearestLevelObject();

        /*--Use Level Object--*/
        if (Input.GetKeyDown(KeyCode.F) && currLvlObj != null && currLvlObj.UseRestrictionsMet())        {            currLvlObj.ActivationFunction();        } 
        if (Input.GetKeyDown(KeyCode.O))        {            Debug.Log("5 Keys Added and 20 Dollars Added");            StatAdjustmentManager.AddStaticPlayerStatAdjustment(Constants.BaseStatType.Keys, 5);
            StatAdjustmentManager.AddStaticPlayerStatAdjustment(Constants.BaseStatType.Money, 20);
        }


    }
    private void FindNearestLevelObject()
    {
        Collider2D[] collStockpile = Physics2D.OverlapCircleAll(transform.position, lvlObjRadius);
        float smallestDistance = lvlObjRadius;
        for (int i = 0; i < collStockpile.Length; i++)
        {
            if (collStockpile[i].gameObject.tag == "LevelObject"/* && Vector2.Distance(transform.position, collStockpile[i].transform.position) < smallestDistance*/)
            {
                smallestDistance = Vector2.Distance(transform.position, collStockpile[i].transform.position);
                currLvlObj = collStockpile[i].gameObject.GetComponent<LevelObject>();
                //Debug.Log(currLvlObj);
            }
        }
    }

    protected override void Move() {
        myBody.velocity = new Vector2(currentSpeed, myBody.velocity.y);
        Jump();
    }


    private void TryJump() {
        if (Input.GetButtonDown("Jump") && (Grounded || Platformed || isClimbing)) {
            if (isClimbing)
            {
                myLadder.LetGoLadder(gameObject);
            }
            if (Grounded || Platformed)
            {
                owner.MyAnimator.SetTrigger("Jumping");
            }
            isJumping = true;
        }
    }

    private void Jump() {
        if (isJumping) {
            myBody.AddForce(Vector2.up * jumpForce);
            isJumping = false;
        }
    }

    private void DisableFallthrough() {
        isFallingThrough = false;
    }

}
