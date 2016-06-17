using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/*
제작자  : 서형준
만든 날 : 2016-03-30
기능    : 코루틴으로 동작해야하는것들(오브젝트 무브, 알파 변경 등..)을 모와두는 매니저.
          필요하시면 더 추가해 주세요.
*/

public class CoroutineManager : MonoBehaviour {
	// Use this for initialization
	void Awake () {
		DontDestroyOnLoad (gameObject);
	}

    // 점점 느려지는 움직임
    // time : 총시간, fFirstspeed : 처음 배속 이후 처음 배속 -> 0 으로 됨
    public IEnumerator TestObj(GameObject go, float time, float fFirstspeed, Vector3 startPos, Vector3 EndPos)
    {
        yield return new WaitForSeconds(0.0001f);
        float elapseTime = 0f;
        float fPlustime = fFirstspeed;
        float fEndCount = 0f;
        while (fEndCount < time)
        {
            fEndCount += Time.deltaTime;
            elapseTime += Time.deltaTime * fPlustime;
            if (fEndCount >= time)
                fEndCount = time;

            go.transform.localPosition = startPos + (EndPos - startPos) * (elapseTime / time);
            fPlustime = fFirstspeed + (0f - fFirstspeed) * (elapseTime / time);

            yield return new WaitForEndOfFrame();
        }

    }

    public IEnumerator objMoveLocal(GameObject go,Vector3 startPos, Vector3 endPos, float time)
	{
		yield return new WaitForEndOfFrame ();
		float elapsetime = 0.0f;
		
		while (elapsetime < time) 
		{
			elapsetime += Time.deltaTime;
			Vector3 Temp;
			if(elapsetime >= time )
				elapsetime = time;

            Temp = startPos + (endPos - startPos) * (elapsetime/time);
			go.transform.localPosition = Temp; 
			
			yield return new WaitForEndOfFrame ();
		} 
		go.transform.localPosition = endPos;
	}

	public IEnumerator objMove<T>(GameObject go,Vector3 startPos, Vector3 endPos, float time, T temp ,string method = "", GameObject obj = null)
	{
		yield return new WaitForEndOfFrame ();
		float elapsetime = 0.0f;
		
		while (elapsetime < time) 
		{
			elapsetime += Time.deltaTime;
			Vector3 Temp;
			if(elapsetime >= time )
				elapsetime = time;
			
			Temp = startPos + (endPos - startPos) * (elapsetime/time);
			go.transform.position = Temp; 
			
			yield return new WaitForEndOfFrame ();
		} 
		go.transform.localPosition = endPos;
	}

	// 정해진 방향으로 계속 움직임..
	public IEnumerator objMoveContinue(GameObject go,Vector3 dis, float Speed)
	{
		yield return new WaitForEndOfFrame ();
		float elapsetime = 0.0f;
		
		while (go != null) 
		{
			elapsetime += Time.deltaTime;

			go.transform.position += dis * Speed; 
			
			yield return new WaitForEndOfFrame ();
		} 
	}

	public IEnumerator objMoveContinueLocal(GameObject go,Vector3 dis, float Speed,float time)
	{
		yield return new WaitForEndOfFrame ();
		float elapsetime = 0.0f;
		
		while (elapsetime < time) 
		{
			if(elapsetime >= time )
				elapsetime = time;
			elapsetime += Time.deltaTime;
			
			go.transform.localPosition += dis * Speed; 
			
			yield return new WaitForEndOfFrame ();
		} 
	}

	public IEnumerator objMoveContinueLocal(GameObject go,Vector3 dis, float Speed)
	{
		yield return new WaitForEndOfFrame ();
		float elapsetime = 0.0f;
		
		while (go != null) 
		{
			elapsetime += Time.deltaTime;
			
			go.transform.localPosition += dis * Speed; 
			
			yield return new WaitForEndOfFrame ();
		} 
	}

	//dis true 면 점점 어두 워짐..
	public IEnumerator ImgAlpha(GameObject go,bool Dis, float time)
	{
		yield return new WaitForEndOfFrame ();
		float elapsetime = 0.0f;
		Image ImTemp = go.GetComponent<Image> ();
		while (elapsetime < time) 
		{
			elapsetime += Time.deltaTime;

			if(elapsetime >= time )
				elapsetime = time;
			if(Dis)
				ImTemp.color = new Color(0.0f,0.0f,0.0f, 1 - (elapsetime/time));
			else
				ImTemp.color = new Color(0.0f,0.0f,0.0f, (elapsetime/time));
			
			yield return new WaitForEndOfFrame ();
		} 
	}

	// 알파 idling..
	public IEnumerator sprAlphaIdling(GameObject go,bool Dis, float time,float Max, float Min,bool roop,int roopCount)
	{
		//yield return new WaitForEndOfFrame ();
		float elapsetime = 0.0f;
		int Count = 0;
		SpriteRenderer sprTemp = go.GetComponent<SpriteRenderer> ();
		while (roop || Count < roopCount) 
		{
			elapsetime += Time.deltaTime;
			
			if(elapsetime >= time )
				elapsetime = time;
			if(Dis)
			{
				sprTemp.color = new Color(sprTemp.color.r,sprTemp.color.g,sprTemp.color.b, Max - (elapsetime/time));

				if(sprTemp.color.a < Min )
				{
					elapsetime = 0.0f;
					Count++;
					Dis = false;
				}
			}
			else
			{
				sprTemp.color = new Color(sprTemp.color.r,sprTemp.color.g,sprTemp.color.b,	Min + (elapsetime/time));
				if(sprTemp.color.a > Max )
				{
					elapsetime = 0.0f;
					Count++;
					Dis = true;
				}
			}
			
			yield return new WaitForEndOfFrame ();
		} 
	}

	public IEnumerator sprAlpha<T>(GameObject go,bool Dis, float time,T Factor,string method = "",GameObject game = null)
	{
		//yield return new WaitForEndOfFrame ();
		float elapsetime = 0.0f;
		SpriteRenderer sprTemp = go.GetComponent<SpriteRenderer> ();
		while (elapsetime < time) 
		{
			elapsetime += Time.deltaTime;
			
			if(elapsetime >= time )
				elapsetime = time;
			if(Dis)
				sprTemp.color = new Color(0.0f,0.0f,0.0f, 1 - (elapsetime/time));
			else
				sprTemp.color = new Color(0.0f,0.0f,0.0f, (elapsetime/time));
			
			yield return new WaitForEndOfFrame ();
		} 
		if( !method.Equals(""))
		{
			game.SendMessage(method,Factor);
		}
	}

	public IEnumerator objRoate(GameObject go,Vector3 startPos, Vector3 endPos, float time)
	{
		yield return new WaitForEndOfFrame ();
		float elapsetime = 0.0f;
		
		while (elapsetime < time) 
		{
			elapsetime += Time.deltaTime;
			Vector3 Temp;
			if(elapsetime >= time )
				elapsetime = time;
			
			Temp = startPos + (endPos - startPos) * (elapsetime/time);
			go.transform.eulerAngles = Temp; 
			
			yield return new WaitForEndOfFrame ();
		} 
	}

	public IEnumerator objRoatePingPong(GameObject go,Vector3 startPos, Vector3 endPos, float time)
	{
		yield return new WaitForEndOfFrame ();
		float elsptime = 0.0f;
		bool bTurn = false;

		while (true) 
		{
			Vector3 Temp;

			if( !bTurn )
			{
				elsptime += Time.deltaTime;
				if(elsptime >= time)
				{
					elsptime = time;
					bTurn = !bTurn;
				}
				Temp = startPos + (endPos - startPos) * (elsptime/time);
			}
			else
			{
				elsptime -= Time.deltaTime;
				if(elsptime <= 0.0f)
				{
					elsptime = 0.0f;
					bTurn = !bTurn;
				}
				Temp = startPos + (endPos - startPos) * (elsptime/time);
			}

			go.transform.eulerAngles = Temp; 
			
			yield return new WaitForEndOfFrame ();
		} 
	}

	public IEnumerator objRoatePingPong(GameObject go,Vector3 startPos, Vector3 endPos, float time,int Count)
	{
		yield return new WaitForEndOfFrame ();
		float elsptime = 0.0f;
		bool bTurn = false;
		int Temp_Count = 0;
		while (Temp_Count < Count) 
		{
			Vector3 Temp;
			
			if( !bTurn )
			{
				elsptime += Time.deltaTime;
				if(elsptime >= time)
				{
					elsptime = time;
					bTurn = !bTurn;
					Temp_Count++;
				}
			}
			else
			{
				elsptime -= Time.deltaTime;
				if(elsptime <= 0.0f)
				{
					elsptime = 0.0f;
					bTurn = !bTurn;
					Temp_Count++;
				}
			}
			Temp = startPos + (endPos - startPos) * (elsptime/time);
			go.transform.eulerAngles = Temp; 
			
			yield return new WaitForEndOfFrame ();
		} 
	}

	// 지정된 원을 도는 함수.. 글로벌 좌표..
	public IEnumerator CircleMove(GameObject go,float CenterX, float CenterY, float radius, float time, int Count, bool loop )
	{
		yield return new WaitForEndOfFrame ();
		float elapsetime = 0.0f;
		int Temp_Count = 0;
		while (loop || Count > Temp_Count) 
		{
			Vector3 Temp;
			elapsetime += Time.deltaTime;

			if(elapsetime >= time )
			{
				Temp_Count++;
				elapsetime = 0.0f;
			}

			Temp = new Vector3(CenterX + radius * Mathf.Cos(Mathf.PI * ( (elapsetime*2f) / time) ),
			                   CenterY + radius * Mathf.Sin(Mathf.PI * ( (elapsetime*2f) / time) ),0.0f );
			go.transform.position = Temp;
			yield return new WaitForEndOfFrame ();
		}
	}
	// 지정된 원을 도는 함수.. 로컬 좌표..
	public IEnumerator LocalCircleMove(GameObject go,float CenterX, float CenterY, float radius, float time )
	{
		yield return new WaitForEndOfFrame ();
		float elapsetime = 0.0f;
		
		while (elapsetime < time) 
		{
			Vector3 Temp;
			elapsetime += Time.deltaTime;
			
			if(elapsetime >= time )
				elapsetime = time*2f;
			
			Temp = new Vector3(CenterX + radius * Mathf.Cos(Mathf.PI * ( (elapsetime*2f) / time) ),
			                   CenterY + radius * Mathf.Sin(Mathf.PI * ( (elapsetime*2f) / time) ),0.0f );
			go.transform.localPosition = Temp;
			yield return new WaitForEndOfFrame ();
		}
	}
}
