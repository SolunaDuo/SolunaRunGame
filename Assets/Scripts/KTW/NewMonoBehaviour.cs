using UnityEngine;
using System.Collections;

public class NewMonoBehaviour : MonoBehaviour
{

    GameObject player; Vector3 startPos; Vector3 destPos; float timer; float vx; float vy; float vz;  // Use this for initialization 
    void Start()
    {
        startPos = transform.position;
        destPos = player.transform.position;
        vx = ( destPos.x - startPos.x ) / 2f;
        vz = ( destPos.z - startPos.z ) / 2f;
        vy = ( destPos.y - startPos.y + 2 * 9.8f ) / 2f;
    }// Update is called once per frame 
    void Update()
    {
        timer += Time.deltaTime;
        float sx = startPos.x + vx * timer;
        float sy = startPos.y + vy * timer - 0.5f * 9.8f * timer * timer;
        float sz = startPos.z + vz * timer;
        transform.position = new Vector3( sx, sy, sz );
        transform.Rotate( 1.2f, 0, 0 );
    }
}
