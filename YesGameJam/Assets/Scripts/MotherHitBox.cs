using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotherHitBox : MonoBehaviour {

	public Mother mother;
	// Use this for initialization
	void Start () {
		
	}
	void OnCollisionEnter2D(Collision2D collisionInfo)
	{
		Debug.Log("Hit");
		if (collisionInfo.gameObject.tag == "Wolf") {
			Debug.Log("Hit WOlf");
		}
		mother.DoneHitting();
		this.gameObject.SetActive(false);
	}
	// Update is called once per frame
	void Update () {
		
	}
}
