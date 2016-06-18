using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class GameManager : MonoBehaviour {
    public static GameManager instance = null;

    public Text tScroe;

    public Image iFeverGage;

    public float fMapSpeed = 10f; // 맵 스피드

    private int nfeverGage = 0;
    private int nScore = 0;

    private float fFeverDelayTime = 0.0f;
    private float fScoreDelayTime = 0.0f;
    void Awake()
    {
        instance = this;
    }

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        fFeverDelayTime += Time.deltaTime;
        fScoreDelayTime += Time.deltaTime;

        if (fFeverDelayTime >= 1f)
        {
            fFeverDelayTime = 0.0f;
            PlusFeverGage(10);
        }

        if(fScoreDelayTime >= 0.2f)
        {
            fScoreDelayTime = 0.0f;
            PlusScore(1);
        }

    } 

    void PlusFeverGage(int nPlus)
    {
        if (nfeverGage > 100)
        {
            nfeverGage = 100;
            return;
        }
        nfeverGage += nPlus;
        StartCoroutine(CoroutineManager.instance.fillAmountAni(iFeverGage,(float)nfeverGage / 100, 0.3f));
    }

    void PlusScore(int nPlus)
    {
        nScore += nPlus;
        tScroe.text = "Score : " + nScore;
    }

    int GetFeverGage()
    {
        return nfeverGage;
    }

    int GetScore()
    {
        return nScore;
    }
}
