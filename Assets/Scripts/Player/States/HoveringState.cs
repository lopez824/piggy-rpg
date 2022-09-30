using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HoveringState : PlayerState
{
    public HoveringState(Animator _anim) : base(_anim)
    {
        anim = _anim;
    }

    public override void enter()
    {
        Debug.Log("Enter Hover");
    }

    public override PlayerState handleInput(InputAction.CallbackContext context)
    {
        if (context.action.name == "Jump")
            if (context.canceled)
                return new IdleState(anim);

        return null;
    }

    public override void update()
    {
        base.update();
    }

    public override void exit()
    {
        Debug.Log("Exit Hover");
    }
}
