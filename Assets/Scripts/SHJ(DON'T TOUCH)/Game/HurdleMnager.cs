using UnityEngine;
using System.Collections;

public class HurdleMnager : MonoBehaviour {
    public static HurdleMnager instance = null;

    public GameObject[] obj_Hurdles;
    private Hundle[] Hurdles_infos;

    public GameObject obj_Drop;         // 떨어지는 장애물
    public GameObject obj_Danger;       // 위험 표시

    public float fDangerTime;
    public float Range;

    int nCurrent = 0; // 현재 장애물
    
    void Awake()
    {
        instance = this;
    }

	// Use this for initialization
	void Start () {
        Hurdles_infos = new Hundle[obj_Hurdles.Length];
        for (int i=0; i < obj_Hurdles.Length;++i)
        {
            Hurdles_infos[i] = obj_Hurdles[i].GetComponent<Hundle>();
            obj_Hurdles[i].SetActive(false);
        }

        obj_Hurdles[nCurrent].SetActive(true);
        StartCoroutine(MapMove(Range));
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator MapMove(float fRange)
    {
        yield return new WaitForEndOfFrame();

        while (true)
        {
            while (GameManager.instance.bPlayMode) // 게임 시작하면 움직이게 변경
            {
                yield return new WaitForEndOfFrame();
                obj_Hurdles[nCurrent].transform.localPosition -= new Vector3(0.0f, GameManager.instance.fGlobalSpeed * Time.deltaTime);

                if (isHundleCheck(Hurdles_infos[nCurrent].GetTopTran(), obj_Hurdles[nCurrent].transform) ) // 장애물 길이 개선 필요
                {
                    obj_Hurdles[nCurrent].transform.localPosition = new Vector3(0.0f, 1.7f, 0.0f);
                    obj_Hurdles[nCurrent].SetActive(false);
                    nCurrent = Random.Range(0, obj_Hurdles.Length);
                    // 장애물 다시 설정
                    Hurdles_infos[nCurrent].Init();
                }
            }
            yield return new WaitForEndOfFrame();
        }
    }

    bool isHundleCheck(Transform topobj, Transform curobj)
    {
        // 가장 높은 오브젝트가 화면에서 사라졌을 경우 true
        if (topobj.localPosition.y + curobj.localPosition.y >= -6.7f)
            return false;
        else
            return true;
    }
    
    // 장애물 삭제
    public void DeleteHurdle(GameObject DeleteObj)
    {
        // 애니메이션 나올시 추가
        DeleteObj.gameObject.SetActive(false);
    }

    // 장애물과 충동했을 경우
    void OnTriggerEnter2D(Collider2D col)
    {
        
    }

 }
