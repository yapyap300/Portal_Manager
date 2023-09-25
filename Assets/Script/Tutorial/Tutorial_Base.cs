using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tutorial_Base : MonoBehaviour
{
    public abstract void Enter();
    public abstract void Excute(Tutorial_Control manager);
    public abstract void Exit();
}
