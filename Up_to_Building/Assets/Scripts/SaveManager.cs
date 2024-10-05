using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;

// 게임 저장 및 로드 기능을 관리하는 SaveManager 클래스
public class SaveManager : MonoBehaviour
{
    // 플레이어 데이터 저장을 위한 클래스 인스턴스
    public PlayerData playerData = new PlayerData();

    
   

    // 게임 매니저 데이터 (게임 상태 관리)
    public GameManagerData GameData = new GameManagerData();

    // 저장 경로와 파일명 설정
    private string path;
    private string filename = "saveGame";

    // 싱글톤 패턴을 적용한 인스턴스
    private static SaveManager instance;
    public static SaveManager Instance
    {
        get
        {
            return instance;
        }
    }

    // 게임 시작 시 SaveManager의 인스턴스를 초기화
    private void Awake()
    {
        // 데이터 저장 경로 설정
        path = Application.persistentDataPath + "/";

        // 싱글톤 패턴: 이미 인스턴스가 존재하지 않는다면 할당
        if (instance == null)
        {
            instance = this;

            // 이 오브젝트를 다른 씬에서도 파괴되지 않도록 설정
            DontDestroyOnLoad(this.gameObject);
        }
    }

    // 게임 데이터를 JSON 형식으로 저장하는 메서드
    public void SaveJson()
    {
        // 플레이어 데이터 JSON으로 변환
        string playerinfo = JsonUtility.ToJson(playerData);

  

     

        // 게임 매니저 데이터를 JSON으로 변환
        string gameData = JsonUtility.ToJson(GameData);

        // 파일에 JSON 데이터 쓰기
        File.WriteAllText(path + filename, playerinfo);
    
        File.WriteAllText(path + "gameData", gameData);

        // 디버그 로그로 저장된 데이터를 출력
     

    }

    // 저장된 데이터를 로드하는 메서드
    public void LoadData()
    {
        // 플레이어, 게임 매니저, 몬스터 데이터를 파일에서 읽어옴
        string data = File.ReadAllText(path + filename);
        string gameData = File.ReadAllText(path + "gameData");
        string m_data = File.ReadAllText((path + "Monsterinfo"));

        // JSON 데이터를 객체로 변환하여 로드
        playerData = JsonUtility.FromJson<PlayerData>(data);
        GameData = JsonUtility.FromJson<GameManagerData>(gameData);
 
    }

    // 새로운 게임을 시작할 때 데이터를 초기화
    public void newGame_DeleteJson()
    {
        // 저장된 파일이 있으면 데이터 초기화
        if (File.Exists(path + filename))
        {
            playerData = new PlayerData();
            GameData = new GameManagerData();
        }
    }
}
