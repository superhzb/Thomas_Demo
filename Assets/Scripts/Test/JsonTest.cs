using LitJson;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JsonTest : MonoBehaviour
{
    DataClass data1 = new DataClass
    {
        a = 1,
        b = true,
        cList = new List<DataClass.UIPath>() {
            new DataClass.UIPath{name="ui1",path="uipath1" },
            new DataClass.UIPath{name="ui2",path="uipath2" }
        }
    };

    private void Start()
    {
        SaveToJson();

        DataClass dataLoad = LoadFromJson();
        Debug.Log(dataLoad.a);
        Debug.Log(dataLoad.b);
        Debug.Log(dataLoad.cList[0].name + "/" + dataLoad.cList[0].path);
        Debug.Log(dataLoad.cList[1].name + "/" + dataLoad.cList[1].path);

    }

    private void SaveToJson()
    {
        string path = Application.dataPath + "/StreamingFiles/Json/dataFile.json";
        string saveJsonStr = JsonMapper.ToJson(data1);
        StreamWriter sw = new StreamWriter(path);
        sw.Write(saveJsonStr);
        sw.Close();
    }

    private DataClass LoadFromJson()
    {
        DataClass data = new DataClass();
        string path= Application.dataPath + "/StreamingFiles/Json/dataFile.json";
        if (File.Exists(path))
        {
            StreamReader sr = new StreamReader(path);
            string jsonStr = sr.ReadToEnd();
            sr.Close();
            data = JsonMapper.ToObject<DataClass>(jsonStr);
        }
        if (data == null)
        {
            Debug.Log("Failed to load Json File.");
        }
        return data;
    }
}

public class DataClass
{
    public int a;
    public bool b = true;
    public List<UIPath> cList;
    public struct UIPath
    {
        public string name;
        public string path;
    }
}
