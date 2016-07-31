﻿using UnityEngine;
using System.Collections;

public class EventTest : MonoBehaviour
{
    void Awake()
    {
        //Util.Event.Litsen( "Start_Game", ( args ) => Debug.Log( "Event!!" ) );
        Util.Event.Litsen( "start_game", Test );
    }

    private void Test( params object[] lol )
    {
        foreach ( var t in lol )
        {
            Debug.Log( t );
        }
    }
}
