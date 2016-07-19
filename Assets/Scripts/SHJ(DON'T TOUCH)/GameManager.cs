using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
public class GameManager : MonoBehaviour {
    public static GameManager instance = null;

    public Text tScroe;
    public Image iFeverGage;

    
    // 코멘트만 남겨둠. 이거 GameState 만들어 둔거 있는데 나중에 그거 써도 될듯
    public bool bPlayMode = true; // 플레이 중인지 확인
    public float fGlobalSpeed = 10f; // 맵 스피드

    private float fFeverGage = 0;
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

        if (fFeverDelayTime >= 0.1f)
        {
            fFeverDelayTime = 0.0f;
            PlusFeverGage(0.1f);
        }

        if(fScoreDelayTime >= 0.2f)
        {
            fScoreDelayTime = 0.0f;
            PlusScore(1);
        }

    } 

    void PlusFeverGage(float nPlus)
    {
        if (fFeverGage > 100f)
        {
            fFeverGage = 100f;
            return;
        }
        fFeverGage += nPlus;
        StartCoroutine(CoroutineManager.instance.fillAmountAni(iFeverGage,fFeverGage / 100f, 0.3f));
    }

    void PlusScore(int nPlus)
    {
        nScore += nPlus;
        tScroe.text = "Score : " + nScore;
    }

    float GetFeverGage()
    {
        return fFeverGage;
    }

    int GetScore()
    {
        return nScore;
    }
}
