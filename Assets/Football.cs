using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Football : MonoBehaviour
{
    private Vector3 throwTarget;

    public void setTarget(Vector3 target)
    {
        throwTarget = target;
    }

    public Vector3 getTarget()
    {
        return throwTarget;
    }
}
