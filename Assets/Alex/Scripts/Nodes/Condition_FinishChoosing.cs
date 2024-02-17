using NodeCanvas.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Condition_FinishChoosing : ConditionTask
{
    protected override bool OnCheck()
    {
        return (GameManager.Instance.OptionManager.IsChoosing == false);
    }
}
