using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveManager : MonoBehaviour
{

    public static SaveManager instance { get; private set; }
    //“о что мы хотим сохранить.
    [EditorButton(nameof(DellAllSave), "Dellete Save", activityType: ButtonActivityType.OnPlayMode)]
    [EditorButton(nameof(Save), "Save", activityType: ButtonActivityType.OnPlayMode)]
    public int CurrentGun;
    public bool[] BoughtGuns;
    public int CurrentLvl;
    public GunsDataSo GunsData;
    public float Money;
    public bool MusicTogle=true;
    public bool SoundTogle=true;
    public bool NewLocation = false;
    public bool TutorialEnded=false;
    public int CurrentLevelCounter=1;
    public int AllLevelCounter = 1;
    public string CurrentGaveVersion = "1.0";

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);
        else
            instance = this;
        DontDestroyOnLoad(gameObject);
        Load();
    }

    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            BinaryFormatter binaryFormater = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
            PlayerData_Storage data = (PlayerData_Storage)binaryFormater.Deserialize(file);
            if (data.BoughtGuns == null)
            {

                BoughtGuns = new bool[13] {true,false,false,false,
                                       false,false,false,false,false,false,false,false,false};
                data.MusicTogle=true;
                data.SoundTogle = true;
                data.NewLocation = false;
                data.CurrentLevelCounter = 1;
}
            else
            {
                BoughtGuns = data.BoughtGuns;
            }
            for (int i = 0; i < GunsData.GunsData.Length; i++)
            {
                GunsData.GunsData[i].IsBought = BoughtGuns[i];
            }
            if (data.CurrentLvl <= 0)
            {
                CurrentLvl = 1;
            }
            else
            {
                CurrentLvl = data.CurrentLvl;
            }
            if(data.CurrentLevelCounter == 0)
            {
                data.CurrentLevelCounter = 1;
            }
            CurrentGun = data.CurrentGun;

            Money = data.Money;
            NewLocation = data.NewLocation;
            MusicTogle = data.MusicTogle;
            SoundTogle = data.SoundTogle;
            TutorialEnded = data.TutorialEnded;
            CurrentLevelCounter = data.CurrentLevelCounter;
            CurrentGaveVersion = data.CurrentGaveVersion;
            file.Close();

        }
        else
        {
            DellAllSave();
        }

    }
    public void Save()
    {
        BinaryFormatter binaryFormater = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");
        PlayerData_Storage data = new PlayerData_Storage();
        data.CurrentGun = CurrentGun;
        data.CurrentLvl = CurrentLvl;
        data.BoughtGuns = BoughtGuns;
        data.NewLocation = NewLocation;
        for (int i = 0; i < GunsData.GunsData.Length; i++)
        {
            GunsData.GunsData[i].IsBought = BoughtGuns[i];
        }
        data.Money = Money;
        data.MusicTogle = MusicTogle;
        data.SoundTogle = SoundTogle;
        data.TutorialEnded = TutorialEnded;
        data.CurrentLevelCounter = CurrentLevelCounter;
        data.CurrentGaveVersion = CurrentGaveVersion;
        binaryFormater.Serialize(file, data);
        file.Close();
    }
    //ћетод возврата данных к заводским установкам.
    public void DellAllSave()
    {
        BinaryFormatter binaryFormater = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");
        PlayerData_Storage data = new PlayerData_Storage();
        data.CurrentGun = 0;
        data.CurrentLvl = -1;
        data.BoughtGuns = null;
        data.Money = 0;
        data.MusicTogle = true;
        data.SoundTogle = true;
        data.NewLocation = false;
        data.TutorialEnded = false;
        data.CurrentGaveVersion = CurrentGaveVersion;
        binaryFormater.Serialize(file, data);
        file.Close();
        Load();
    }

}



//Ѕаза данных(должна дублировать данные из SaveManager)
[Serializable]
class PlayerData_Storage
{
    public int CurrentGun;
    public bool[] BoughtGuns;
    public int CurrentLvl;
    public float Money;
    public bool MusicTogle;
    public bool SoundTogle;
    public bool NewLocation;
    public bool TutorialEnded;
    public int CurrentLevelCounter;
    public string CurrentGaveVersion;
}
