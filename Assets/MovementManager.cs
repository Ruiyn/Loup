using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.TopDownEngine;

public class MovementManager : MonoBehaviour
{
    public CharacterAbility movement;


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
        if (movement.AbilityPermitted)
        {
            movement.PermitAbility(false);
        }
        else
        {
            movement.PermitAbility(true);
        }
    }

    public void disableMovement()
    {
        movement.PermitAbility(false);
    }
}
