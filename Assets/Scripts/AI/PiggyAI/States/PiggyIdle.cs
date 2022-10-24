using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiggyIdle : PiggyState
{
    public PiggyIdle(PiggyAIController controller) : base (controller)
    {
        piggyController = controller;
    }

    public override void enter()
    {
        base.enter();
    }

    public override PiggyState handleEvent()
    {
        return base.handleEvent();
    }

    public override void update()
    {
        base.update();
    }

    public override void exit()
    {
        base.exit();
    }
}
