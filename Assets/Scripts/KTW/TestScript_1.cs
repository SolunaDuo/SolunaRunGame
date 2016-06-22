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

    public float power = 10f;

    private Vector2 fixPosition;

    public bool isFix;

    // Use this for initialization
    void Awake()
    {
        playerRig = GetComponent<Rigidbody2D>();
        text = GameObject.Find( "StartText" );
    }

    // Update is called once per frame
    void Update()
    {
        if ( !isStartGame )
        {
            FirstMove();
        }
        PlayerMove();
        if ( isFix )
        {
            transform.position = fixPosition;
        }
    }

    private void FirstMove()
    {
        if ( Input.GetMouseButtonDown( 0 ) || Input.GetMouseButtonDown( 1 ) )
        {
            Jump( ( Vector2.right + Vector2.up ).normalized );
            transform.Rotate( 0, 0, 90 );
            isStartGame = true;
            playerRig.gravityScale = 0f;
            Utils.Event.Send( "start_game" );
        }
    }

    private void PlayerMove()
    {
        if ( Input.GetMouseButtonDown( 0 ) )
        {
            Jump( Vector2.left);
        }
        else if ( Input.GetMouseButtonDown( 1 ) )
        {
            Jump( Vector2.right );
        }
    }

    private void Jump( Vector2 dir )
    {
        playerRig.AddForce( dir * power, ForceMode2D.Force );
        transform.localScale = new Vector3( transform.localScale.x, ( dir.x < 0 ) ? -1 : 1, transform.localScale.z );
        UnFix();
    }

    void OnCollisionEnter2D( Collision2D pCollision )
    {
        if ( pCollision.gameObject.name.Contains( "Wall" ) )
        {
            Fix( transform.position );
        }
    }

    private void Fix( Vector2 pos )
    {
        isFix = true;
        fixPosition = pos;
    }

    private void UnFix()
    {
        isFix = false;
    }
}
