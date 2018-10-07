using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf : MonoBehaviour {
	public float speed;
	public float aggroRange;
	Child target = null;
	public CircleCollider2D aggroCollider;
	// Use this for initialization
	void Start () {
		aggroCollider.radius = aggroRange;
	}
	
	// Update is called once per frame
	void Update () {
		if (target != null){
			var pos = transform.position;
			//pos = Vector2.MoveTowards(pos, target.transform.position, speed * Time.deltaTime);
			transform.position = pos;
			if (Vector2.Distance(pos, target.transform.position) < 1){
				target = null;
			}
		}
	}
	void OnTriggerEnter2D(Collider2D other)
	{
		if (target == null && other.gameObject.GetComponent<Child>() != null){
			target = other.gameObject.GetComponent<Child>();
		}
	}
}
