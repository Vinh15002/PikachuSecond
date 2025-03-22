using System;
using System.Collections;
using System.Collections.Generic;
using Board;
using Event;

using UnityEngine;



public enum TypeGame
{
    Normal, 
    Special01,
    Special02,
}

public class BoardController : MonoBehaviour
{
    
    [SerializeField] private TypeGame typeGame = TypeGame.Normal;
    
    [SerializeField] private MatrixBuilder matrixBuilder;
    private int[,] matrix = new int[3, 3];
    
    public int[,] Matrix => matrix;
    
    
    private Item firstItem;
    private Item secondItem;

    // [SerializeField]private LineSearching lineSearching;
    [SerializeField]private LineRenderer line;
    
    public int Counter => counter;
    private int counter;
    private void Update()
    {
        ChooseItem();
    }

    private void ChooseItem()
    {
        
        if (Input.GetMouseButton(0))
        {
            
            var hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
           
            if (hit.collider != null)
            {
                Debug.Log(hit.collider.gameObject.GetComponent<Item>().Index);
                if (firstItem == null)
                {
                    firstItem = hit.collider.gameObject.GetComponent<Item>();
                    firstItem.TurnOnPOutLine();
                }
                else
                {
                    
                    if(firstItem ==  hit.collider.gameObject.GetComponent<Item>()) return;
                    
                    if (secondItem == null)
                    {
                        secondItem = hit.collider.gameObject.GetComponent<Item>();
                        secondItem.TurnOnPOutLine();
                       
                        if (firstItem.Index != secondItem.Index)
                        {
                            firstItem.TurnOffPOutLine();
                            secondItem.TurnOffPOutLine();
                            firstItem = null;
                            secondItem = null;
                            return;
                            
                        }
                        
                        
                        if (LineSearching.CheckConnectItem(firstItem.PairIndex, secondItem.PairIndex, matrix))
                        {
                            //Show Line 
                            ShowLine(LineSearching.tracking);
                            StartCoroutine(DelayDisactive());

                           


                        }
                        else
                        {
                            firstItem.TurnOffPOutLine();
                            secondItem.TurnOffPOutLine();
                            firstItem = null;
                            secondItem = null;
                           
                           
                        }
                        
                        //Check Conection
                    }
                }
            }
        }
    }

    private IEnumerator DelayDisactive()
    {
        yield return new WaitForSeconds(0.5f);
        firstItem.transform.gameObject.SetActive(false);
        secondItem.transform.gameObject.SetActive(false);
        //Get Pair Index

        PairIndex item1Pair = firstItem.PairIndex;
        PairIndex item2Pair = secondItem.PairIndex;
        matrix[firstItem.PairIndex.First, firstItem.PairIndex.Second] = -1;
        matrix[secondItem.PairIndex.First, secondItem.PairIndex.Second] = -1;
        firstItem = null;
        secondItem = null;
        line.enabled = false;
        counter-=2;
        if (typeGame == TypeGame.Special01)
        {
            matrix = ResetBoard.GetType2Matrix(item1Pair,  item2Pair, this.matrix);
            matrixBuilder.SetNewMatrix(matrix);
        }
        else if (typeGame == TypeGame.Special02)
        {
            matrix = ResetBoard.GetType3Matrix(item1Pair,  item2Pair, this.matrix);
            matrixBuilder.SetNewMatrix(matrix);
        }
    }

    private void ShowLine(List<PairIndex> lineSearchingTracking)
    {
        line.enabled = true;
        line.positionCount = lineSearchingTracking.Count;
        Vector3[] outlinePoints = new Vector3[lineSearchingTracking.Count];
        for (int i = 0; i < lineSearchingTracking.Count; i++)
        {
            outlinePoints[i] = new Vector3(lineSearchingTracking[i].Second ,  lineSearchingTracking[i].First , 0) + new Vector3(-9.5f, -5.5f, 0);
        }
        line.SetPositions(outlinePoints);

    }


    private void OnEnable()
    {
        BoardEvent.GetData += GetDataMatrix;
    }

    private void OnDisable()
    {
        BoardEvent.GetData -= GetDataMatrix;
    }

    private void GetDataMatrix(int[,] ints)
    {
        int rows = ints.GetLength(0);
        int cols = ints.GetLength(1);
        matrix = new int[rows + 2, cols + 2];
        for (int i = 0; i < cols + 2; i++) matrix[0, i] = -1;
        for (int i = 0; i < rows + 2; i++) matrix[i, 0] = -1;
        for (int i  = 0; i < rows + 2; i++) matrix[i, cols+1] = -1;
        for (int i  = 0; i < cols + 2; i++) matrix[rows+1, i] = -1;

        for (int i = 1; i <= rows; i++)
        {
            for (int j = 1; j <= cols; j++)
            {
                matrix[i, j] = ints[i - 1, j - 1];
            }
        }
        counter = rows * cols;
    }

    public void ResetMatrix()
    {
        matrix = ResetBoard.GetRandomNewMatrix(matrix);
        matrixBuilder.SetNewMatrix(matrix);
        
    }

    public void SuggestItem()
    {
        if(counter == 0 ) return;
        List<PairIndex> suggestion = Suggest.GetSuggest(matrix);
       
        while (suggestion.Count == 0)
        {
            ResetMatrix();
            suggestion = Suggest.GetSuggest(matrix);
        }
        firstItem = matrixBuilder.GetItem(suggestion[0], matrix[suggestion[0].First, suggestion[0].Second]);
        secondItem = matrixBuilder.GetItem(suggestion[1], matrix[suggestion[1].First, suggestion[1].Second]);
        firstItem.TurnOnPOutLine();
        secondItem.TurnOnPOutLine();
        ShowLine(LineSearching.tracking);
        StartCoroutine(DelayDisactive());

        
        
    }

    public void ResetNewBoard()
    {
        matrixBuilder.GenerateNewMatrix();
    }

    public void SetNewTypeGame(TypeGame special01)
    {
        if (special01 == TypeGame.Special01)
        {
            matrixBuilder.GenerateNewMatrix();
            this.typeGame = TypeGame.Special01; 
        }
        else if (special01 == TypeGame.Special02)
        {
            matrixBuilder.GenerateNewMatrix();
            this.typeGame = TypeGame.Special02;
        }
    }
}
