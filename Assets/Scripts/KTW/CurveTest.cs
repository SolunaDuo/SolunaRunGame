using UnityEngine;
using System.Collections;

public class CurveTest : MonoBehaviour {

    [SerializeField]
    private Vector3 startPos;
    private Vector3 endPos;

    [SerializeField]
    private Vector3 leftEndPos;
    [SerializeField]
    private Vector3 rightEndPos;

    [SerializeField]
    private bool isJumping = false;

    private bool startCurve = false;

    private float t = 0.0f;

    [SerializeField]
    private float jumpHeight = 2f;

    [SerializeField]
    private float speed = 0.0f;

    // Use this for initialization
    void Awake() {
        startPos = transform.position;
        leftEndPos.x += 0.5f;
        rightEndPos.x -= 0.5f;
        GameState.SetState( GameState.StateAtt.READY );
    }

    // Update is called once per frame
    void Update() {
        if( Input.GetMouseButtonDown( 0 ) ) {
            isJumping = true;
            endPos = leftEndPos;
        }
        else if( Input.GetMouseButtonDown( 1 ) ) {
            isJumping = true;
            endPos = rightEndPos;
        }

        if( isJumping ) {
            Jumping();
        }
    }

    public void Jumping() {
        if( startCurve ) {
            t += Time.deltaTime * speed;
            Vector3 center = ( startPos + endPos ) * 0.5f;
            center.y += jumpHeight;

            var e = GetLinearCurve( startPos, center, t );
            var f = GetLinearCurve( center, endPos, t );
            var p = GetLinearCurve( e, f, t );
            transform.position = p;
        }
        else {
            t += Time.deltaTime * speed;
            var p = GetLinearCurve( startPos, endPos, t );
            transform.position = p;
        }
    }

    private Vector3 GetLinearCurve(Vector3 p1, Vector3 p2, float t ) {
        var result = ( ( 1f - t ) * p1 ) + ( t * p2 );
        return result;
    }

    void OnTriggerEnter2D( Collider2D pCollider ) {
        if( pCollider.gameObject.name.Contains( "Wall" ) ) {
            isJumping = false;
            if( startCurve == false ) {
                startCurve = true;
            }
            startPos = transform.position;
            t = 0;
        }
    }
}
