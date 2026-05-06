using TMPro;
using UnityEditor;
using UnityEngine;

public class LeaderBoard : MonoBehaviour
{


    public GameObject Leader;

    public GameObject stage1;
    public GameObject stage2;
    public GameObject stage3;
    public GameObject stage4;
    public GameObject stage5;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Leader.SetActive(false);
        stage1.SetActive(false);
        stage2.SetActive(false);
        stage3.SetActive(false);
        stage4.SetActive(false);
        stage5.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void OpenLeader()
    {
        Leader.SetActive(true);
    }

    public void CloseLeader()
    {
        Leader.SetActive(false);
    }

    public void OpenStage1()
    {
        stage1.SetActive (true);
    }

    public void OpenStage2()
    {
        stage2.SetActive(true);
    }

    public void OpenStage3()
    {
        stage3.SetActive(true);
    }

    public void OpenStage4()
    {
        stage4.SetActive(true);
    }

    public void OpenStage5()
    {
        stage5.SetActive(true);
    }


    public void CloseStage()
    {
        stage1.SetActive (false);
        stage2.SetActive (false);
        stage3.SetActive(false);
        stage4.SetActive(false);
        stage5.SetActive(false);
    }

}
