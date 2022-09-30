using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerState
{
    public Animator anim;

    public PlayerState(Animator _anim)
    {
        anim = _anim;
    }

    public virtual void enter() { }
    public virtual PlayerState handleInput(InputAction.CallbackContext context) { return this; }
    public virtual void update() { }
    public virtual void exit() { }
}
