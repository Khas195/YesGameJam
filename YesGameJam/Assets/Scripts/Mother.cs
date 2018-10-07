using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mother : MonoBehaviour {

	Vector2 facing;
	public float speed;
	public float rangeOfMotherYelling;
	public float attackDamage;
	public float maxDistanceToDisconnectFromMother;
	public float attackRange;
	public KeyCode attackButton;
	public List<Child> children = new List<Child>();
	public GameObject model = null;
	public Animator anim = null;
    private bool attackOrder = false;
	

    // Use this for initialization
    void Start () {
	}
	public void OnDrawGizmos()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(this.transform.position, rangeOfMotherYelling);
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(this.transform.position, maxDistanceToDisconnectFromMother);
		Gizmos.color = Color.blue;
		Gizmos.DrawWireSphere(this.transform.position, attackRange);
	}
	
	private void FixedUpdate() {
		if (attackOrder) {
			var hit = Physics2D.Raycast(this.transform.position, Vector2.right, attackRange);
			if (hit.collider != null && hit.collider.tag == "Wolf") {
			}
			attackOrder = false;
		}
	}
	// Update is called once per frame
	void Update ()
    {
		if (Input.GetKeyDown(attackButton)){
			attackOrder = true;
		}
        HandleMovement();
        if (Input.GetKey(KeyCode.Space))
        {
            CallAllChildToMother();
        }
    }

    private void CallAllChildToMother()
    {
        foreach (var child in children)
        {
            if (Vector3.Distance(this.transform.position, child.transform.position) <= rangeOfMotherYelling)
            {
                child.ComeToMother();
            }
        }
    }

    private void HandleMovement()
    {
        var horizontal = Input.GetAxisRaw("Horizontal");
        var vertical = Input.GetAxisRaw("Vertical");
		var scale = model.transform.localScale;
		
		if (horizontal > 0){
			facing.x = 1;
		} else if (horizontal < 0){
			facing.x = -1;
		}
		anim.SetBool("IsWalking", horizontal != 0 ? true : false);
		if (vertical > 0) {
			facing.y = 1;
		} else {
			facing.y = -1;
		}
		scale.x = facing.x;
		model.transform.localScale = scale; 
        var pos = this.transform.position;
        pos.x += speed * Time.deltaTime * horizontal;
        pos.y += speed * Time.deltaTime * vertical;
        this.transform.position = pos;
    }

    public void Attack(){
	}

}
