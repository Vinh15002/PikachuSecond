
using System.Collections.Generic;
using Event;
using UnityEngine;
using Random = UnityEngine.Random;

public class MatrixBuilder : MonoBehaviour
{
    public int row = 10;
    public int col = 18;

    public GameObject[] dataItem;

    
    public int[,] matrix;
    
    Dictionary<int, List<GameObject>> datas = new  Dictionary<int, List<GameObject>>();
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
                
                item.name = dataItem[matrix[i,j]].name;
                item.transform.localPosition = new Vector3(j, i, 0);

                if (!datas.ContainsKey(matrix[i, j]))
                {
                    datas.Add(matrix[i, j], new List<GameObject>());
                    datas[matrix[i, j]].Add(item);
                }
                else
                {
                    datas[matrix[i, j]].Add(item);
                }
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

    public void SetNewMatrix(int[,] matrix)
    {
        //Set Disactive All Item
        foreach (var data in datas)
        {
            foreach(var item in data.Value)
            {
                item.GetComponent<Item>().TurnOffPOutLine();
                item.SetActive(false);
            }
        }
        
        
        for (int i = 1; i < matrix.GetLength(0) - 1; i++)
        {
            for (int j = 1; j < matrix.GetLength(1) - 1; j++)
            {
                if (matrix[i, j] != -1)
                {
                    int index = 0;
                    List<GameObject> items = datas[matrix[i, j]];
                    
                        
                    while (items[index].activeSelf != false) index++;
                    GameObject item = items[index];
                    item.GetComponent<Item>().SetIndex(matrix[i,j]);
                    item.GetComponent<Item>().SetPair(i, j);
                
                   
                    item.transform.localPosition = new Vector3(j-1, i-1, 0);
                    item.SetActive(true);
                    
                    
                }
            }
        }
        
    }

    public Item GetItem(PairIndex pairIndex, int index)
    {
        List<GameObject> items = datas[index];
        foreach (var item in items)
        {
            if (item.transform.localPosition == new Vector3(pairIndex.Second - 1, pairIndex.First - 1, 0))
            {
                return item.GetComponent<Item>();
            }
        }
        return null;
    }
}
