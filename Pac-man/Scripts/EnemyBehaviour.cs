using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBehaviour : MonoBehaviour
{
    public Enemy enemy;
    public float duration;
    private void Awake() {
        enemy = GetComponent<Enemy>();
        enabled = false;
    }

    public void Enable()
    {
        Enable(duration);
    }
    //this enable has a duration so that it can change states after a set duration of time(using the same duration of the original pacman)
    public virtual void Enable(float duration)
    {
        enabled = true;
        CancelInvoke();
        Invoke(nameof(Disable), duration);
    }

    public virtual void Disable()
    {
        enabled = false;
        CancelInvoke();
    }
}
