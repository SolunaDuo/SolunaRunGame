using UnityEngine;
using System.Collections;

public class HundleTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    // 장애물과 충동했을 경우 나중에 플레이어에서 따로 처리해야할듯
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag.Equals("Player"))
        {
            //GameManager.instance.bPlayMode = false;
        }
    }
}
