using UnityEngine;
using System.Collections;

public class MapDown : MonoBehaviour {
    public GameObject[] obj_Maps;
    public float Range;

    
    private int nTop = 0; // 맨위에
    private int nBot = 2; // 가장 아래
	// Use this for initialization
	void Start () {
        StartCoroutine(MapMove(Range));
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator MapMove(float fRange)
    {
        yield return new WaitForEndOfFrame();

        while(true) // 게임 시작하면 움직이게 변경
        {
            yield return new WaitForEndOfFrame();

            for(int i=0; i< obj_Maps.Length;++i)
                obj_Maps[i].transform.localPosition -= new Vector3(0.0f, GameManager.instance.fMapSpeed * Time.deltaTime);

            if (obj_Maps[nTop].transform.localPosition.y <= 0.0f)    // 맨 위에가 중앙으로 왔을 경우
            {
                int PrveTop = nTop; 
     
                obj_Maps[nBot].transform.localPosition = new Vector3(obj_Maps[nBot].transform.localPosition.x, fRange - 0.5f, obj_Maps[nBot].transform.localPosition.z);
                // Bot을 Top로 옮겨준 다음 Bot은 전에 있던 Top 밑에 애로 바꿔줌.
                nTop = nBot;

                nBot = PrveTop + 1;

                if (nBot >= 3)
                    nBot = 0;
            }
        }


    }
}
