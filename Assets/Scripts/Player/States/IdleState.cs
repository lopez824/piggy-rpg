using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class IdleState : PlayerState
{
    public IdleState(Animator _anim) : base(_anim)
    {
        anim = _anim;
    }

    public override void enter()
    {
        Debug.Log("Enter Idle");
    }

    public override PlayerState handleInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            switch (context.action.name)
            {
                case "Attack":
                    return new AttackState(anim);
                case "Move":
                    return new WalkingState(anim);
                case "Jump":
                    return new JumpState(anim);
            }
        }

        return null;
    }

    public override void update()
    {
        base.update();
    }

    public override void exit()
    {
        Debug.Log("Exit Idle");
    }
}
