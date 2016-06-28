using UnityEngine;
using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;

public enum KEY
{
    SCORE = 0,
    HIGHSCORE,

    MAX
}

public class Save : MonoBehaviour
{
    private static List<string> sKeyList = new List<string> ();  // 데이터를 찾을 키값 저장 리스트
    private static byte [] btSecKey; // 암호화 키 저장 배열
    private static bool bInit;

    // Use this for initialization
    void Awake ()
    {
        DontDestroyOnLoad ( gameObject );

        bInit = false;
        btSecKey = ASCIIEncoding.ASCII.GetBytes ( "btSecKey" );    // 암호화 키
    }

    public static void SaveData<T> ( KEY eKey, T Data )
    {
        BinaryFormatter bFormatter = new BinaryFormatter ();
        MemoryStream memStream = new MemoryStream ();

        bFormatter.Serialize ( memStream, Data );

        // ---------------------------
        // 암호화는 이 부분에서 진행
        //string sData;
        //sData = Encrypt(Convert.ToBase64String(memStream.GetBuffer()));
        // ---------------------------

        PlayerPrefs.SetString ( GetKey ( eKey ), Convert.ToBase64String ( memStream.GetBuffer () ) );
    }

    public static T LoadData<T> ( KEY eKey )
    {
        string sTemp;
        T temp = default ( T );

        sTemp = PlayerPrefs.GetString ( GetKey ( eKey ) );
        //sTemp = Decrypt(sTemp);


        if ( !string.IsNullOrEmpty ( sTemp ) )
        {
            BinaryFormatter bFormatter = new BinaryFormatter ();
            MemoryStream memStream = new MemoryStream ( Convert.FromBase64String ( sTemp ) );

            temp = (T) bFormatter.Deserialize ( memStream );
        }
        else
        {
            Debug.Log ( "Data is Empty. (Key : " + eKey + ")" );
        }

        return temp;
    }

    private static void Init ()
    {
        TextAsset txtFile = (TextAsset) Resources.Load ( "SAVEFILE" );

        if ( txtFile != null )
        {
            char [] cSplitTarget1 = new char [] { '\n', '\r' };
            char [] cSplitTarget2 = new char [] { '\t' };
            string [] sText1;
            string [] sText2;

            sText1 = txtFile.text.Split ( cSplitTarget1, System.StringSplitOptions.RemoveEmptyEntries );

            for ( int i = 0; i < (int) KEY.MAX; i++ )
            {
                sText2 = sText1 [ i ].Split ( cSplitTarget2, System.StringSplitOptions.RemoveEmptyEntries );

                if ( sText2 [ 1 ] == null )
                    break;

                sKeyList.Add ( sText2 [ 1 ] );
            }
        }
        bInit = true;
    }

    private static string GetKey ( KEY eKey )
    {
        if ( !bInit || sKeyList == null )
            Init ();

        return sKeyList [ (int) eKey ];
    }

    private static string Encrypt ( string sData )
    {
        MemoryStream memStream = new MemoryStream ();
        RC2 rc2 = new RC2CryptoServiceProvider ();

        rc2.Key = btSecKey;
        rc2.IV = btSecKey;

        CryptoStream ctStream = new CryptoStream ( memStream, rc2.CreateEncryptor (), CryptoStreamMode.Write );

        byte [] btData = Encoding.UTF8.GetBytes ( sData.ToCharArray () );

        ctStream.Write ( btData, 0, btData.Length );
        ctStream.FlushFinalBlock ();

        return Convert.ToBase64String ( memStream.GetBuffer () );
    }

    private static string Decrypt ( string sData )
    {
        MemoryStream memStream = new MemoryStream ();
        RC2 rc2 = new RC2CryptoServiceProvider ();

        rc2.Key = btSecKey;
        rc2.IV = btSecKey;

        CryptoStream ctStream = new CryptoStream ( memStream, rc2.CreateDecryptor (), CryptoStreamMode.Read );

        byte [] btData = Convert.FromBase64String ( sData );

        ctStream.Read ( btData, 0, btData.Length );
        ctStream.FlushFinalBlock ();

        return Encoding.UTF8.GetString ( memStream.GetBuffer () );
    }
}
