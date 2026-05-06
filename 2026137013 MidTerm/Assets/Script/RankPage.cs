using System.Linq;
using UnityEngine;
using TMPro;

public class RankPage : MonoBehaviour
{
    [SerializeField] Transform contentRoot;
    [SerializeField] GameObject rowPrefab;

    public int StageNum;

    StageResultList allDate;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        allDate = StageResultSaver.LoadRank();
        RefreshRankList(StageNum);
    }

    void RefreshRankList(int _stage)
    {
        //모든 기본 오브젝트 삭제
        foreach(Transform child in contentRoot)
        {
            Destroy(child.gameObject);
        }

        // 랭크 데이터 정렬 (스테이지1)
        var sortedDate = allDate.Results.Where(r => r.stage == _stage).OrderByDescending(x => x.score).ToList();

        

        //랭크 데이터 생성
        for(int i = 0; i < sortedDate.Count; i++)
        {
            GameObject row = Instantiate(rowPrefab, contentRoot);
            TMP_Text rankText = row.GetComponentInChildren<TMP_Text>();
            // 텍스트 형식
            rankText.text = $"{i + 1}위. 이름: {sortedDate[i].playerName} 스코어: {sortedDate[i].score}";

        }

    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
