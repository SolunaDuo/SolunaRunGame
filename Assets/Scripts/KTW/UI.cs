using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class UI : MonoBehaviour
{
    private GameObject startText = null;

    void Awake()
    {
        startText = GameObject.Find( "StartText" );
    }

    void Update()
    {
        Assets.Utils.Event.Litsen( "start_game", StartGame);
    }

    private void StartGame( object[] args )
    {
        startText.SetActive( false );
        Assets.Utils.Event.Remove( "start_game", StartGame );
    }
}
