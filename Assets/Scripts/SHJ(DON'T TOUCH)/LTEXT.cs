using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/*
제작자  : 서형준
만든 날 : 2016-03-23
기능    : 텍스트를 읽어오는 스크립트
*/

public enum LTEXTIDX
{
    T_BUY_EMPLOYEE = 0,

    T_MAX,
}

public enum KEYSTR
{
    K_EMP = 0,
    T_PLUSMONEY,
    K_GAME,

    K_MAX
}
public class LTEXT : MonoBehaviour {

    private static List<string> ltext = null;
    private static List<string> KeyText = null;

    private static bool bInit = false;

    private static void Init(string szFile,int loadidx = 0) 
    {
        TextAsset texAsst = (TextAsset)Resources.Load(szFile);

        if (loadidx == 0)
        {
            if (ltext == null)
            {
                ltext = new List<string>();
            }
            else
            {
                ltext.Clear();
                bInit = false;
            }
        }
        else if (loadidx == 1)
        {
            if (KeyText == null)
            {
                KeyText = new List<string>();
            }
            else
            {
                KeyText.Clear();
                bInit = false;
            }
        }

        if (texAsst != null)
        {
            char[] charsplit = new char[] {'\r','\n'};
            string[] token1;
            token1 = texAsst.text.Split(charsplit, System.StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < token1.Length; ++i)
            {
                char[] charsplit2 = new char[] { ',', '\t' };
                string[] token2 = token1[i].Split(charsplit2, System.StringSplitOptions.RemoveEmptyEntries);
                if (loadidx == 0)
                {
                    if (token2 != null)
                    {
                        ltext.Add(token2[1]);
                    }
                    else
                    {
                        ltext.Add("null");
                    }
                }
                else if(loadidx == 1)
                {
                    if (token2 != null)
                    {
                        KeyText.Add(token2[1]);
                    }
                    else
                    {
                        KeyText.Add("null");
                    }
                }
            }   
        }
        bInit = true;
    }

    public static void LoadUIText(string szFile)
    {
        Init(szFile,0);
    }

    public static void LoadKeyText(string szFile)
    {
        Init(szFile,1);
    }

    public static string GetUI(LTEXTIDX i)
    {
        if (!bInit) return "null";

        if(ltext.Count < (int)i)
        {
            return "null";
        }

        return ltext[(int)i];
    }

    public static string GetKey(KEYSTR i)
    {
        if (!bInit) return "null";

        if (KeyText.Count < (int)i)
        {
            return "null";
        }

        return KeyText[(int)i];
    }
}
