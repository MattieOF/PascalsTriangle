using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PascalsTriangle : MonoBehaviour
{
    [Header("Scene References")]
    public TMP_InputField layerInput;
    public TextMeshProUGUI highestValueText;
    public FadingText benchmarkText;
    public GameObject canvas;

    [Header("Asset References")]
    public GameObject numberPrefab;

    [Header("Properties")]
    public bool benchmark = true;

    private List<GameObject> objects;
    private double val = 1;
    private double highestVal = 0;
    private System.Diagnostics.Stopwatch sw;

    void Start()
    {
        sw = new System.Diagnostics.Stopwatch();
        objects = new List<GameObject>();
        Clear();
    }

    public void Create()
    {
        if (benchmark) sw.Restart();

        Clear();
        int layers = Convert.ToInt32(layerInput.text);
        val = 1;
        highestVal = 0;

        for (int i = 0; i < layers; i++)
        {
            for (int j = 0; j <= i; j++)
            {
                if (j == 0 || i == 0)
                    val = 1;
                else
                    val = val * (i - j + 1) / j;

                if (val > highestVal) highestVal = val;
                CreateNumber(val, i, j);
            }
        }

        if (benchmark)
        {
            sw.Stop();
            benchmarkText.ShowToast("Generation took " + sw.ElapsedMilliseconds + "ms (" + sw.ElapsedMilliseconds / 1000 + "s)", 3);
        }

        highestValueText.text = "Highest value: " + highestVal.ToString();
    }

    public void CreateNumber(double value, int row, int column)
    {
        GameObject go = Instantiate(numberPrefab, canvas.transform);
        go.transform.position = GetNumberPos(row, column);
        Debug.Log(go.transform.position);
        go.GetComponent<TextMeshProUGUI>().text = value.ToString();
        go.name = value.ToString() + " - " + new Vector2(row, column).ToString();
        objects.Add(go);
    }

    public Vector3 GetNumberPos(int row, int column)
    {
        Vector3 pos = new Vector3();
        pos.y = row * -500;
        if (row == 0)
        {
            pos.x = 0;
            return pos;
        }

        int totalDiff = row * 500;
        int leftPos = 0 - (totalDiff / 2);
        pos.x = leftPos + (column * 500);

        Debug.Log("Set pos to " + pos + " from " + new Vector2(row, column).ToString());

        return pos;
    }

    public void Clear()
    {
        foreach (GameObject go in objects) Destroy(go);
    }
}
