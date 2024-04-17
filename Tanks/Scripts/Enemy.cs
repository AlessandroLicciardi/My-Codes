using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float range;
    private float detectionCheckDelay = 0.1f;
    public Transform target;
    public LayerMask playerlayer;
    
    private void Start()
    {
        StartCoroutine(DetectionCoroutine());
    }
    private void DetectionTarget()
    {
        if(target == null)
            CheckIfPlayerInRange();
        else if(target != null)
            DetectIfOutOfRange();
    }

    private void CheckIfPlayerInRange()
    {
        Collider2D collision = Physics2D.OverlapCircle(transform.position, range, playerlayer);
        if(collision != null)
        {
            target = collision.transform;
        }
    }

    private void DetectIfOutOfRange()
    {
        if(target == null || target.gameObject.GetComponent<Damageable>().CurrentHealth == 0 || Vector2.Distance(transform.position, target.position) > range)
        {
            target = null;
        }
    }

    IEnumerator DetectionCoroutine()
    {
        yield return new WaitForSeconds(detectionCheckDelay);
        DetectionTarget();
        StartCoroutine(DetectionCoroutine());
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, range);    
    }
}
