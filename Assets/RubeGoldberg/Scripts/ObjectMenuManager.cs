using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMenuManager : MonoBehaviour {
    public List<GameObject> objectList;
    public List<GameObject> objectPrefabList;
    public List<int> objectGenerationLimitList;

    public int currentObject = 0;

    [SerializeField]
    private float instantiateDistance = 1f;
    private float showTime = 2f;

    // Use this for initialization
    void Start() {
    }

    public void Show(bool show)
    {
        objectList[currentObject].SetActive(show);
    }

    public void MenuLeft()
    {
        objectList[currentObject].SetActive(false);
        currentObject--;
        if (currentObject < 0)
        {
            currentObject = objectList.Count - 1;
        }
        StartCoroutine(ShowActiveMenuItem(showTime, objectList[currentObject]));
        //objectList[currentObject].SetActive(true);
    }

    public void MenuRight()
    {
        objectList[currentObject].SetActive(false);
        currentObject++;
        if (currentObject > objectList.Count - 1)
        {
            currentObject = 0;
        }
        StartCoroutine(ShowActiveMenuItem(showTime, objectList[currentObject]));
        //objectList[currentObject].SetActive(true);
    }

    private IEnumerator ShowActiveMenuItem(float time, GameObject menuItem)
    {
        menuItem.SetActive(true);
        yield return new WaitForSeconds(time);
        menuItem.SetActive(false);
    }

    public void Create()
    {
        if (objectGenerationLimitList[currentObject] > 0)
        {
            Instantiate(objectPrefabList[currentObject], this.transform.parent.position + this.transform.parent.forward * instantiateDistance, objectPrefabList[currentObject].transform.rotation);
            objectGenerationLimitList[currentObject]--;
        }
    }
}
