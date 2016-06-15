﻿using UnityEngine;
using System.Collections;


/// <summary>
/// 프로토 타입용 스크립트
/// 여기다 점프에 관련된 모든걸 박는다
/// </summary>
public class TestScript_1 : MonoBehaviour
{
    private Rigidbody2D playerRig = null;

    private bool isStartGame = false;

    private GameObject text = null;

    // Use this for initialization
    void Awake()
    {
        playerRig = GetComponent<Rigidbody2D>();
        text = GameObject.Find( "StartText" );
    }

    // Update is called once per frame
    void Update()
    {
        if ( Input.GetMouseButtonDown( 0 ) )
        {
            text.SetActive( false );
            if ( !isStartGame )
            {
                FirstMove();
            }
            isStartGame = true;
        }

        if ( isStartGame )
        {
            PlayerMove();
        }
    }

    private void FirstMove()
    {
        transform.Rotate( 0, 0, 90 );
        // ;sldkfjhg;lksdfgjklsdjglksdf'l;gdj';khlsdj;lgf
        // 나는 빡빡이다 나는 빡빡이다 나는 빡빡이다 나는 빡빡이다 나는 빡빡이다 나는 빡빡이다
    }

    private void PlayerMove()
    {

    }
}
