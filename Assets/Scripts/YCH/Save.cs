using UnityEngine;
using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;

public enum KEY
{
    SCORE = 0,
    HIGHSCORE,

    MAX
}

public class Save : MonoBehaviour
{
    private static List<string> sKeyList = new List<string>();
    private static bool bInit;

    // Use this for initialization
    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        bInit = false;
    }

    public static void SaveData<T>(KEY eKey, T Data)
    {
        // ---------------------------
        // 암호화는 이 부분에서 진행
        
        // ---------------------------

        BinaryFormatter bFormatter = new BinaryFormatter();
        MemoryStream memStream = new MemoryStream();

        bFormatter.Serialize(memStream, Data);

        PlayerPrefs.SetString(GetKey(eKey), Convert.ToBase64String(memStream.GetBuffer()));
    }

    public static T LoadData<T>(KEY eKey)
    {
        string sTemp;
        T temp = default(T);

        sTemp = PlayerPrefs.GetString(GetKey(eKey));


        if (!string.IsNullOrEmpty(sTemp))
        {
            BinaryFormatter bFormatter = new BinaryFormatter();
            MemoryStream memStream = new MemoryStream(Convert.FromBase64String(sTemp));

            temp = (T)bFormatter.Deserialize(memStream);
        }
        else
        {
            Debug.Log("Data is Empty. (Key : " + eKey + ")");
        }

        return temp;
    }

    private static void Init()
    {
        TextAsset txtFile = (TextAsset)Resources.Load("SAVEFILE");

        if (txtFile != null)
        {
            char[] cSplitTarget1 = new char[] { '\n', '\r' };
            char[] cSplitTarget2 = new char[] { '\t' };
            string[] sText1;
            string[] sText2;

            sText1 = txtFile.text.Split(cSplitTarget1, System.StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < (int)KEY.MAX; i++)
            {
                sText2 = sText1[i].Split(cSplitTarget2, System.StringSplitOptions.RemoveEmptyEntries);

                if (sText2[1] == null)
                    break;

                sKeyList.Add(sText2[1]);
            }
        }
        bInit = true;
    }

    private static string GetKey(KEY eKey)
    {
        if (!bInit || sKeyList == null)
            Init();

        return sKeyList[(int)eKey];
    }
}
