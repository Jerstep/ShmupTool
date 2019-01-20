using System.IO;
using UnityEngine;

public static class SaveSystem
{
    public static void SaveWaves(WaveController controller)
    {        
        string path = Application.dataPath + "/Resources/Waves.json";

        WaveData data = new WaveData(controller);

        string jsonData = JsonUtility.ToJson(data);
        File.WriteAllText(path, jsonData);
    }

    public static WaveData LoadWaves()
    { 
        string path = Application.dataPath + "/Resources/Waves.json";

        if(File.Exists(path))
        {
            var data = File.ReadAllText(path);
            WaveData waveData = JsonUtility.FromJson<WaveData>(data);
            return waveData;
        }
        else
        {
            Debug.LogError("Save file not found at " + path);
            return null;
        }
    }

}
