using UnityEngine;
using System.Collections;

namespace Util {
    /// <summary>
    /// Listen을 하고 있다가 Send를 하면 받는다
    /// 사용방법은
    ///     1. Awake나 Start 타임에서 Listen등록
    ///     2. 다른곳에서 Send
    /// </summary>
    public class Event {
        public delegate void EventHandler( params object[] args );
        private static Hashtable listeners = new Hashtable();

        /// <summary>
        /// 받는다. 나는. 이벤트.
        /// </summary>
        /// <param name="msg"> 받을 메시지 </param>
        /// <param name="action"> 받았을때 Action </param>
        public static void Litsen( string msg, EventHandler action ) {
            var actions = listeners[ msg ] as EventHandler;
            if( actions != null ) {
                listeners[ msg ] = actions + action;
            }
            else {
                listeners[ msg ] = action;
            }
        }

        /// <summary>
        /// 지운다. 나는. 이벤트
        /// </summary>
        /// <param name="msg"> 지울 메시지 </param>
        /// <param name="action"> 지울 Action </param>
        public static void Remove( string msg, EventHandler action ) {
            var actions = listeners[ msg ] as EventHandler;
            if( actions != null ) {
                listeners[ msg ] = actions - action;
            }
        }

        /// <summary>
        /// 보낸다. 이벤트. 나는
        /// </summary>
        /// <param name="msg"> 보낼 메시지 </param>
        /// <param name="args"> 보낼 데이터 </param>
        public static void Send( string msg, params object[] args ) {
            // 간단하게 만들수 있다며 Invoke 추천하는데 바꾸지 말것
            // : 유니티가 모노 2.0 아재 버전 써가지고 지원안함
            var actions = listeners[ msg ] as EventHandler;
            if( actions != null ) {
                actions( args );
            }
        }
    }
    public static class Math {
        public static Vector3 GetLinearCurve( Vector3 p1, Vector3 p2, float t ) {
            var result = ( ( 1f - t ) * p1 ) + ( t * p2 );
            return result;
        }

        public static Vector3 GetBezierCurve( Vector3 p1, Vector3 p2, Vector3 p3, float t ) {
            Vector3 result = Vector3.zero;

            var l1 = GetLinearCurve( p1, p2, t );
            var l2 = GetLinearCurve( p2, p3, t );
            result = GetLinearCurve( l1, l2, t );

            return result;
        }

        public static float ClampAngle( float angle, float min, float max ) {
            if( min < 0 && max > 0 && ( angle > max || angle < min ) ) {
                angle -= 360;
                if( angle > max || angle < min ) {
                    if( Mathf.Abs( Mathf.DeltaAngle( angle, min ) ) < Mathf.Abs( Mathf.DeltaAngle( angle, max ) ) )
                        return min;
                    else
                        return max;
                }
            }
            else if( min > 0 && ( angle > max || angle < min ) ) {
                angle += 360;
                if( angle > max || angle < min ) {
                    if( Mathf.Abs( Mathf.DeltaAngle( angle, min ) ) < Mathf.Abs( Mathf.DeltaAngle( angle, max ) ) )
                        return min;
                    else
                        return max;
                }
            }

            if( angle < min )
                return min;
            else if( angle > max )
                return max;
            else
                return angle;

        }
    }
}
