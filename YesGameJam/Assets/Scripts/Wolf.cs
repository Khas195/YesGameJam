using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf : MonoBehaviour {
	public float speed;
	public float aggroRange;
	public float grabRange;
	public GameObject model;
	public Mother mother = null;
	Child target = null;
	bool eatingChild = false;
	Rigidbody2D rigidbody;
	public float stunDuration = 5;
	public float curStun = 0;
	// Use this for initialization
	void Start () {
		rigidbody = this.GetComponent<Rigidbody2D>();
	}
	void OnDrawGizmos()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(transform.position, aggroRange);
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, grabRange);
	}
	// Update is called once per frame
	void Update () {
		if (curStun > 0) {
			curStun -= Time.deltaTime;
			return;
		}
		if (!eatingChild){
			target = FindClosetChild(aggroRange);
			if (target != null)
            {
                HuntChild();
            } else {
				rigidbody.velocity = Vector2.zero;
			}
        } else {
				rigidbody.velocity = Vector2.zero;
		}
	}
	void HandleFacing(Vector2 dir) {
		var scale = model.transform.localScale;
        if (dir.x > 0)
        {
            scale.x = 1;
        }
        else if (dir.x < 0)
        {
            scale.x = -1;
        }

		model.transform.localScale= scale;
	}

    private void HuntChild()
    {
		rigidbody.velocity = speed * (target.transform.position - transform.position).normalized;
		HandleFacing((target.transform.position - transform.position).normalized);
        if (Vector2.Distance(transform.position, target.transform.position) <= grabRange)
        {
            eatingChild = true;
            target.CapturedBy(this);
        }
    }

    public void Stagger()
    {
		Debug.Log(this  + " is staggered");
		eatingChild = false;
		target.Free();
		target = null;
		curStun = stunDuration;
    }

    Child FindClosetChild(float range){
		foreach( var child in mother.children){
			if (Vector2.Distance(child.transform.position, transform.position ) <= range && !child.isCaptured){
				return child;
			}
		}
		return null;
	}
	
}
