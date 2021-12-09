using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System;
//using System.Linq;


public class Level_Editor : EditorWindow {
    
    public int Level_No = 1;
    List<GameObject> Objects = new List<GameObject>();
    //List<GameObject> Hints = new List<GameObject>();

    [MenuItem("Window/Level-Editor")]

    static void OpenEditor()
    {
        EditorWindow.GetWindow(typeof(Level_Editor));
        //GetWindow<Level_Editor>(false, "Level Editor", true);
    }

    private void OnFocus()
    {
        if (FindObjectOfType<Level_Generater>() != null)
        {
            Debug.Log("Focus=======================");
            Level_Generater levelSelect = FindObjectOfType<Level_Generater>();
            foreach (GameObject obj in levelSelect.Objects)
            {
                Objects.Add(obj);
            }
            //foreach (GameObject obj in levelSelect.Hints)
            //{
            //    Hints.Add(obj);
            //}
        }
        else{
            Debug.Log("Level_Generater Not Founded For Objects List.");
        }
    }
    private void OnGUI()
    {
        GUILayout.Label("Level Settings", EditorStyles.boldLabel);
        GUILayout.Space(20);

        GUILayout.BeginHorizontal();

        GUILayout.Space(15);
        string asd = GUILayout.TextField(" " + Level_No, GUILayout.Width(80), GUILayout.Height(18));
        Level_No = int.Parse(asd);
        GUILayout.Space(20);
        if (GUILayout.Button("<<", GUILayout.Width(30), GUILayout.Height(20)))
        {
            if (Level_No > 1)
            {
                Level_No -= 1;
            }
        }
        GUILayout.Space(15);
        if (GUILayout.Button(">>", GUILayout.Width(30), GUILayout.Height(20)))
        {
            Level_No += 1;
        }
        GUILayout.Space(15);
        if (GUILayout.Button("Clear Level", GUILayout.Width(80), GUILayout.Height(30)))
        {
            if (!Application.isPlaying)
            {
                Clear_Level();
            }
        }

        GUILayout.EndHorizontal();
        GUILayout.Space(30);
        GUILayout.BeginHorizontal();

        GUILayout.Space(20);

        if (GUILayout.Button("Show Level", GUILayout.Width(80), GUILayout.Height(30)))
        {
            if(!Application.isPlaying)
            {
                Show_Level(Level_No, false);
            }
        }

        GUILayout.Space(15);

        if (GUILayout.Button("Save Level", GUILayout.Width(80), GUILayout.Height(30)))
        {
            string path = "Assets/Resources/Level_" + (Level_No) + ".txt";
            if (!Application.isPlaying)
            {
                Save_Add_Level(path, (Level_No));
            }
        }

        GUILayout.Space(15);

        if (GUILayout.Button("New Level", GUILayout.Width(80), GUILayout.Height(30)))
        {
            string subString = "1";
            int Total_File = 0;
            DirectoryInfo dir = new DirectoryInfo(Application.dataPath + "/Resources");
            FileInfo[] info = dir.GetFiles();
            int[] Index = new int[info.Length];
            foreach (FileInfo f in info)
            {
                if(f.Extension.Contains("txt") && f.Name.Contains("Level_")){
                    subString = f.Name.Substring(6,f.Name.Length-10);
                    int a = int.Parse(subString);
                    Index[a - 1] = a;
                }
            }
            for (int i = 0; i < Index.Length; i++)
            {
                if (Index[i] != i + 1){
                    Total_File = i+1;
                    break;
                }
            }
            Debug.Log("New File Number == " + Total_File);
            string path = "Assets/Resources/Level_" + (Total_File) + ".txt";
            if (!Application.isPlaying)
            {
                Save_Add_Level(path, (Total_File));
            }
        }

        GUILayout.EndHorizontal();

        GUILayout.Space(15);

        GUILayout.BeginHorizontal();

        GUILayout.Space(20);

        if (GUILayout.Button("Hint", GUILayout.Width(280), GUILayout.Height(30)))
        {
            if (!Application.isPlaying)
            {
                Show_Level(Level_No, true);
            }
        }

        GUILayout.EndHorizontal();
    }

    void Clear_Level()
    {
        if (FindObjectOfType<Level_Generater>() != null)
        {
            GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
            foreach (GameObject go in allObjects)
            {
                if (go.transform.parent == null && go.activeInHierarchy && go.name.Contains("("))
                {
                    string[] name = go.name.Split('(');
                    string ch = name[0].Trim();
                    if(Objects.Find(obj => obj.name == ch))
                    {
                        DestroyImmediate(go);
                    }
                }
                else if (go.transform.parent == null && go.activeInHierarchy && Objects.Find(obj => obj.name == go.name))
                {
                    DestroyImmediate(go);
                }
                //else if (go.transform.parent == null && go.activeInHierarchy && Hints.Find(obj => obj.name == go.name))
                //{
                //    DestroyImmediate(go);
                //}
            }
            Debug.Log("Clear All.......");
        }
        else
        {
            Debug.Log("Level_Generater Not Founded For Objects List.");
        }
    }

    void Show_Level(int LevelNo, bool hint)
    {
        try
        {
            if (LevelNo > 0 && FindObjectOfType<Level_Generater>() != null && File.Exists(Application.dataPath + "/Resources/Level_" +LevelNo +".txt"))
            {
                Debug.Log("Level No == " + LevelNo);
                Level_Generater levelSelect = FindObjectOfType<Level_Generater>();
                if(hint)
                {
                    //levelSelect.Show_Hint(LevelNo);
                }
                else
                {
                    Clear_Level();
                    levelSelect.Read_Level_File(LevelNo);
                }

            }
            else
            {
                Debug.Log("Level_Generater Not Founded For Objects List OR Level_No is Incorrect.");
            }
        }
        catch (Exception)
        {
            throw;
        }
    }


    void Save_Add_Level(String path, int FileNo)
    {
        StreamWriter writer = new StreamWriter(path, false);

        //GameObject Boy = GameObject.Find("Boy");
        //GameObject Girl = GameObject.Find("Girl");
        GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();

        writer.WriteLine("{0}", FileNo);
        //writer.WriteLine("Boy {0} {1} {2} {3} {4} {5}",Boy.transform.position.x,Boy.transform.position.y,Boy.transform.position.z, Boy.transform.eulerAngles.x, Boy.transform.eulerAngles.y, Boy.transform.eulerAngles.z);
        //writer.WriteLine("Girl {0} {1} {2} {3} {4} {5}", Girl.transform.position.x, Girl.transform.position.y, Girl.transform.position.z, Girl.transform.eulerAngles.x, Girl.transform.eulerAngles.y, Girl.transform.eulerAngles.z);

        foreach (GameObject go in allObjects){
            if (go.transform.parent == null && go.activeInHierarchy && go.name.Contains("("))
            {
                string[] name = go.name.Split('(');
                string ch = name[0].Trim();
                if (Objects.Find(obj => obj.name == ch))
                {
                    Vector3 po = go.transform.position;
                    Vector3 ro = go.transform.rotation.eulerAngles;
                    Vector3 scale = go.transform.localScale;
                    writer.WriteLine("{0} {1} {2} {3} {4} {5} {6} {7} {8} {9}", ch, po.x, po.y, po.z, ro.x, ro.y, ro.z, scale.x, scale.y, scale.z);
                }
            }
            else if (go.transform.parent == null && go.activeInHierarchy && Objects.Find(obj => obj.name == go.name))
            {
                Vector3 po = go.transform.position;
                Vector3 ro = go.transform.rotation.eulerAngles;
                Vector3 scale = go.transform.localScale;
                writer.WriteLine("{0} {1} {2} {3} {4} {5} {6} {7} {8} {9}", go.name, po.x, po.y, po.z, ro.x, ro.y, ro.z, scale.x, scale.y, scale.z);
            }
            //else if (go.transform.parent == null && go.activeInHierarchy && Hints.Find(obj => obj.name == go.name))
            //{
            //    Vector3 po = go.transform.position;
            //    Vector3 ro = go.transform.rotation.eulerAngles;
            //    Vector3 scale = go.transform.localScale;
            //    writer.WriteLine("{0} {1} {2} {3} {4} {5} {6} {7} {8} {9}", go.name, po.x, po.y, po.z, ro.x, ro.y, ro.z, scale.x, scale.y, scale.z);
            //}
        }

        writer.Close();
        //Re-import the file to update the reference in the editor
        AssetDatabase.ImportAsset(path);

        TextAsset textAsset = Resources.Load("Level_" + FileNo) as TextAsset;
        Debug.Log(textAsset.text);
        //string text = File.ReadAllText(Application.dataPath + "/Resources/Level_" + (Total_Level + 1) + ".txt");
        //Debug.Log(text);
    }

}
