using UnityEngine;
using System.Collections;

/// <summary>
/// 스킬
/// - 장애물 파괴 (파괴 시 점수 증가)
/// </summary>

public enum DIRECTION
{
    LEFT = 0,
    RIGHT
}

public class Skill : MonoBehaviour
{
    public Vector2 m_vec2TargetPos_L;    // 왼쪽으로 스킬 사용 시 도착 지점
    public Vector2 m_vec2TargetPos_R;    // 오른쪽으로 스킬 사용 시 도착 지점
    public float m_fPlayer_Y;            // 스킬 쓰고 복귀할 위치의 y좌표
    public float m_fSkillLenth;          // 스킬 진행되는 시간
    public float m_fCoolTime;            // 스킬 쿨타임
    public float m_fDoubleClickTerm;     // 더블 클릭 체크 시간

    private DIRECTION m_eSkillDir;      // 스킬 사용 방향
    private DIRECTION m_ePlayerDir;     // 플레이어 오브젝트가 있는 위치
    private Vector2 m_vec2TargetPos;    // 스킬 사용시 도착 지점 저장 변수
    private Vector2 m_vec2StartPos;     // 스킬 시작 지점
    private Vector2 m_vec2ClickPnt;     // 화면 클릭 지점 저장 변수
    private TrailRenderer m_trSkillEff; // 스킬 효과 연출용 트레일 렌더러
    private float m_fMapSpeed;          // 맵 움직이는 속도
    private bool m_bUse;                // 스킬이 사용중인지 체크하는 변수
    private bool m_bCoolTime;           // 스킬이 쿨타임인지 체크하는 변수
    private bool m_bDoubleClick;        // 마우스 더블 클릭이 됐었는지 체크하는 변수
    private bool m_bClick;              // 마우스 클릭이 됐었는지 체크하는 변수

    public bool isClick
    {
        get
        {
            return m_bClick;
        }
    }

    // Use this for initialization
    void Awake ()
    {
        m_vec2TargetPos = new Vector2 ();
        m_vec2StartPos = new Vector2 ();
        m_vec2ClickPnt = new Vector2 ();
        m_trSkillEff = gameObject.GetComponent<TrailRenderer> ();
        m_bCoolTime = false;
        m_bUse = false;
        m_bClick = false;
        m_bDoubleClick = false;
    }

    void Start ()
    {
        m_trSkillEff.enabled = false;
    }

    // Update is called once per frame
    void Update ()
    {
        m_fMapSpeed = GameManager.instance.fGlobalSpeed;

        if ( gameObject.transform.localPosition.x > 0.0f )
        {
            m_ePlayerDir = DIRECTION.RIGHT;
        }
        else if ( gameObject.transform.localPosition.x < 0.0f )
        {
            m_ePlayerDir = DIRECTION.LEFT;
        }

        // 테스트용 입력
        if ( Input.GetKeyDown ( KeyCode.LeftArrow ) )
        {
            m_eSkillDir = DIRECTION.LEFT;
            UseSkill ();
        }
        else if ( Input.GetKeyDown ( KeyCode.RightArrow ) )
        {
            m_eSkillDir = DIRECTION.RIGHT;
            UseSkill ();
        }

        if ( Input.GetMouseButtonDown ( 0 ) )
        {
            m_bClick = true;
            if ( !m_bDoubleClick )
            {
                m_vec2ClickPnt = Camera.main.ScreenToWorldPoint ( new Vector3 ( Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane ) );
                StartCoroutine ( DClickTimer () );
            }
            else
            {
                if ( m_vec2ClickPnt.x > 0.0f &&
                   Camera.main.ScreenToWorldPoint ( new Vector3 ( Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane ) ).x > 0.0f )
                {
                    m_eSkillDir = DIRECTION.RIGHT;
                    UseSkill ();
                }
                else if ( m_vec2ClickPnt.x < 0.0f &&
                        Camera.main.ScreenToWorldPoint ( new Vector3 ( Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane ) ).x < 0.0f )
                {
                    m_eSkillDir = DIRECTION.LEFT;
                    UseSkill ();
                }
            }
        }
    }

    public void UseSkill ( DIRECTION direction )
    {
        if ( m_bUse || m_bCoolTime )
            return;

        StartCoroutine ( SkillAnimation ( direction ) );
    }

    void UseSkill ()
    {
        if ( m_bUse || m_bCoolTime )
            return;

        StartCoroutine ( SkillAnimation ( m_eSkillDir ) );
    }

    // 스킬 사용시 이동하는 연출
    IEnumerator SkillAnimation ( DIRECTION direction )
    {
        // 현재 위치의 x좌표와 목표 위치의 x좌표와 다를 경우만 함수 기능 실행
        if ( m_ePlayerDir != direction )
        {
            Debug.Log ( "Call Function : Skill.SkillAnimation(" + direction.ToString () + ")" );
            m_bUse = true;

            if ( direction == DIRECTION.LEFT )
                m_vec2TargetPos = m_vec2TargetPos_L;
            else if ( direction == DIRECTION.RIGHT )
                m_vec2TargetPos = m_vec2TargetPos_R;

            m_trSkillEff.enabled = true;
            // 위로 이동
            float fElapseTime = 0.0f;
            Vector2 vec2TempPos = new Vector2 ();
            m_vec2StartPos = gameObject.transform.localPosition;

            while ( fElapseTime < m_fSkillLenth )
            {
                fElapseTime += Time.deltaTime;
                vec2TempPos = m_vec2StartPos + ( m_vec2TargetPos - m_vec2StartPos ) * ( fElapseTime / m_fSkillLenth );
                gameObject.transform.localPosition = vec2TempPos;

                yield return new WaitForEndOfFrame ();
            }
            gameObject.transform.localPosition = m_vec2TargetPos;
            m_trSkillEff.enabled = false;

            StartCoroutine ( CoolTimeTimer () );

            // y좌표 0.0f 될때까지 내려감
            while ( gameObject.transform.localPosition.y > m_fPlayer_Y )
            {
                //gameObject.transform.Translate(Vector2.down * Time.deltaTime * m_fMapSpeed, Space.World);
                gameObject.transform.localPosition -= new Vector3 ( 0.0f, gameObject.transform.localPosition.y * m_fMapSpeed * Time.deltaTime );
                yield return new WaitForEndOfFrame ();
            }
            gameObject.transform.localPosition.Set ( gameObject.transform.localPosition.x, 0.0f, gameObject.transform.localPosition.z );
        }

        m_bUse = false;
    }

    // 스킬 쿨타임을 재는 기능
    IEnumerator CoolTimeTimer ()
    {
        m_bCoolTime = true;

        Debug.Log ( "CoolTime Start" );
        yield return new WaitForSeconds ( m_fCoolTime );

        Debug.Log ( "CoolTime Over" );
        m_bCoolTime = false;
    }

    // 더블 클릭 타임을 재는 기능
    IEnumerator DClickTimer ()
    {
        m_bDoubleClick = true;
        Debug.Log ( "ClickTimer Start" );
        yield return new WaitForSeconds ( m_fDoubleClickTerm );

        Debug.Log ( "ClickTimer Over" );
        m_bDoubleClick = false;
        m_bClick = false;
    }
}