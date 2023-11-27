using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Flow_Base : MonoBehaviour
{
    public abstract void Enter();
    public abstract void Excute(Flow_Control manager);
    public abstract void Exit();
}
