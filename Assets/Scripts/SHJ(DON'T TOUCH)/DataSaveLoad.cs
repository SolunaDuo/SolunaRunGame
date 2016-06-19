using UnityEngine;
using System.Collections;
using System.Collections.Generic; 
  
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


/*
제작자  : 서형준
만든 날 : 2016-03-24
기능    : 데이터 저장,로드하는 스크립트.
*/

public class DataSaveLoad : MonoBehaviour
{
    public static DataSaveLoad ins;
    void Awake()
    {
        ins = this;
        DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {

	}

    public void SaveData<T>(List<T> temp,string szKey)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        MemoryStream memoryStream = new MemoryStream();

        // 지정된 리스트를 바이트 배열로 바꿔줌
        binaryFormatter.Serialize(memoryStream, temp);

        if (memoryStream == null)
        {
            Debug.Log("Date Save error! your key : " + szKey);
            return;
        }

        // 문자열 값으로 변환하여 저장
        PlayerPrefs.SetString(szKey, Convert.ToBase64String(memoryStream.GetBuffer() ));
    }

    public void LoadData<T>(ref List<T> temp,string szKey)
    {
        string data = PlayerPrefs.GetString(szKey);

        if(!string.IsNullOrEmpty(data))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(data));
            
            // 가져온 데이터를 바이트 배열로 변환후 캐스팅
            temp = (List<T>)binaryFormatter.Deserialize(memoryStream);
        }
        else
        {
            Debug.Log("Date Load error! your key : " + szKey);
        }
    }

    // 

}
