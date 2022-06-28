using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyState
{
    public void StateEntered();
    public void StateUpdate();
    public void StateExit();
}
