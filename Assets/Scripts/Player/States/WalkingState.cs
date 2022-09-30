using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WalkingState : PlayerState
{
    public WalkingState(Animator _anim) : base(_anim)
    {
        anim = _anim;
    }

    public override void enter()
    {
        Debug.Log("Enter Walk");
    }

    public override PlayerState handleInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            switch (context.action.name)
            {
                case "Move":
                    update();
                    break;
                case "Attack":
                    return new AttackState(anim);
                case "Jump":
                    return new JumpState(anim);
            }
        }

        return null;
    }

    public override void update()
    {
        Debug.Log("Walking!");
    }

    public override void exit()
    {
        Debug.Log("Exit Walk");
    }
}
