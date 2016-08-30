using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour {

    public enum JumpDirection {
        NONE,
        LEFT,
        RIGHT,
    }

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

    private PlayerAnim animControl;

    private SpriteRenderer playerRenderer;

    private JumpDirection j_dir = JumpDirection.NONE;

    // Use this for initialization
    void Awake() {
        startPos = transform.position;

        targetMinus = transform.localScale.x / 3f + 0.5f;

        leftEndPos = GameObject.Find( "Map_02" ).transform.Find( "LeftWall" ).position;
        rightEndPos = GameObject.Find( "Map_02" ).transform.Find( "RightWall" ).position;

        leftEndPos.x += targetMinus;
        rightEndPos.x -= targetMinus;

        leftEndPos.y -= 2f;
        rightEndPos.y -= 2f;

        animControl = GetComponent<PlayerAnim>();
        playerRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update() {
        if( Input.GetKeyDown( KeyCode.A ) ) {
            j_dir = JumpDirection.LEFT;
            isJumping = true;
            endPos = leftEndPos;
        }
        else if( Input.GetKeyDown( KeyCode.D ) ) {
            j_dir = JumpDirection.RIGHT;
            isJumping = true;
            endPos = rightEndPos;
        }

        if( isJumping ) {
            JumpRotation();
            Jumping();
        }
    }

    private void Jumping() {
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
                playerRenderer.flipY = true;
                startCurve = true;
            }
            isJumping = false;
            animControl.Play( PlayerAnim.Animation.Run_2 );
            startPos = transform.position;
        }
    }

    private void DoubleJumping() {

    }

    private void JumpRotation() {
        animControl.Play( PlayerAnim.Animation.Jump );
        playerRenderer.flipX = ( j_dir != JumpDirection.RIGHT );

        int sign = ( j_dir == JumpDirection.LEFT ) ? 1 : -1;
        transform.rotation = Quaternion.Euler( 0, 0, ( 90f * sign ) * t * 5f );
    }
}
