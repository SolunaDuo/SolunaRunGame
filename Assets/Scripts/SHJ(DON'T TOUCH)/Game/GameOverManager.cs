using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour {
    public static GameOverManager instance = null;
    public Cards[] Cards;

    private Text tScroe;

    [SerializeField]
    private Text    tReTry;
    [SerializeField]
    private Image   iReTryBtn;
    [SerializeField]
    private Text    tMainMenu;
    [SerializeField]
    private Image   iMainMenuBtn;

    void Awake()
    {
        instance = this;
    }
	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetReulstText(int idx, string szmsg, int restulnum)
    {
        if (idx > Cards.Length)
        {
            Debug.Log("Cards idx OverFlow!");
            return;
        }
        Cards[idx].tResult.text = szmsg + restulnum;
    }

    public void SetScore(int Score)
    {
        if(tScroe == null)
        {
            Debug.Log("tScroe is Null!");
            return;
        }
        tScroe.text = "Score : " + Score;
    }

    public void SetReTryText(string szmsg)
    {
        if (tReTry == null)
        {
            Debug.Log("tReTry is Null!");
            return;
        }
        tReTry.text = szmsg;
    }

    public void SetMainMenuText(string szmsg)
    {
        if (tMainMenu == null)
        {
            Debug.Log("tMainMenu is Null!");
            return;
        }
        tMainMenu.text = szmsg;
    }

    public void MainBtnClick()
    {

    }

    public void ReTryBtnClick()
    {

    }



}
