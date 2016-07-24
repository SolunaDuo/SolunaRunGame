using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour {
    public static GameOverManager instance = null;
    public Cards[] Cards;

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
}
