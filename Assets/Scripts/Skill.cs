using UnityEngine;
using System.Collections;

/// <summary>
/// 스킬
/// - 움직임 및 연출
/// - 쿨타임
/// - 스킬 사용 가능 여부
/// - 장애물 파괴 (파괴 시 점수 증가)
/// - 스킬 사용 후 원위치
/// </summary>

public enum DIRECTION
{
    LEFT,
    RIGHT
}

public class Skill : MonoBehaviour
{

    public Vector2 m_vec2TargetPos_L;    // 왼쪽으로 스킬 사용 시 도착 지점
    public Vector2 m_vec2TargetPos_R;    // 오른쪽으로 스킬 사용 시 도착 지점
    public float m_fSkillLenth;          // 스킬 진행되는 시간
    public float m_fMapSpeed;            // 맵 움직이는 속도
    public float m_fCoolTime;            // 스킬 쿨타임

    private DIRECTION m_eSkillDir;      // 스킬 사용 방향
    private Vector2 m_vec2TargetPos;    // 스킬 사용시 도착 지점 저장 변수
    private Vector2 m_vec2StartPos;     // 스킬 시작 지점
    private TrailRenderer m_trSkillEff; // 스킬 효과 연출용 트레일 렌더러
    private bool m_bUse;                // 스킬이 사용중인지 체크하는 함수
    private bool m_bCoolTime;           // 스킬이 쿨타임인지 체크하는 함수

    // Use this for initialization
    void Awake()
    {
        m_vec2TargetPos = new Vector2();
        m_vec2StartPos = new Vector2();
        m_trSkillEff = gameObject.GetComponent<TrailRenderer>();
        m_bCoolTime = false;
        m_bUse = false;
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // 테스트용 입력
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            m_eSkillDir = DIRECTION.LEFT;
            UseSkill();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            m_eSkillDir = DIRECTION.RIGHT;
            UseSkill();
        }
    }

    public void UseSkill(DIRECTION direction)
    {
        if (m_bUse || m_bCoolTime)
            return;

        StartCoroutine(SkillAnimation(direction));
    }

    void UseSkill()
    {
        if (m_bUse || m_bCoolTime)
            return;

        StartCoroutine(SkillAnimation(m_eSkillDir));
    }

    // 스킬 사용시 이동하는 연출
    IEnumerator SkillAnimation(DIRECTION direction)
    {
        m_bUse = true;

        if (direction == DIRECTION.LEFT)
            m_vec2TargetPos = m_vec2TargetPos_L;
        else if (direction == DIRECTION.RIGHT)
            m_vec2TargetPos = m_vec2TargetPos_R;

        // 현재 위치의 x좌표와 목표 위치의 x좌표와 다를 경우만 이동
        if (gameObject.transform.position.x != m_vec2TargetPos.x)
        {
            m_trSkillEff.enabled = true;
            // 위로 이동
            float fElapseTime = 0.0f;
            Vector2 vec2TempPos = new Vector2();
            m_vec2StartPos = gameObject.transform.localPosition;

            while (fElapseTime < m_fSkillLenth)
            {
                fElapseTime += Time.deltaTime;
                vec2TempPos = m_vec2StartPos + (m_vec2TargetPos - m_vec2StartPos) * (fElapseTime / m_fSkillLenth);
                gameObject.transform.localPosition = vec2TempPos;

                yield return new WaitForEndOfFrame();
            }
            gameObject.transform.localPosition = m_vec2TargetPos;
            m_trSkillEff.enabled = false;

            StartCoroutine(CoolTimeChecker());

            // y좌표 0.0f 될때까지 내려감
            while (gameObject.transform.localPosition.y > 0.0f)
            {
                gameObject.transform.Translate(Vector2.down * Time.deltaTime * m_fMapSpeed, Space.World);

                yield return new WaitForEndOfFrame();
            }
            gameObject.transform.localPosition.Set(gameObject.transform.localPosition.x, 0.0f, gameObject.transform.localPosition.z);
        }

        m_bUse = false;        
    }

    // 스킬 쿨타임을 재는 기능
    IEnumerator CoolTimeChecker()
    {
        m_bCoolTime = true;

        Debug.Log("CoolTime Start");
        yield return new WaitForSeconds(m_fCoolTime);

        Debug.Log("CoolTime Over");
        m_bCoolTime = false;
    }
}
