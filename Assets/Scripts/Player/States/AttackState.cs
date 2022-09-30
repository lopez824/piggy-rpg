using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AttackState : PlayerState
{
    public AttackState(Animator _anim) : base(_anim)
    {
        anim = _anim;
    }

    public override void enter()
    {
        Debug.Log("Enter Attack");
    }

    public override PlayerState handleInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            switch (context.action.name)
            {
                case "Attack":
                    update();
                    break;
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
        Debug.Log("Attack!");
    }

    public override void exit()
    {
        Debug.Log("Exit Attack");
    }
}
