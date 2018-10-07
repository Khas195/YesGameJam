using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMover : MonoBehaviour {

    public enum DIRECTION
    {
        NORTH,
        NORTHEAST,
        EAST,
        SOUTHEAST,
        SOUTH,
        SOUTHWEST,
        WEST,
        NORTHWEST
    }

    private Dictionary<DIRECTION, Vector2> DIRECTION_VECTOR = new Dictionary<DIRECTION, Vector2>()
    {
        { DIRECTION.NORTH, new Vector2(0f, 1f) },
        { DIRECTION.NORTHEAST, new Vector2(1f, 1f).normalized },
        { DIRECTION.EAST, new Vector2(1f, 0f) },
        { DIRECTION.SOUTHEAST, new Vector2(1f, -1f).normalized },
        { DIRECTION.SOUTH, new Vector2(0f, -1f) },
        { DIRECTION.SOUTHWEST, new Vector2(-1f, -1f).normalized },
        { DIRECTION.WEST, new Vector2(-1f, 0f) },
        { DIRECTION.NORTHWEST, new Vector2(-1f, 1f).normalized }
    };

    private float myAcceleration = 0f;
    private float myDrag = 0f;

    private float myVelocityX = 0f;
    private float myVelocityY = 0f;

    private Rigidbody2D myRigidbody = null;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
    {
        
    }

    public void SetRigidBody(Rigidbody2D R)
    {
        this.myRigidbody = R;
    }

    public void SetAcceleration(float a, bool bHalf = true)
    {
        this.myAcceleration = a;

        if (bHalf)
        {
            this.myDrag = this.myAcceleration / 2f;
        }
    }

    public void SetDrag(float d)
    {
        this.myDrag = d;
    }
}
