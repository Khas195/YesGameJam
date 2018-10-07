using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Child : MonoBehaviour {
	public float speed;
	public Mother mother;
	public float maxDistanceFromMother;
	bool moving = false;
	bool disconnect = false;
	bool runToMother = false;
	Vector3 targetPos;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
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
		var offset = maxDistanceFromMother * Random.insideUnitSphere;
		while (IsTooCloseToMother(offset))
		{
			offset = maxDistanceFromMother * Random.insideUnitSphere;
		}
		targetPos = mother.transform.position + offset;
		targetPos.z = 1;
		moving = true;
	}
	public void GoToTarget(){
		var dir = targetPos - transform.position;
		dir = dir.normalized;
		transform.position += speed * dir * Time.deltaTime;
		if (Vector2.Distance(transform.position, targetPos) <= 1){
			moving = false;
			runToMother = false;
		}
		if (Vector3.Distance(mother.transform.position, transform.position) < mother.maxDistanceToDisconnectFromMother){
			disconnect = false;
		}
	}
	bool IsTooCloseToMother(Vector2 offset) {
		return offset.x > -1 && offset.x < 1 || offset.y > -1 && offset.y < 1;
	}
	public void ComeToMother(){
		runToMother = true;
	}
}
