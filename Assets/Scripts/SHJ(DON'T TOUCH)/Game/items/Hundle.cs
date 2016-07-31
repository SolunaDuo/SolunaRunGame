using UnityEngine;
using System.Collections;

public class Hundle : MonoBehaviour
{
    public GameObject[] obj_Hundles;    // 장애물들
    public Transform[] tr_Hundles;    // 장애물들

    public float[] fDropHundles;           // 떨어지는 장애물이 언제 생성될지

    private int nDropidx = 0;

    private Transform tr_topHundle; // 가장 높은 장애물
    // Use this for initialization
    void Start()
    {
        tr_Hundles = new Transform[gameObject.transform.childCount];
        tr_Hundles = gameObject.transform.GetComponentsInChildren<Transform>();
        for(int i=0; i < tr_Hundles.Length; ++i )
        {
            obj_Hundles[i] = tr_Hundles[i].gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameObject.activeSelf || !GameManager.instance.bPlayMode)
            return;
        if (fDropHundles == null || fDropHundles.Length <= nDropidx)
            return;

        if (fDropHundles[nDropidx] >= gameObject.transform.localPosition.y)
        {
            StartCoroutine(DropMove());
            nDropidx++;
        }
    }

    public void Init()
    {
        for(int i=0; i< obj_Hundles.Length; ++i)
        {
            obj_Hundles[i].transform.position = tr_Hundles[i].position;
            obj_Hundles[i].SetActive(true);
        }
        gameObject.SetActive(true);
        transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);

        nDropidx = 0;
        HurdleMnager.instance.fDangerTime = GameManager.instance.fGlobalSpeed / 10;
    }

    IEnumerator DropMove()
    {
        yield return new WaitForEndOfFrame();
        float elspetime = 0.0f;

        HurdleMnager.instance.obj_Danger.gameObject.SetActive(true);

        while (elspetime <= HurdleMnager.instance.fDangerTime)
        {
            elspetime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        elspetime = 0f;
        HurdleMnager.instance.obj_Danger.gameObject.SetActive(false);


        HurdleMnager.instance.obj_Drop.gameObject.SetActive(true);
        HurdleMnager.instance.obj_Drop.transform.localPosition = new Vector3(0.0f, 7.41f, 0.0f);
        while (HurdleMnager.instance.obj_Drop.transform.localPosition.y >= -(HurdleMnager.instance.Range * 2f))
        {
            HurdleMnager.instance.obj_Drop.transform.localPosition -= new Vector3(0.0f, GameManager.instance.fGlobalSpeed * 1.5f * Time.deltaTime);

            yield return new WaitForEndOfFrame();
        }

        HurdleMnager.instance.obj_Drop.gameObject.SetActive(false);
        HurdleMnager.instance.obj_Drop.transform.localPosition = new Vector3(0.0f, 7.41f, 0.0f);
    }

    void SetTopHundle()
    {
        Transform obj_temp = tr_Hundles[0];

        //0번째는 자기 자신
        for (int i=1; i< tr_Hundles.Length;++i)
        {
            if (obj_temp.transform.localPosition.y <= tr_Hundles[i].localPosition.y)
                obj_temp = tr_Hundles[i];
        }
        tr_topHundle = obj_temp;
    }

   public Transform GetTopTran()
    {
        if (tr_topHundle == null)
            SetTopHundle();

        return tr_topHundle;
    }
}
