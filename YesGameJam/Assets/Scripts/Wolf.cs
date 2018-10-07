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
	// Use this for initialization
	void Start () {
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
		if (!eatingChild){
			target = FindClosetChild(aggroRange);
			if (target != null)
            {
                HuntChild();
            }
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
        var pos = transform.position;
        pos = Vector2.MoveTowards(pos, target.transform.position, speed * Time.deltaTime);
		HandleFacing((target.transform.position - pos).normalized);
        transform.position = pos;
        if (Vector2.Distance(pos, target.transform.position) <= grabRange)
        {
            eatingChild = true;
            target.CapturedBy(this);
        }
    }

    Child FindClosetChild(float range){
		foreach( var child in mother.children){
			if (Vector2.Distance(child.transform.position, transform.position ) <= range){
				return child;
			}
		}
		return null;
	}
	
}
