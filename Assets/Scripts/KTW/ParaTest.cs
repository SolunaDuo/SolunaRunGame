using UnityEngine;
using System.Collections;

public class ParaTest : MonoBehaviour
{

    private Vector2 leftTargetPos;
    private Vector2 rightTargetPos;
    private Vector2 startPos; // 시작
    private Vector2 targetPos; // 도착

    private float xSpeed = 0f; // x축 방향 속도
    private float ySpeed = 0f; // y축 방향 속도

    private float timer;

    private bool startJump = false;

    // Use this for initialization
    void Awake()
    {
        leftTargetPos = GameObject.Find( "LeftWall" ).transform.position;
        rightTargetPos = GameObject.Find( "RightWall" ).transform.position;
        startPos = transform.position;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ( Input.GetMouseButtonDown( 0 ) )
        {
            targetPos = leftTargetPos;
            JumpInfoInit();
        }
        else if ( Input.GetMouseButtonDown( 1 ) )
        {
            targetPos = rightTargetPos;
            JumpInfoInit();
        }

        if ( startJump )
        {
            Jump();
        }
    }

    private void JumpInfoInit()
    {
        startPos = transform.position;
        xSpeed = ( targetPos.x - startPos.x ) / 2f;
        ySpeed = ( targetPos.y - startPos.y + 2 * 9.8f ) / 2f;
        startJump = true;
    }

    private void Jump()
    {
        timer += Time.deltaTime;
        float x = startPos.x + xSpeed * timer;
        float y = startPos.y + ySpeed * timer - 0.5f * 9.8f * timer * timer;
        transform.position = new Vector2( x, y );
    }

    void OnCollisionEnter2D( Collision2D pCollision )
    {
        Debug.Log( "!" );
        if ( pCollision.gameObject.name.Contains( "Wall" ) )
        {
            startJump = false;
        }
    }
}
