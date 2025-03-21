using System;
using System.Collections;
using System.Collections.Generic;
using Event;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class MatrixBuilder : MonoBehaviour
{
    public int row = 10;
    public int col = 18;

    public GameObject[] dataItem;


    public int[,] matrix;
    private void Start()
    {
        GetData();
        GenerateMatrix();
        GenerateItem();
    }

    private void GenerateItem()
    {
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < col; j++)
            {
                GameObject item =  Instantiate(dataItem[matrix[i,j]], this.transform);
                item.GetComponent<Item>().SetIndex(matrix[i,j]);
                item.GetComponent<Item>().SetPair(i+1, j+1);
                item.transform.localPosition = new Vector3(j, i, 0);
            }
        }
    }


    private void GetData()
    {
       dataItem = Resources.LoadAll<GameObject>("Prefab" );
       matrix = new int[row, col];

    }
    private void GenerateMatrix()
    {
        List<int> indexItem = new List<int>();
        for (int i = 0; i <= row * col-1; i++)
        {
            indexItem.Add(i%18);
        }

        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < col; j++)
            {
                int index = Random.Range(0, indexItem.Count);
                matrix[i, j] = indexItem[index];
                indexItem.RemoveAt(index);
            }
        }
        BoardEvent.GetData?.Invoke(matrix);
    }
}
