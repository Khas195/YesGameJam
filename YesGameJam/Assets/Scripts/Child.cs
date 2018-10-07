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
	Rigidbody2D rigidbody;
	Vector3 targetPos;
    private Wolf capturer;
     public bool isCaptured;

    // Use this for initialization
    void Start () {
		this.rigidbody = this.GetComponent<Rigidbody2D>();
	}

	
	// Update is called once per frame
	void Update () {
		if (isCaptured || !moving) {
			rigidbody.velocity = Vector2.zero;
		}
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
		rigidbody.velocity = speed * dir;
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

    public void Free()
    {
		this.capturer = null;
		isCaptured = false;
    }

    void OnCollisionEnter2D(Collision2D collisionInfo)
	{
		if (collisionInfo.gameObject.tag == "Child"){
			var dir = (transform.position - collisionInfo.gameObject.transform.position).normalized;
			rigidbody.AddForce(dir * 20.0f);
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
