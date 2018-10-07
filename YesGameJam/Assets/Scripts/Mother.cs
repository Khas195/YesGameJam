using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mother : MonoBehaviour {
	public enum FacingDirection {
		Left,
		Right,
		Up,
		Down
	}
	public float speed;
	public float rangeOfMotherYelling;
	public float attackDamage;
	public float maxDistanceToDisconnectFromMother;

	public List<Child> children = new List<Child>();
	// Use this for initialization
	void Start () {
	}
	public void OnDrawGizmos()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(this.transform.position, rangeOfMotherYelling);
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(this.transform.position, maxDistanceToDisconnectFromMother);
	}
	
	// Update is called once per frame
	void Update () {
		var horizontal = Input.GetAxis("Horizontal");
		var vertical = Input.GetAxis("Vertical");
		var pos = this.transform.position;
		pos.x += speed * Time.deltaTime * horizontal;
		pos.y += speed * Time.deltaTime * vertical;
		this.transform.position = pos;
		if (Input.GetKey(KeyCode.Space)){
			foreach(var child in children){
				if (Vector3.Distance(this.transform.position, child.transform.position) <= rangeOfMotherYelling){
					child.ComeToMother();
				}
			}
		}
	}
	public void Attack(){
	}

}
