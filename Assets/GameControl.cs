using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameControl : MonoBehaviour {
    Dictionary<string, float> metricRecord = new Dictionary<string, float>() {
    {"inch",25400000},
    {"foot",304800000},
    {"fanthom",1829000000},
    {"chain",20120000000},
    {"furlong",201200000000},
    {"mile",1609000000000}};

    List<string> metricNameList = new List<string> { "inch", "foot", "fanthom", "chain", "furlong", "mile" };

    public int scale;
    public int wrongScale;
    public double linePercent;

    public GameObject Nano;

	// Use this for initialization
	void Start () {
        SetChallenge();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void SetChallenge() {
        scale = (int)(Random.value * 7)+2;

        string widthUnit = metricNameList[(int)(Random.value * 6)];
        string heightUnit = metricNameList[(int)(Random.value * 6)];
        float widthNumber = (10/scale) / metricRecord[widthUnit];
        float heightNumber = (10/scale) / metricRecord[heightUnit];
        GameObject.Find("HeightText").GetComponent<Text>().text = heightNumber + " " + heightUnit;
        GameObject.Find("WidthText").GetComponent<Text>().text = widthNumber + " " + widthUnit;

        wrongScale = (int)(Random.value * 7)+2;
        while (scale == wrongScale){
            wrongScale = (int)(Random.value * 7)+2;
        }

        int correctChoice = (int) (Random.value * 2 - 1);
        if (correctChoice == 1)
        {
            GameObject.Find("Choice01Text").GetComponent<Text>().text = scale.ToString();
            GameObject.Find("Choice02Text").GetComponent<Text>().text = wrongScale.ToString();
        }
        else {
            GameObject.Find("Choice01Text").GetComponent<Text>().text = wrongScale.ToString();
            GameObject.Find("Choice02Text").GetComponent<Text>().text = scale.ToString();
        }

        linePercent = scale * scale / 100f;

    }

    public void ButtonOneAction() {
        ProduceBrick(GameObject.Find("Choice01Text").GetComponent<Text>().text);
        // should disable both button
    }

    public void ButtonTwoAction() {
        ProduceBrick(GameObject.Find("Choice02Text").GetComponent<Text>().text);
        // should disable both button
    }

    void ProduceBrick(string stringScale) {
        int compareScale = System.Convert.ToInt32(stringScale);
        if (compareScale == scale)
        {
            GameObject.Find("DisplayResult").GetComponent<Text>().text = "YES";
        }
        else {
            GameObject.Find("DisplayResult").GetComponent<Text>().text = "NO";
        }

        float chosenScale = System.Convert.ToInt32(stringScale);
        chosenScale *= 0.028f;
        Debug.Log(chosenScale);

        for (int i = 0; i < 100; i++)
        {
            GameObject clone = Instantiate(Nano) as GameObject;
            clone.transform.localScale = Vector3.one * chosenScale;
            clone.tag = "nanos";
        }
        GameObject theLine =  GameObject.Find("targetLine");
        theLine.transform.position = new Vector3(theLine.transform.position.x,-1.5f + (float)linePercent * 3f, theLine.transform.position.z);
    }

    public void startNewGame() {
        SetChallenge();
        GameObject theLine = GameObject.Find("targetLine");
        theLine.transform.position = new Vector3(0,1.5f,-1);

        GameObject[] Nanos;
        Nanos = GameObject.FindGameObjectsWithTag("nanos");

        for (var i = 0; i < Nanos.Length; i++)
        {
            Destroy(Nanos[i]);
        }

        GameObject.Find("DisplayResult").GetComponent<Text>().text = "One hundred nanos will fall down\nwith size X ? \n make your choice?";
    }
}
