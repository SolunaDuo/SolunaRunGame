using UnityEngine;
using System.Collections;

public class Hundle : MonoBehaviour
{
    public GameObject[] obj_Hundles;    // 장애물들

    public float[] fDropHundles;           // 떨어지는 장애물이 언제 생성될지

    private int nDropidx = 0;
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameObject.activeSelf)
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
        gameObject.SetActive(true);
        transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);

        nDropidx = 0;
        HurdleMnager.instance.fDangerTime = GameManager.instance.fMapSpeed / 10;
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
            HurdleMnager.instance.obj_Drop.transform.localPosition -= new Vector3(0.0f, GameManager.instance.fMapSpeed * 2f * Time.deltaTime);

            yield return new WaitForEndOfFrame();
        }

        HurdleMnager.instance.obj_Drop.gameObject.SetActive(false);
        HurdleMnager.instance.obj_Drop.transform.localPosition = new Vector3(0.0f, 7.41f, 0.0f);
    }
}
