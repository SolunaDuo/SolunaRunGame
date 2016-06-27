using UnityEngine;
using System.Collections;


/// <summary>
/// 프로토 타입용 스크립트
/// 여기다 점프에 관련된 모든걸 박는다
/// </summary>
public class TestScript_1 : MonoBehaviour
{
    private Rigidbody2D playerRig = null;

    private bool isStartGame = false;

    private GameObject text = null;

    public float power;

    private Vector2 left;
    private Vector2 right;
    private Vector2 target;
    private Vector2 start;

    private float xSp = 0.0f;
    private float ySp = 0.0f;

    private float timer = 0.0f;

    private bool startJump = false;

    // Use this for initialization
    void Awake()
    {
        playerRig = GetComponent<Rigidbody2D>();
        text = GameObject.Find( "StartText" );

        left = GameObject.Find( "LeftWall" ).transform.position;
        right = GameObject.Find( "RightWall" ).transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if ( Input.GetMouseButtonDown( 0 ) )
        {
            target = left;
            JumpInfoInit();
        }
        else if ( Input.GetMouseButtonDown( 1 ) )
        {
            target = right;
            JumpInfoInit();
        }

        if ( startJump )
        {
            Jump();
        }
    }

    private void JumpInfoInit()
    {
        if(!isStartGame)
        {
            Utils.Event.Send( "start_game" );
            isStartGame = true;
        }
        playerRig.isKinematic = false;
        start = transform.position;
        xSp = ( target.x - start.x ) / 2f;
        ySp = ( target.y - start.y + 2 * 9.8f ) / 2f;
        startJump = true;
    }

    private void Jump()
    {
        timer += Time.deltaTime;
        float x = start.x + xSp * timer;
        float y = start.y + ySp * timer - 0.5f * 9.8f * timer * timer;
        transform.position = new Vector2( x, y );
    }

    void OnCollisionEnter2D( Collision2D pCollision )
    {
        if ( pCollision.gameObject.name.Contains( "Wall" ) )
        {
            startJump = false;
            playerRig.isKinematic = true;
        }
    }

}
