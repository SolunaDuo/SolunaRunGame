using UnityEngine;
using System.Collections;

public class ParaTest2 : MonoBehaviour
{


    private Transform start;
    private Transform target;
    private Vector3 targetPos;

    private Transform leftTarget;
    private Transform rightTarget;

    private Vector3 leftTargetPos;
    private Vector3 rightTargetPos;

    private float journeyTime = 1.0f;
    private float startTime = 0.0f;

    public bool isJumping = false;

    // Use this for initialization
    void Awake()
    {
        startTime = Time.time;
        start = transform;
        //leftTarget = GameObject.Find( "LeftWall" ).transform;
        //rightTarget = GameObject.Find( "RightWall" ).transform;

        var middleMap = GameObject.Find( "Map_02" ).transform;


        leftTargetPos = middleMap.Find( "LeftWall" ).position;
        rightTargetPos = middleMap.Find( "RightWall" ).position;

        leftTargetPos = new Vector3( leftTargetPos.x + 0.5f, leftTargetPos.y, leftTargetPos.z );
        rightTargetPos = new Vector3( rightTargetPos.x - 0.5f, rightTargetPos.y, rightTargetPos.z );

        Debug.Log( leftTargetPos );
        Debug.Log( rightTargetPos );
    }

    // Update is called once per frame
    void Update()
    {
        if ( Input.GetMouseButtonDown( 0 ) )
        {
            //target = leftTarget;
            targetPos = leftTargetPos;
            isJumping = true;
        }
        else if ( Input.GetMouseButtonDown( 1 ) )
        {
            //target = rightTarget;
            targetPos = rightTargetPos;
            isJumping = true;
        }

        if ( isJumping )
        {
            Jump();
        }
    }

    private void Jump()
    {
        Vector3 center = ( start.position + targetPos ) * 0.5f;
        center -= Vector3.up;
        Vector3 startRelCenter = start.position - center;
        Vector3 targetRelCenter = targetPos - center;
        //float frac = ( Time.time - startTime ) / journeyTime;
        float frac = journeyTime * Time.deltaTime;
        transform.position = Vector3.Slerp( startRelCenter, targetRelCenter, frac );
        transform.position += center;
    }

    void OnTriggerEnter2D(Collider2D pCollider) {
        if( pCollider.gameObject.name.Contains( "Wall" ) ) {
            isJumping = false;
        }
    }
}
