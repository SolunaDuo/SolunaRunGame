using UnityEngine;
using System.Collections;

public static class GameState
{
    public enum StateAtt
    {
        READY,
        START,
        OVER,
    }

    private static StateAtt currentState;

    public static void SetState( StateAtt st )
    {
        currentState = st;
    }

    public static StateAtt GetState()
    {
        return currentState;
    }

    public static bool IsEqualState( StateAtt st )
    {
        return ( currentState == st );
    }
}
