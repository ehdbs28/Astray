using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasTargetDecision : AIDecision
{
    public override bool MakeADecision()
    {
        return _controller.ActionData.TargetSpotted && _controller.ActionData.Target != null;
    }
}
