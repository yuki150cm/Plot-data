using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using UnityEngine.UI; 
using TMPro;

public class JsonData
{
    public RRdataInfo[] RRdata2023;
}

[System.Serializable]
public class RRdataInfo
{
    public string sid;
    public string condition;
    public float X;
    public float Y;
    public float Z;
    public int idx;
}

[System.Serializable]
public class Select_JSON_data
{
    public string path;
    public string[] sid;
    public string[] condition;
}

public class Plot_data : MonoBehaviour
{
    //[SerializeField] TextMeshProUGUI textObject;

    //プレハブを生成する
    public GameObject sphere;
    public GameObject cube;
    public GameObject capsule;
    public GameObject cylinder;


    public List<Vector3> getMeanResult(List<List<Vector3>> vec)
    {
        int maxLength = 0;
        List<Vector3> meanVec = new List<Vector3>();
        List<int> countAddVec = new List<int>();

        for(int i=0; i<vec.Count; i++){
            if(maxLength < vec[i].Count){
                //リストi番目の要素の個数を数える
                maxLength = vec[i].Count;
            }
        }

        for(int i=0; i<maxLength; i++){
            meanVec.Add(new Vector3(0,0,0));
            countAddVec.Add(0);
        }

        for(int i=0; i<vec.Count; i++){
            for(int j=0; j<vec[i].Count; j++){
                meanVec[j] += vec[i][j];
                countAddVec[j] += 1;
            }
        }

        for(int i=0; i<maxLength; i++){
            meanVec[i] /= countAddVec[i];
        }
        return meanVec;
    }

    // Start is called before the first frame update
    void Start()
    {
        //ファイルパスを外部からの入力で入るようにする
     
        string selectjson = File.ReadAllText("/Users/ohuchiyuki/UoA/4year/Plot_data_var2/Assets/Resources/JSON/Select_data.json");
        var select = JsonUtility.FromJson<Select_JSON_data>(selectjson);
        string loadjson = File.ReadAllText(select.path);

        JsonData jsonData = new JsonData();
        JsonUtility.FromJsonOverwrite(loadjson, jsonData);

        //s1~s29
        //SidとConditionに外部入力データを入れられるようにする
        string[] Sid = select.sid;
        //Sidの数を数える
        int cnt = Sid.Length;      

        string[] Condition = select.condition;
        //conditionの数を数える
        int cnt_c = Condition.Length;

        List<List<Vector3>> vec1 = new List<List<Vector3>>();
        List<List<Vector3>> vec2 = new List<List<Vector3>>();
        List<List<Vector3>> vec3 = new List<List<Vector3>>();
        List<List<Vector3>> vec4 = new List<List<Vector3>>();


        for(int i=0; i<cnt; i++){
            vec1.Add(new List<Vector3>());
            vec2.Add(new List<Vector3>());
            vec3.Add(new List<Vector3>());
            vec4.Add(new List<Vector3>());
        }
      
        for(int j=0; j<cnt; j++){
            for(int k=0; k<cnt_c; k++){

                int i = 0;
                int length = 0;

                foreach(var item in jsonData.RRdata2023){
                    if(Sid[j].Equals(item.sid) && Condition[k].Equals(item.condition)){
                        length++;
                    }
                }

                //データの軌道を出す
                foreach (var item in jsonData.RRdata2023)
                {
                    /*Jsonファイルの読み込み確認デバッグ
                    Debug.Log("sID: " + item.sid);
                    Debug.Log("condition: " + item.condition);
                    Debug.Log("X-coordinate: " + item.X);
                    Debug.Log("Y-coordinate: " + item.Y);
                    Debug.Log("Z-coordinate: " + item.Z);
                    Debug.Log("idx: " + item.idx);*/
                    

                    if(Sid[j].Equals(item.sid) && Condition[k].Equals(item.condition)){
                        //objにインスタンス化したsphereを入れる
                        GameObject obj_sphere = Instantiate(sphere);
                        GameObject obj_cube = Instantiate(cube);
                        GameObject obj_cap = Instantiate(capsule);
                        GameObject obj_cyl = Instantiate(cylinder);

                            //conditonの入力数によって出力する形を変える
                        if(k==0){
                            vec1[j].Add(new Vector3(item.X,item.Y,item.Z));
                        }
                        if(k==1){
                            vec2[j].Add(new Vector3(item.X,item.Y,item.Z));
                        }
                        if(k==2){
                            vec3[j].Add(new Vector3(item.X,item.Y,item.Z));
                        }
                        if(k==3){
                            vec4[j].Add(new Vector3(item.X,item.Y,item.Z));
                        }

                        if(cnt <=5){
                        
                            if(k==0){
                                //objの位置を入れる
                                obj_sphere.transform.position = new Vector3(item.X,item.Y,item.Z);
                                //objの色をグラデーションに変える
                                if(j==0){
                                //red
                                    obj_sphere.GetComponent<Renderer>().material.color = new Color(1, 0, 0, 1-((float)i/length));
                                    i++;
                                }
                                if(j==1){
                                //blue
                                    obj_sphere.GetComponent<Renderer>().material.color = new Color(0, 0, 1, 1-(float)i/length);
                                    i++;
                                }
                                if(j==2){
                                //yellow
                                    obj_sphere.GetComponent<Renderer>().material.color = new Color(1, 1, 0, 1-(float)i/length);
                                    i++;
                                }
                                if(j==3){
                                    //green
                                    obj_sphere.GetComponent<Renderer>().material.color = new Color(0, 0.5f, 0, 1-(float)i/length);
                                    i++;
                                }
                                if(j==4){
                                //pink
                                    obj_sphere.GetComponent<Renderer>().material.color = new Color(1, 0.078f, 0.577f, 1-(float)i/length);
                                    i++;
                                }
                            }
                        
                            if(k==1){
                                //objの位置を入れる
                                obj_cube.transform.position = new Vector3(item.X,item.Y,item.Z);
                                //objの色をグラデーションに変える
                                if(j==0){
                                //red
                                    obj_cube.GetComponent<Renderer>().material.color = new Color(1, 0, 0, 1-((float)i/length));
                                    i++;
                                }
                                if(j==1){
                                    //blue
                                    obj_cube.GetComponent<Renderer>().material.color = new Color(0, 0, 1, 1-(float)i/length);
                                    i++;
                                }
                                if(j==2){
                                    //yellow
                                    obj_cube.GetComponent<Renderer>().material.color = new Color(1, 1, 0, 1-(float)i/length);
                                    i++;
                                }
                                if(j==3){
                                    //green
                                    obj_cube.GetComponent<Renderer>().material.color = new Color(0, 0.5f, 0, 1-(float)i/length);
                                    i++;
                                }
                                if(j==4){
                                    //pink
                                    obj_cube.GetComponent<Renderer>().material.color = new Color(1, 0.078f, 0.577f, 1-(float)i/length);
                                    i++;
                                }
                                    
                            }

                            if(k==2){
                                //objの位置を入れる
                                obj_cap.transform.position = new Vector3(item.X,item.Y,item.Z);
                                //objの色をグラデーションに変える
                                if(j==0){
                                //red
                                    obj_cap.GetComponent<Renderer>().material.color = new Color(1, 0, 0, 1-((float)i/length));
                                    i++;
                                }
                                if(j==1){
                                    //blue
                                    obj_cap.GetComponent<Renderer>().material.color = new Color(0, 0, 1, 1-(float)i/length);
                                    i++;
                                }
                                if(j==2){
                                    //yellow
                                    obj_cap.GetComponent<Renderer>().material.color = new Color(1, 1, 0, 1-(float)i/length);
                                    i++;
                                }
                                if(j==3){
                                    //green
                                    obj_cap.GetComponent<Renderer>().material.color = new Color(0, 0.5f, 0, 1-(float)i/length);
                                    i++;
                                }
                                if(j==4){
                                    //pink
                                    obj_cap.GetComponent<Renderer>().material.color = new Color(1, 0.078f, 0.577f, 1-(float)i/length);
                                    i++;
                                }
                            }
                        
                            if(k==3){
                                //objの位置を入れる
                                obj_cyl.transform.position = new Vector3(item.X,item.Y,item.Z);
                                //objの色をグラデーションに変える
                                if(j==0){
                                //red
                                    obj_cyl.GetComponent<Renderer>().material.color = new Color(1, 0, 0, 1-((float)i/length));
                                    i++;
                                }
                                if(j==1){
                                    //blue
                                    obj_cyl.GetComponent<Renderer>().material.color = new Color(0, 0, 1, 1-(float)i/length);
                                    i++;
                                }
                                if(j==2){
                                    //yellow
                                    obj_cyl.GetComponent<Renderer>().material.color = new Color(1, 1, 0, 1-(float)i/length);
                                    i++;
                                }
                                if(j==3){
                                    //green
                                    obj_cyl.GetComponent<Renderer>().material.color = new Color(0, 0.5f, 0, 1-(float)i/length);
                                    i++;
                                }
                                if(j==4){
                                    //pink
                                    obj_cyl.GetComponent<Renderer>().material.color = new Color(1, 0.078f, 0.577f, 1-(float)i/length);
                                    i++;
                                }
                                    
                            }
                        }
                        
                    }

                }
            }
        }

        if(cnt > 5){
            
            List<Vector3> meanVecPoint1 = new List<Vector3>();
            List<Vector3> meanVecPoint2 = new List<Vector3>();
            List<Vector3> meanVecPoint3 = new List<Vector3>();
            List<Vector3> meanVecPoint4 = new List<Vector3>();

            meanVecPoint1 = getMeanResult(vec1);
            meanVecPoint2 = getMeanResult(vec2);
            meanVecPoint3 = getMeanResult(vec3);
            meanVecPoint4 = getMeanResult(vec4);

            for(int i=0; i<meanVecPoint1.Count; i++){
                GameObject obj_sphere = Instantiate(sphere);
                obj_sphere.transform.position = meanVecPoint1[i];
                obj_sphere.GetComponent<Renderer>().material.color = new Color(0, 0, 0, 1-(float)i/meanVecPoint1.Count);
            }

            for(int i=0; i<meanVecPoint2.Count; i++){
                GameObject obj_cube = Instantiate(cube);
                obj_cube.transform.position = meanVecPoint2[i];
                obj_cube.GetComponent<Renderer>().material.color = new Color(0, 0, 0, 1-(float)i/meanVecPoint2.Count);
            }

            for(int i=0; i<meanVecPoint3.Count; i++){
                GameObject obj_cap = Instantiate(capsule);
                obj_cap.transform.position = meanVecPoint3[i];
                obj_cap.GetComponent<Renderer>().material.color = new Color(0, 0, 0, 1-(float)i/meanVecPoint3.Count);
            }

            for(int i=0; i<meanVecPoint4.Count; i++){
                GameObject obj_cyl = Instantiate(cylinder);
                obj_cyl.transform.position = meanVecPoint4[i];
                obj_cyl.GetComponent<Renderer>().material.color = new Color(0, 0, 0, 1-(float)i/meanVecPoint4.Count);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
