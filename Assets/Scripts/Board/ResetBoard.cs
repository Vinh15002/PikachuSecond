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



        
    }
}