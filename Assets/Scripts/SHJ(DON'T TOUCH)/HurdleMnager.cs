using UnityEngine;
using System.Collections;

public class HurdleMnager : MonoBehaviour {
    public GameObject[] obj_Hurdles;
    public float fSpeed;
    public float Range;

    int nCurrent = 0; // 현재 장애물
	// Use this for initialization
	void Start () {
        for(int i=0; i < obj_Hurdles.Length;++i)
        {
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

        while (true) // 게임 시작하면 움직이게 변경
        {
            yield return new WaitForEndOfFrame();
            obj_Hurdles[nCurrent].transform.localPosition -= new Vector3(0.0f, fSpeed * Time.deltaTime);

            if(obj_Hurdles[nCurrent].transform.localPosition.y <= -(fRange * 2f)) // 장애물 길이 개선 필요
            {
                obj_Hurdles[nCurrent].transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
                obj_Hurdles[nCurrent].SetActive(false);
                nCurrent = Random.Range(0, obj_Hurdles.Length);
                obj_Hurdles[nCurrent].SetActive(true);
            }

        }
    }

    // 장애물과 충동했을 경우
    void OnTriggerEnter2D(Collider2D col)
    {
        
    }

 }
