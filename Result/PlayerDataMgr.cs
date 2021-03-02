using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

// PlayerData를 관리하는 스크립트입니다.
// (2) DataSyn Functions : Persis -> Cache / Cache -> Persis
public class PlayerDataMgr
{
    public static PlayerData_SO playerData_SO = Resources.Load<PlayerData_SO>("PlayerData_SO");

    #region PUBLIC METHODS
    // 첫 플레이시, 플레이어 데이터를 첫플레이에 맞게 초기화합니다.
    public static void Init_PlayerData()
    {
        PlayerData data = new PlayerData();


        string JsonData = JsonUtility.ToJson(data, true);

        string path = GetPathFromSaveFile();
        using (FileStream stream = File.Open(path, FileMode.Create))
        {

            byte[] byteData = Encoding.UTF8.GetBytes(JsonData);

            stream.Write(byteData, 0, byteData.Length);

            stream.Close();

            Sync_Persis_To_Cache();
            Debug.Log("PlayerDataMgr: INIT COMPLETE - " + path);
        }



    }

    public static void Sync_Persis_To_Cache()
    {
        PlayerData playerPersisData;
        string path = GetPathFromSaveFile();
        using (FileStream stream = File.Open(path, FileMode.Open))
        {

            byte[] byteData = new byte[stream.Length];

            stream.Read(byteData, 0, byteData.Length);

            stream.Close();

            string JsonData = Encoding.UTF8.GetString(byteData);

            playerPersisData = JsonUtility.FromJson<PlayerData>(JsonData);

        }

        playerData_SO.SongProgress = 0;
        playerData_SO.SongList.Clear();
        //들어갈 cache 초기화

        playerData_SO.SongProgress = playerPersisData.SongProgress;
        foreach (SongData c in playerPersisData.SongList)
        {
            playerData_SO.SongList.Add(c);
        }
        // cache에 삽입

        Debug.Log("PlayerDataMgr: PLAYER_DATA (PERSIS->CACHE) COMPLETE \n " + path);
    }

    public static void Sync_Cache_To_Persis()
    {
        PlayerData playerPersisData = new PlayerData();

        playerPersisData.SongList.Clear();
        playerPersisData.SongProgress = 0;

        foreach (SongData s in playerData_SO.SongList)
        {
            playerPersisData.SongList.Add(s);
        }
        playerPersisData.SongProgress = playerData_SO.SongProgress;

        string JsonData = JsonUtility.ToJson(playerPersisData, true);
        string path = GetPathFromSaveFile();
        using (FileStream stream = File.Open(path, FileMode.Create))
        {

            byte[] byteData = Encoding.UTF8.GetBytes(JsonData);

            stream.Write(byteData, 0, byteData.Length);

            stream.Close();

            Debug.Log("PlayerDataMgr: PLAYER_DATA (CACHE->PERSIS) COMPLETE \n " + path);
        }

    }

    // 플레이어데이터가 있는지 없는지. => 보통 Init_PlayerData() 하기전에 검사용
    public static bool isPlayerDataExist()
    {
        if (File.Exists(GetPathFromSaveFile()))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    #endregion


    #region PRIVATE METHODS

    // Helper Function
    private static string GetPathFromSaveFile()
    {
        return Path.Combine(Application.persistentDataPath, "PlayerData.json");
    }

    #endregion
}
