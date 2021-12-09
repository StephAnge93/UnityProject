using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Data;

public class Level_Generater : MonoBehaviour {

    public static Level_Generater Instance;

    public List<GameObject> Objects = new List<GameObject>();
    public List<GameObject> Hints = new List<GameObject>();

    float Screen_W;
    float Screen_H;

	// Use this for initialization
    void Start () {

        Instance = this;

        Read_Level_File(PlayerPrefs.GetInt("Cureent_Level", 1));
	}
    public void Read_Level_File(int LevelNo)
    {
        //string path = Application.dataPath + "/Resources/Level_" + LevelNo + ".txt";
        //StreamReader reader = new StreamReader(path);
        //string text = reader.ReadToEnd();
           
        //TextAsset Data1 = Resources.Load("AAA") as TextAsset;
        //string text1 = Data1.text;
        //string[] text_Array1 = text1.Split('$');

        TextAsset Data = Resources.Load("Level_" + LevelNo) as TextAsset;
        string text = Data.text;
        string[] text_Array = text.Split('\n');

        //for (int j = 0; j < text_Array1.Length; j++)
        //{
        //    string[] text_Array2 = text_Array1[j].Split('\n');
        //    text_Array2 = text_Array2.Where((item, index) => item != "").ToArray();
        //    if (text_Array2[0] == LevelNo.ToString())
        //    {
        //        Dynamic_Data = true;
        //        break;
        //    }else{
        //        Dynamic_Data = false;
        //    }
        //}

        for (int i = 1; i < text_Array.Length-1; i++)
        {
            string[] obj_info = text_Array[i].Split(' ');

            if (Objects.Find(obj => obj.name == obj_info[0].ToString()))
            {
                //GameObject obj = Instantiate(Objects[int.Parse(obj_info[0]) - 1]);
                GameObject obj = (GameObject)Instantiate(Resources.Load("Object/" + obj_info[0].ToString()));

                float Sc_X = Filter_Data(obj_info[7], obj);
                float Sc_Y = Filter_Data(obj_info[8], obj);
                float Sc_Z = Filter_Data(obj_info[9], obj);
                Vector3 Scale = new Vector3(Sc_X, Sc_Y, Sc_Z);
                obj.transform.localScale = Scale;

                Vector3 Ro = new Vector3(float.Parse(obj_info[4]), float.Parse(obj_info[5]), float.Parse(obj_info[6]));
                obj.transform.eulerAngles = Ro;

                float Po_X = Filter_Data(obj_info[1], obj);
                float Po_Y = Filter_Data(obj_info[2], obj);
                //Vector3 Po = new Vector3(Mathf.Clamp(Po_X,-8.5f,8.5f), Po_Y, float.Parse(obj_info[3]));
                Vector3 Po = new Vector3(Po_X, Po_Y, float.Parse(obj_info[3]));
                obj.transform.position = Po;
                obj.name = obj_info[0].ToString();
            }
        }
        //reader.Close();
    }

    float Filter_Data(string data, GameObject obj)
    {
        string Val = data;

        string[] Character = new string[] { "W", "H", "SX" };
        foreach(string ch in Character){
            if (data.Contains(ch))
            { 
                switch(ch){
                    case "W":
                        Val = Val.Replace(ch, Screen_W.ToString());
                        break;
                    case "H":
                        Val = Val.Replace(ch, Screen_H.ToString());
                        break;
                    case "SX":
                        Val = Val.Replace(ch, obj.GetComponent<SpriteRenderer>().bounds.size.x.ToString());
                        break;
                    default:
                        print("Got Character is == " + ch);
                        break;
                }
            } 
        }

        if (!Val.Equals(data))
        {
            return Evaluate(Val);
        }
        else
        {
            return float.Parse(Val);
        }
    }

    float Evaluate(string expression) 
    {
        var loDataTable = new DataTable(); 
        var loDataColumn = new DataColumn("Eval", typeof (float), expression); 
        loDataTable.Columns.Add(loDataColumn); 
        loDataTable.Rows.Add(0); 
        return (float) (loDataTable.Rows[0]["Eval"]); 
    }


    public void Show_Hint(int LevelNo)
    {
        TextAsset Data = Resources.Load("Level_" + LevelNo) as TextAsset;
        string text = Data.text;
        string[] text_Array = text.Split('\n');

        for (int i = 1; i < text_Array.Length - 1; i++)
        {
            string[] obj_info = text_Array[i].Split(' ');
            if (Hints.Find(obj => obj.name == obj_info[0].ToString()))
            {
                string name = obj_info[0].Replace("_", "");
                GameObject obj = Instantiate(Hints[int.Parse(name) - 1]);

                float Sc_X = Filter_Data(obj_info[7], obj);
                float Sc_Y = Filter_Data(obj_info[8], obj);
                float Sc_Z = Filter_Data(obj_info[9], obj);
                Vector3 Scale = new Vector3(Sc_X, Sc_Y, Sc_Z);
                obj.transform.localScale = Scale;

                Vector3 Ro = new Vector3(float.Parse(obj_info[4]), float.Parse(obj_info[5]), float.Parse(obj_info[6]));
                obj.transform.eulerAngles = Ro;

                float Po_X = Filter_Data(obj_info[1], obj);
                float Po_Y = Filter_Data(obj_info[2], obj);
                Vector3 Po = new Vector3(Po_X, Po_Y, float.Parse(obj_info[3]));
                obj.transform.position = Po;
                obj.name = obj_info[0].ToString();
            }
        }
    }

}
