using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ENVmds : MonoBehaviour
{
    static float xOffset = -0.4f;
    static float yOffset = -1.9f;
    static float zOffset = 0.4f;
    private static List<float> Xcoord = new List<float>();
    private static List<float> Ycoord = new List<float>();
    private static List<float> Zcoord = new List<float>();
    private static List<string> clusterID = new List<string>();

    private static List<float> x2 = new List<float>();
    private static List<float> y2 = new List<float>();
    private static List<string> cluster2 = new List<string>();

    private static List<float> x3 = new List<float>();
    private static List<float> y3 = new List<float>();
    private static List<string> cluster3 = new List<string>();

    public static List<GameObject> allPTS = new List<GameObject>();


    public static void read3()
    {
        StringReader reader;
        string txt = "";
        TextAsset x = (TextAsset)Resources.Load("scatter3-x", typeof(TextAsset));
        reader = new StringReader(x.text);
        while ((txt = reader.ReadLine()) != null)
        {
            x3.Add(float.Parse(txt));
        }

        TextAsset y = (TextAsset)Resources.Load("scatter3-y", typeof(TextAsset));
        reader = new StringReader(y.text);
        while ((txt = reader.ReadLine()) != null)
        {
            y3.Add(float.Parse(txt));
        }
        TextAsset id = (TextAsset)Resources.Load("cluster3", typeof(TextAsset));
        reader = new StringReader(id.text);
        while ((txt = reader.ReadLine()) != null)
        {
            cluster3.Add(txt);
        }
        plot3();
    }
    public static void read2()
    {
        StringReader reader;
        string txt = "";
        TextAsset x = (TextAsset)Resources.Load("scatter2-x", typeof(TextAsset));
        reader = new StringReader(x.text);
        while ((txt = reader.ReadLine()) != null)
        {
            x2.Add(float.Parse(txt));
        }

        TextAsset y = (TextAsset)Resources.Load("scatter2-y", typeof(TextAsset));
        reader = new StringReader(y.text);
        while ((txt = reader.ReadLine()) != null)
        {
            y2.Add(float.Parse(txt));
        }
        TextAsset id = (TextAsset)Resources.Load("scatter2-cluster", typeof(TextAsset));
        reader = new StringReader(id.text);
        while ((txt = reader.ReadLine()) != null)
        {
            cluster2.Add(txt);
        }
        plot2();

    }
    public static void read1()
    {
        StringReader reader;
        string txt = "";
        TextAsset x = (TextAsset)Resources.Load("env_mdsX", typeof(TextAsset));
        reader = new StringReader(x.text);
        while ((txt = reader.ReadLine()) != null)
        {
            Xcoord.Add(float.Parse(txt));
        }

        TextAsset y = (TextAsset)Resources.Load("env_mdsY", typeof(TextAsset));
        reader = new StringReader(y.text);
        while ((txt = reader.ReadLine()) != null)
        {
            Ycoord.Add(float.Parse(txt));
        }

        TextAsset z = (TextAsset)Resources.Load("env_mdsZ", typeof(TextAsset));
        reader = new StringReader(z.text);
        while ((txt = reader.ReadLine()) != null)
        {
            Zcoord.Add(float.Parse(txt));
        }

        TextAsset id = (TextAsset)Resources.Load("env_mdsCluster", typeof(TextAsset));
        reader = new StringReader(id.text);
        while ((txt = reader.ReadLine()) != null)
        {
            clusterID.Add(txt);
        }
        //print(Xcoord.Count + " " + Ycoord.Count + " " + Zcoord.Count + " " + clusterID.Count);
        makePlot();
    }
    public static void makePlot()
    {
        float scale = 0.02f;
        for (int i = 0; i < Zcoord.Count; i++)
        {
            var dataPt = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            dataPt.transform.parent = Subspaces.focus2;
            dataPt.transform.localPosition = new Vector3((Xcoord[i] / 1.4f) + xOffset, (Zcoord[i] / 1.5f - 0.6f) + zOffset, (Ycoord[i] * -20) + yOffset);
            dataPt.transform.localRotation = Quaternion.identity;
            dataPt.transform.localScale = new Vector3(scale, scale, scale * 20); dataPt.name = i.ToString();
            Material newMaterial = new Material(Shader.Find("VertexLit"));
            newMaterial.color = findColor(clusterID[i]);
            dataPt.GetComponent<Renderer>().material = newMaterial;
            //dataPt.layer = LayerMask.NameToLayer("Ignore Raycast");
            dataPt.name = "2,3," + SmokeyController.logScatterCount.ToString();
            SmokeyController.logScatterCount++;
            dataPt.gameObject.SetActive(true);
            allPTS.Add(dataPt);
        }
    }

    public static void plot2()
    {
        float scale = 0.02f;
        for (int i = 0; i < x2.Count; i++)
        {
            var dataPt = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            dataPt.transform.parent = Subspaces.focus2;
            dataPt.transform.localPosition = new Vector3((x2[i] / 1.4f) + xOffset, (y2[i] / 1.5f - 0.6f) + zOffset, (y2[i])*Random.Range(-15,-2));
            dataPt.transform.localRotation = Quaternion.identity;
            dataPt.transform.localScale = new Vector3(scale, scale, 0.4f);
            Material newMaterial = new Material(Shader.Find("VertexLit"));
            newMaterial.color = findColor(cluster2[i]);
            dataPt.GetComponent<Renderer>().material = newMaterial;
            //dataPt.layer = LayerMask.NameToLayer("Ignore Raycast");
            dataPt.name = "2,3," + SmokeyController.logScatterCount.ToString();
            SmokeyController.logScatterCount++;
            dataPt.gameObject.SetActive(true);
            allPTS.Add(dataPt);
        }
    }

    public static void plot3()
    {
        float scale = 0.02f;
        for (int i = 0; i < x3.Count; i++)
        {
            var dataPt = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            dataPt.transform.parent = Subspaces.focus2;
            dataPt.transform.localPosition = new Vector3((x3[i] / 1.4f) + xOffset, (y3[i] / 1.5f - 0.6f) + zOffset, (y3[i]) * Random.Range(-15, -2));
            dataPt.transform.localRotation = Quaternion.identity;
            dataPt.transform.localScale = new Vector3(scale, scale, 0.4f);
            Material newMaterial = new Material(Shader.Find("VertexLit"));
            newMaterial.color = findColor(cluster3[i]);
            dataPt.GetComponent<Renderer>().material = newMaterial;
            //dataPt.layer = LayerMask.NameToLayer("Ignore Raycast");
            dataPt.name = "2,3," + SmokeyController.logScatterCount.ToString();
            SmokeyController.logScatterCount++;
            dataPt.gameObject.SetActive(true);
            allPTS.Add(dataPt);
        }
    }

    public static Color findColor(string color)
    {
        Color outt = Color.white;
        switch (color)
        {
            case "1":
                outt = Color.green;
                break;
            case "2":
                outt = Color.blue;
                break;
            case "3":
                outt = Color.red;
                break;
            case "4":
                outt = Color.yellow;
                break;
            case "5":
                outt = Color.cyan;
                break;
            case "6":
                outt = Color.magenta;
                break;
            case "7":
                outt = Color.gray;
                break;
            case "8":
                outt = new Color(1, 0.15f, 0);
                break;
            case "9":
                outt = new Color(1f, 0.2f, 0.45f);
                break;
        }
        return outt;
    }
    public static void hideScatter()
    {
        foreach (GameObject item in ENVmds.allPTS)
        {
            item.SetActive(false);
        }
    }
}
