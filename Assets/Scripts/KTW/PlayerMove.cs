using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour {

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
    private float speed = 3.0f;

    [SerializeField]
    private float targetMinus = 0.0f;

    private Rigidbody2D rig;

    // Use this for initialization
    void Awake() {
        startPos = transform.position;

        targetMinus = transform.localScale.x + 0.5f;

        leftEndPos = GameObject.Find( "Map_02" ).transform.Find( "LeftWall" ).position;
        rightEndPos = GameObject.Find( "Map_02" ).transform.Find( "RightWall" ).position;

        rig = GetComponent<Rigidbody2D>();

        leftEndPos.x += targetMinus;
        rightEndPos.x -= targetMinus;
    }

    // Update is called once per frame
    void Update() {
        if( Input.GetKeyDown( KeyCode.A ) ) {
            isJumping = true;
            endPos = leftEndPos;
        }
        else if( Input.GetKeyDown( KeyCode.D ) ) {
            isJumping = true;
            endPos = rightEndPos;
        }

        if( isJumping ) {
            Jumping();
        }
    }

    public void Jumping() {
        t += Time.deltaTime * speed;
        if( startCurve ) {
            Vector3 center = ( startPos + endPos ) * 0.5f;
            center.y += jumpHeight;
            transform.position = Util.Math.GetBezierCurve( startPos, center, endPos, t );
        }
        else {
            var p = Util.Math.GetLinearCurve( startPos, endPos, t );
            transform.position = p;
        }
        if( t > 1.0f ) {
            t = 0.0f;
            if( startCurve == false ) {
                startCurve = true;
            }
            isJumping = false;
            startPos = transform.position;
        }
    }
}
