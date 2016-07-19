using UnityEngine;
using System.Collections;

public class ParaTest2 : MonoBehaviour
{


    private Transform start;
    private Transform target;

    private Transform leftTarget;
    private Transform rightTarget;

    private float journeyTime = 1.0f;
    private float startTime = 0.0f;

    public bool isJumping = false;

    // Use this for initialization
    void Awake()
    {
        startTime = Time.time;
        start = transform;
        leftTarget = GameObject.Find( "LeftWall" ).transform;
        rightTarget = GameObject.Find( "RightWall" ).transform;
    }

    // Update is called once per frame
    void Update()
    {
        if ( Input.GetMouseButtonDown( 0 ) )
        {
            target = leftTarget;
            isJumping = true;
        }
        else if ( Input.GetMouseButtonDown( 1 ) )
        {
            target = rightTarget;
            isJumping = true;
        }

        if ( isJumping )
        {
            Jump();
        }
    }

    private void Jump()
    {
        Vector3 center = ( start.position + target.position ) * 0.5f;
        center -= Vector3.up;
        Vector3 startRelCenter = start.position - center;
        Vector3 targetRelCenter = target.position - center;
        //float frac = ( Time.time - startTime ) / journeyTime;
        float frac = journeyTime * Time.deltaTime;
        transform.position = Vector3.Slerp( startRelCenter, targetRelCenter, frac );
        transform.position += center;
    }

    void OnCollisionEnter2D( Collision2D pCollision )
    {
        Debug.Log( "!" );
        if ( pCollision.gameObject.name.Contains( "Wall" ) )
        {
            isJumping = false;
        }
    }
}
