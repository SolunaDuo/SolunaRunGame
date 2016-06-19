using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;

/// <summary>
/// - 텍스트 파일에 키값과 데이터 저장
/// - 키값에 해당하는 데이터를 반환
/// - 암호화
/// </summary>

public class Save : MonoBehaviour
{
    public static Save instance = new Save();

    private string sFilePath;    // 데이터 저장할 파일 경로
    private FileStream  fsSaveFile;
    private StreamWriter swDataWrite;
    private StreamReader srDataRead;

    // Use this for initialization

    Save()
    {
        sFilePath = "Assets/Resources/SAVEFILE.txt";

        // SAVEFILE.txt 가 존재하는지 확인
        FileInfo Info = new FileInfo(sFilePath);
        if (!Info.Exists)
        {
            // SAVEFILE.txt 가 존재하지 않아 생성
            File.Create(sFilePath);
        }
    }

    // 키값에 해당하는 위치에 데이터 저장. 키값이 없으면 새로 생성
    public void SetData(string sKey, string sData)
    {
        fsSaveFile = new FileStream(sFilePath, FileMode.Open, FileAccess.Read);
        srDataRead = new StreamReader(fsSaveFile);

        string sAllText;
        string[] sSplit_1;
        string[] sSplit_2 = new string[2];
        char[] charsplit = new char[] { '\r', '\n' };

        sAllText = srDataRead.ReadToEnd();
        sSplit_1 = sAllText.Split(charsplit);
        for (int i = 0; i < sSplit_1.Length; i++)
        {
            sSplit_2 = sSplit_1[i].Split('\t');
            if (sSplit_2[0] == sKey)
            {
                srDataRead.Close();

                sSplit_2[1] = sData;
                sSplit_1[i] = sSplit_2[0] + '\t' + sSplit_2[1];

                fsSaveFile = new FileStream(sFilePath, FileMode.Create, FileAccess.Write);
                swDataWrite = new StreamWriter(fsSaveFile, Encoding.Unicode);
                for (int j = 0; j < sSplit_1.Length; j++)
                {
                    //string sTemp = sSplit_1[j].Split('\r');

                    if (sSplit_1[j] != "")
                        swDataWrite.WriteLine(sSplit_1[j]);
                }
                swDataWrite.Close();
                fsSaveFile.Close();
                return;
            }
        }

        srDataRead.Close();

        fsSaveFile = new FileStream(sFilePath, FileMode.Append, FileAccess.Write);
        swDataWrite = new StreamWriter(fsSaveFile, Encoding.Unicode);
        swDataWrite.WriteLine(sKey + '\t' + sData);
        swDataWrite.Close();
        fsSaveFile.Close();
        return;
    }

    // 키값에 해당하는 데이터를 반환
    public string GetData(string sKey)
    {
        fsSaveFile = new FileStream(sFilePath, FileMode.Open, FileAccess.Read);
        srDataRead = new StreamReader(fsSaveFile);

        string sAllText;
        string[] sSplit_1;
        string[] sSplit_2 = new string[2];
        char[] charsplit = new char[] { '\r', '\n' };

        sAllText = srDataRead.ReadToEnd();
        sSplit_1 = sAllText.Split(charsplit);

        for (int i = 0; i < sSplit_1.Length; i++)
        {
            sSplit_2 = sSplit_1[i].Split('\t');
            if (sSplit_2[0] == sKey)
            {
                srDataRead.Close();
                fsSaveFile.Close();

                return sSplit_2[1];
            }
        }

        srDataRead.Close();
        fsSaveFile.Close();
        return null;
    }
}
