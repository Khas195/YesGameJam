using System;
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

    public void DoneHitting()
    {
		canMove = true;
    }

    public KeyCode attackButton;
	public List<Child> children = new List<Child>();
	public GameObject model = null;
	public Animator anim = null;
	Rigidbody2D rigidbody;
	public GameObject hitBoxHorizontal;
	public GameObject hitBoxUp;
	public GameObject hitBoxDown;
    private bool attackOrder = false;
	bool canMove = true;
	Collider2D mCollider;

    // Use this for initialization
    void Start () {
		rigidbody = transform.GetComponent<Rigidbody2D>();
		mCollider = transform.GetComponent<Collider2D>();
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
	}
	// Update is called once per frame
	int beckon;
	void Update ()
    {
        if (Input.GetKey(attackButton))
        {
			if (facing.x != 0)
			{
				hitBoxHorizontal.SetActive(true);
			}
			else
			{
				if (facing.y > 0)
				{
					hitBoxUp.SetActive(true);
				}
				else
				{
					hitBoxDown.SetActive(true);
				}
			}
            //canMove = false;
        }
        if (canMove)
        {
            HandleMovement();
            HandleMovementAnimation();
        }
        if (Input.GetKey(KeyCode.Space))
        {
            CallAllChildToMother();
            PlayBeckonAnimation();
            canMove = false;
            rigidbody.velocity = Vector2.zero;
        }
        ReleaseCharacterAfterBeckon();

    }

    private void PlayBeckonAnimation()
    {
        if (facing.x != 0)
        {
            anim.Play("Beckon_H");
            beckon = 1;
        }
        else
        {
            if (facing.y > 0)
            {
                anim.Play("Beckon_Up");
                beckon = 2;
            }
            else
            {
                anim.Play("Beckon_Down");
                beckon = 3;
            }
        }
    }

    private void ReleaseCharacterAfterBeckon()
    {
        if (beckon != 0)
        {
            if (beckon == 1)
            {
                if (anim.GetCurrentAnimatorStateInfo(0).IsName("Beckon_H") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
                {
                    canMove = true;
                    beckon = 0;
                }
            }
            else if (beckon == 2)
            {
                if (anim.GetCurrentAnimatorStateInfo(0).IsName("Beckon_Up") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
                {
                    canMove = true;
                    beckon = 0;
                }
            }
            else if (beckon == 3)
            {
                if (anim.GetCurrentAnimatorStateInfo(0).IsName("Beckon_Down") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
                {
                    canMove = true;
                    beckon = 0;
                }
            }
        }
    }

    private void HandleMovementAnimation()
    {
        if (rigidbody.velocity.x != 0)
        {
            anim.Play("Running_H");
        }
        else if (rigidbody.velocity.y > 0)
        {
            anim.Play("Running_Up");
        }
        else if (rigidbody.velocity.y < 0)
        {
            anim.Play("Running_Down");
        }
        else if (rigidbody.velocity.y == 0 && rigidbody.velocity.x == 0)
        {
            if (facing.x != 0)
            {
                anim.Play("Idle_H");
            }
            else
            {
                if (facing.y > 0)
                {
                    anim.Play("Idle_Up");
                }
                else
                {
                    anim.Play("Idle_Down");
                }
            }
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
			scale.x = 1;
		} else if (horizontal < 0){
			facing.x = -1;
			scale.x = -1;
		} 
		if (vertical > 0) {
			facing.y = 1;
			facing.x = 0;
		} else if (vertical < 0){
			facing.y = -1;
			facing.x = 0;
		} 
		model.transform.localScale = scale; 
		rigidbody.velocity = speed * new Vector3(horizontal, vertical, 1) ;
    }

    public void Attack(){
	}

}
