using UnityEngine;
using System.Collections;

public static class Utils
{
    /// <summary>
    /// Listen을 하고 있다가 Send를 하면 받는다
    /// Send 후 Listen 하면 Litsen 동작안함 ( 한번만 돌릴 경우 )
    /// </summary>
    public class Event
    {
        public delegate void EventHandler( params object[] args );
        private static Hashtable listeners = new Hashtable();

        /// <summary>
        /// 받는다. 나는. 이벤트.
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="action"></param>
        public static void Litsen( string msg, EventHandler action )
        {
            var actions = listeners[ msg ] as EventHandler;
            if ( actions != null )
            {
                listeners[ msg ] = actions + action;
            }
            else
            {
                listeners[ msg ] = action;
            }
        }

        /// <summary>
        /// 지운다. 나는. 이벤트
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="action"></param>
        public static void Remove( string msg, EventHandler action )
        {
            var actions = listeners[ msg ] as EventHandler;
            if ( actions != null )
            {
                listeners[ msg ] = actions - action;
            }
        }

        /// <summary>
        /// 보낸다. 이벤트. 나는
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="args"></param>
        public static void Send( string msg, params object[] args )
        {
            // 간단하게 만들수 있다며 Invoke 추천하는데 바꾸지 말것
            // : 유니티가 모노 2.0 아재 버전 써가지고 지원안함
            var actions = listeners[ msg ] as EventHandler;
            if ( actions != null )
            {
                actions( args );
            }
        }
    }
}
