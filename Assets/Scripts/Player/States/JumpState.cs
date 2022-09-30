using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class JumpState : PlayerState
{
    public JumpState(Animator _anim) : base(_anim)
    {
        anim = _anim;
    }

    public override void enter()
    {
        Debug.Log("Enter Jump");
    }

    public override PlayerState handleInput(InputAction.CallbackContext context)
    {
        if (context.action.name == "Jump")
            if (context.performed)
                return new HoveringState(anim);
        
        return null;
    }

    public override void update()
    {
        base.update();
    }

    public override void exit()
    {
        Debug.Log("Exit Jump");
    }
}
