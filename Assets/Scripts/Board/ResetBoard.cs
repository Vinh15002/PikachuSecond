using System.Collections.Generic;
using UnityEngine;

namespace Board
{
    public class ResetBoard
    {
        


        public static int[,] GetRandomNewMatrix(int[,] originalMatrix)
        {
           
            bool[,] checkMatrix = new  bool[originalMatrix.GetLength(0),  originalMatrix.GetLength(1)];
            List<int> containtItems = new List<int>();
            for (int i = 0; i < originalMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < originalMatrix.GetLength(1); j++)
                {
                    if (originalMatrix[i, j] == -1)
                    {
                        checkMatrix[i, j] = false;
                    }
                    else
                    {
                        containtItems.Add(originalMatrix[i, j]);
                        checkMatrix[i, j] = true;
                    }
                }
            }
            int[,] result = new  int[originalMatrix.GetLength(0), originalMatrix.GetLength(1)];
            for (int i = 0; i < checkMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < checkMatrix.GetLength(1); j++)
                {
                    if(!checkMatrix[i,j]) result[i, j] = -1;
                    else
                    {
                        int valueRandom = Random.Range(0, containtItems.Count);
                        result[i, j] = containtItems[valueRandom];
                        containtItems.RemoveAt(valueRandom);
                    }
                }
            }

            return result;
        }

        public static int[,] GetType2Matrix(PairIndex item1, PairIndex item2, int[,] originalMatrix)
        {
            if (item1.First < item2.First)
            {
                PairIndex tmp = new PairIndex(item1.First, item1.Second);
                item1 = item2;
                item2 = tmp;
            }
            int row = originalMatrix.GetLength(0);
            for (int i = item1.First; i < row -1 ; i++)
            {
                originalMatrix[i, item1.Second] = originalMatrix[i+1, item1.Second];
            }
         
            for (int i = item2.First; i < row -1 ; i++)
            {
                originalMatrix[i, item2.Second] = originalMatrix[i+1, item2.Second];
            }
            
            
            return originalMatrix;
        }
        
        public static int[,] GetType3Matrix(PairIndex item1, PairIndex item2, int[,] originalMatrix)
        {
            
            
            if (item1.Second < item2.Second)
            {
                PairIndex tmp = new PairIndex(item1.First, item1.Second);
                item1 = item2;
                item2 = tmp;
            }
           
            int col =  originalMatrix.GetLength(1);
            for (int i = item1.Second; i < col -1; i++)
            {
                originalMatrix[item1.First, i] = originalMatrix[item1.First, i+1];
            }
            
            for (int i = item2.Second; i < col- 1; i++)
            {
                originalMatrix[item2.First, i] = originalMatrix[item2.First, i+1];
            }
            
            
            return originalMatrix;
        }

        
    }
}