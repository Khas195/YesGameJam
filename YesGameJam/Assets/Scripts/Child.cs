using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Child : MonoBehaviour {
	public float speed;
	public Mother mother;
	public float maxDistanceFromMother;
	public GameObject model;
	bool moving = false;
	bool disconnect = false;
	bool runToMother = false;
	Vector3 targetPos;
    private Wolf capturer;
    private bool isCaptured;

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (isCaptured) return;
		var distanceFromMothertoChild = Vector3.Distance(mother.transform.position, transform.position ); 
		if (!disconnect || runToMother) {
			if (  distanceFromMothertoChild > maxDistanceFromMother){
				if (!moving){
					FindMother();
				}
			}
			if (moving){
				GoToTarget();
			}
			if ( distanceFromMothertoChild >= mother.maxDistanceToDisconnectFromMother) {

				moving = false;
				disconnect = true;
			}
		} 		
	}
	public void FindMother(){
		var offset = maxDistanceFromMother * UnityEngine.Random.insideUnitSphere;
		while (IsTooCloseToMother(offset))
		{
			offset = maxDistanceFromMother * UnityEngine.Random.insideUnitSphere;
		}
		targetPos = mother.transform.position + offset;
		targetPos.z = 1;
		moving = true;
	}

    public void CapturedBy(Wolf wolf)
    {
		this.capturer = wolf;
		isCaptured = true;
    }

    public void GoToTarget()
    {
        var dir = targetPos - transform.position;
        dir = dir.normalized;
        HandleFacing(dir);
        transform.position += speed * dir * Time.deltaTime;
        if (Vector2.Distance(transform.position, targetPos) <= 1)
        {
            moving = false;
            runToMother = false;
        }
        if (Vector3.Distance(mother.transform.position, transform.position) < mother.maxDistanceToDisconnectFromMother)
        {
            disconnect = false;
        }
    }

    private void HandleFacing(Vector3 dir)
    {
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

    bool IsTooCloseToMother(Vector2 offset) {
		return offset.x > -1 && offset.x < 1 || offset.y > -1 && offset.y < 1;
	}
	public void ComeToMother(){
		runToMother = true;
	}
}
