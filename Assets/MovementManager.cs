using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.TopDownEngine;

public class MovementManager : MonoBehaviour
{
    public CharacterAbility movement;
    public CharacterMovement movementAbility;
    public CharacterButtonActivation buttonAbility;
    public Animator characterAnimator;
    public TopDownController3D controller;
    Vector3 stop;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void toggleMovement()
    {
        stop = new Vector3(0f, 0f, 0f);
        if (movement.AbilityPermitted)
        {
            /*characterAnimator.SetBool("Walking", false);
            characterAnimator.SetBool("Running", false);*/
            movementAbility.SetMovement(Vector2.zero);
            movementAbility.MovementForbidden = true;
            //buttonAbility.PermitAbility(false);
            movement.PermitAbility(false);

        }
        else
        {
            movementAbility.MovementForbidden = false;
            //buttonAbility.PermitAbility(true);
            movement.PermitAbility(true);
        }
    }

    public void disableMovement()
    {
        movementAbility.SetMovement(Vector2.zero);
        buttonAbility.PermitAbility(false);
        movementAbility.MovementForbidden = true;
    }

    public void enableMovement()
    {
        buttonAbility.PermitAbility(true);
        //movement.PermitAbility(true);
        movementAbility.MovementForbidden = false;
    }
}
