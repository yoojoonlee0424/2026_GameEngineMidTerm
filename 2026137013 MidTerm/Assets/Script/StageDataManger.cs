using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class StageResult
{
    public string playerName;
    public int stage;
    public int score;
}

[System.Serializable]
public class StageResultList
{
    public List<StageResult> Results = new List<StageResult>();
}

public static class StageResultSaver
{
    private const string FILE = "stage_results.json"; // 파일 명
    private const string PLAYER_NAME = "PlayerName";  // PlayerPrefs에 사용할 플레이어네임 key
    private static string filePath = Path.Combine(Application.persistentDataPath, FILE);    //저장 경로
    public static void SaveStage(int stage, int score)
    {
        StageResultList list = LoadInternal();

        string playerName = PlayerPrefs.GetString(PLAYER_NAME,"");  //PlayerName 키로 불러오기 (PlayerPrefs)

        //StageResult 타입 데이터 생성
        StageResult entry = new StageResult
        {
            playerName = playerName,
            stage = stage,
            score = score,
        };

        list.Results.Add(entry);    //기존 load 한 데이터에 entry 추가

        string json = JsonUtility.ToJson(list,true);    //다시 Json으로 직렬
        File.WriteAllText(filePath, json);  //filePath에 데이터 저장
        
    }

    public static StageResultList LoadRank()
    {
        return LoadInternal();
    }

    private static StageResultList LoadInternal()
    {
        if(!File.Exists(filePath))  // filePath에 파일이 없다면
        {
            return new StageResultList(); //새로운 리스트 생성
        }

        string json = File.ReadAllText(filePath);  //filePath에 있는 데이터 읽기
        StageResultList list = JsonUtility.FromJson<StageResultList>(json); // json에서 StageResultList 타입으로 테이터 변환

        if(list == null)   
        {
            return new StageResultList(); // 새로 list 생성
        }
        else
        {
            return list; // list 돌려주기
        }
    }
}