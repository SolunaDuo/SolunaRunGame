using UnityEngine;
using System.Collections;

public class HundleTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    // 장애물과 충동했을 경우
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag.Equals("Player"))
        {
            GameManager.instance.bPlayMode = false;
        }
    }
}
