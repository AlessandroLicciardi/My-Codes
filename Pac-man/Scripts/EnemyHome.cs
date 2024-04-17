using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHome : EnemyBehaviour
{
    public Transform firstPos;
    public Transform secondPos;

    private void OnEnable() {
        StopAllCoroutines();
    }
    private void OnDisable() 
    {
        if(this.gameObject.activeSelf)
        {
            StartCoroutine(ExitHome());
        }
    }

    //coroutine to force the ghost to exit the "home"
    private IEnumerator ExitHome()
    {
        this.enemy.movement.SetDirection(Vector2.up, true);
        this.enemy.movement.rigidBody.isKinematic = true;
        this.enemy.movement.enabled = false;

        Vector3 pos = this.transform.position;
        float _duration = 0.5f;
        float _timePassed = 0f;
        while(_timePassed < _duration)
        {
            Vector3 newPos = Vector3.Lerp(pos, this.firstPos.position, _timePassed/_duration);
            newPos.z = pos.z;
            this.enemy.transform.position = newPos;
            _timePassed += Time.deltaTime;
            yield return null;
        }

        _timePassed = 0f;

        while(_timePassed < _duration)
        {
            Vector3 newPos = Vector3.Lerp(this.firstPos.position, this.secondPos.position, _timePassed/_duration);
            newPos.z = pos.z;
            this.enemy.transform.position = newPos;
            _timePassed += Time.deltaTime;
            yield return null;
        }

        this.enemy.movement.SetDirection(new Vector2(Random.value < 0.5 ? -1.0f : 1.0f, 0), true);
        this.enemy.movement.rigidBody.isKinematic = false;
        this.enemy.movement.enabled = true;
    }
}
