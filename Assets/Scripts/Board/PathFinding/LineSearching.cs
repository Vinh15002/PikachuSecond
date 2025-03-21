using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LineSearching : MonoBehaviour
{
    
    public List<PairIndex> tracking  = new List<PairIndex>();
    
  

    public bool CheckConnectItem(PairIndex item1,  PairIndex item2, int[,] matrix)
    {
        
       
        tracking.Clear();
        //CheckOneline
        if (item1.First == item2.First || item1.Second == item2.Second)
        {
            //Check OneLine 
            if (CheckOneLine(item1, item2, matrix))
            {
                tracking.Add(item1);
                tracking.Add(item2);
                return true;
            };
            if (CheckThreeLinesSameDirection(item1, item2, matrix)) return true;
            //Check Three Line Same X or Y 
            return false;

        }
        //CheckTwoLine
        if (CheckTwoLines(item1, item2, matrix))
        {
            return true;
        }
        //Check ThreeLine 
        if (CheckThreeLines(item1, item2, matrix))
        {
            return true;
        }
        
        
        
        return false;
    }

    public bool CheckThreeLinesSameDirection(PairIndex item1, PairIndex item2, int[,] matrix)
    {
        bool result = false;

        if (item1.First == item2.First)
        {
            int tmp = item1.First;
            for (int i = tmp - 1; i >= 0; i--)
            {
                if (matrix[i, item1.Second] != -1 || matrix[i, item2.Second] != -1) break;
                result = checkSameDirectionX(i, item1, item2, matrix);
                if (result) return true;
            }

            for (int i = tmp + 1; i < matrix.GetLength(0); i++)
            {
                if (matrix[i, item1.Second] != -1 || matrix[i, item2.Second] != -1) break;
                result = checkSameDirectionX(i, item1, item2, matrix);
                if (result) return true;
            }
            return false;
        }

        if (item1.Second == item2.Second)
        {
            int tmp = item1.Second;
            for (int i = tmp - 1; i >= 0; i--)
            {
                if (matrix[item1.First, i] != -1 || matrix[item2.First, i] != -1) break;
                result = checkSameDirectionY(i, item1, item2, matrix);
                if (result) return true;
            }
            for (int i = tmp + 1; i < matrix.GetLength(1); i++)
            {
                if (matrix[item1.First, i] != -1 || matrix[item2.First, i] != -1) break;
                result = checkSameDirectionY(i, item1, item2, matrix);
                if (result) return true;
            }
            return false;
        }
        return false;
    }
    public bool checkSameDirectionY(int tmp,  PairIndex item1, PairIndex item2, int[,] matrix)
    {
       
        bool result = CheckOneLine(new PairIndex(item1.First, tmp), new PairIndex(item2.First,tmp), matrix);
        if (result)
        {
            tracking.Clear();
            tracking.Add(item1);
            tracking.Add(new PairIndex(item1.First, tmp));
            tracking.Add(new PairIndex(item2.First, tmp));
            tracking.Add(item2);
            
        }
        return result;
    }
    public bool checkSameDirectionX(int tmp,  PairIndex item1, PairIndex item2, int[,] matrix)
    {
       
        bool result = CheckOneLine(new PairIndex(tmp, item1.Second), new PairIndex(tmp,item2.Second), matrix);
        if (result)
        {
            tracking.Clear();
            tracking.Add(item1);
            tracking.Add(new PairIndex(tmp, item1.Second));
            tracking.Add(new PairIndex(tmp, item2.Second));
            tracking.Add(item2);
            
        }
        return result;
    }
    
    public bool CheckThreeLines(PairIndex item1, PairIndex item2, int[,] matrix)
    {
        int rows = matrix.GetLength(0);
        int cols = matrix.GetLength(1);
      
        // Check Inside 
        bool result = false;
        int indexAdd = 1;
        int count = 0;

        #region Check Inside

        //Check Vertical 
        indexAdd = item1.First < item2.First ? 1 : -1;
        count = item1.First + indexAdd;
        while (count != item2.First)
        {
            PairIndex mid1 = new PairIndex(count, item1.Second);
            PairIndex mid2 = new PairIndex(count, item2.Second);
            result = CheckFourPoint(item1,item2, mid1, mid2, matrix);
            if (result) return true;
            count += indexAdd;
        }
        // Check Horizontal 
        indexAdd = item1.Second < item2.Second ? 1 : -1;
        count = item1.Second + indexAdd;
        while (count != item2.Second)
        {
            PairIndex mid1 = new PairIndex(item1.First, count);
            PairIndex mid2 = new PairIndex(item2.First, count);
            result = CheckFourPoint(item1, item2, mid1, mid2, matrix);
            if (result) return true;
            count += indexAdd;
        }
        

        #endregion

        #region Check Outside
        //Go Up
        PairIndex tmpitem1 = new PairIndex(item1.First, item1.Second);
        PairIndex tmpitem2 = new PairIndex(item2.First, item2.Second);
        tmpitem1.First = tmpitem1.First <=  tmpitem2.First ? tmpitem1.First : tmpitem2.First;
        tmpitem2.First = tmpitem1.First <= tmpitem2.First ? tmpitem2.First : tmpitem1.First;
        while (tmpitem1.First - 1 >= 0)
        {
            if (matrix[tmpitem1.First - 1, tmpitem1.Second] != -1) break;
            if (matrix[tmpitem2.First - 1, tmpitem2.Second] != -1) break;
            tmpitem1.First -= 1;
            tmpitem2.First -= 1;
            result = CheckFourPoint(item1, item2, tmpitem1, tmpitem2, matrix);
            if(result) return true;
        }
        //Go Down
        tmpitem1 = new PairIndex(item1.First, item1.Second);
        tmpitem2 = new PairIndex(item2.First, item2.Second);
        tmpitem1.First = tmpitem1.First <= tmpitem2.First ? tmpitem2.First : tmpitem1.First; 
        tmpitem2.First = tmpitem1.First <= tmpitem2.First ? tmpitem1.First : tmpitem2.First;
        while (tmpitem1.First + 1 < rows)
        {
            if (matrix[tmpitem1.First + 1, tmpitem1.Second] != -1) break;
            if (matrix[tmpitem2.First + 1, tmpitem2.Second] != -1) break;
            tmpitem1.First += 1;
            tmpitem2.First += 1;
            result = CheckFourPoint(item1, item2, tmpitem1, tmpitem2, matrix);
            if (result) return true;
        }
        // Go Left 
        tmpitem1 = new PairIndex(item1.First, item1.Second);
        tmpitem2 = new PairIndex(item2.First, item2.Second);
        tmpitem1.Second = tmpitem1.Second <=  tmpitem2.Second ? tmpitem1.Second : tmpitem2.Second;
        tmpitem2.Second = tmpitem1.Second <= tmpitem2.Second ? tmpitem2.Second : tmpitem1.Second;
        while (tmpitem1.Second - 1 >= 0)
        {
            if (matrix[tmpitem1.First, tmpitem1.Second - 1] != -1) break;
            if (matrix[tmpitem2.First, tmpitem2.Second - 1] != -1) break;
            tmpitem1.Second -= 1;
            tmpitem2.Second -= 1;
            result = CheckFourPoint(item1, item2, tmpitem1, tmpitem2, matrix);
            if(result) return true;
        }
        //GO Right 
        tmpitem1 = new PairIndex(item1.First, item1.Second);
        tmpitem2 = new PairIndex(item2.First, item2.Second);
        tmpitem1.Second = tmpitem1.Second <= tmpitem2.Second ? tmpitem2.Second : tmpitem1.Second; 
        tmpitem2.Second = tmpitem1.Second <= tmpitem2.Second ? tmpitem1.Second : tmpitem2.Second;
        while (tmpitem1.Second + 1 < cols)
        {
            if (matrix[tmpitem1.First, tmpitem1.Second + 1] != -1) break;
            if (matrix[tmpitem2.First, tmpitem2.Second + 1] != -1) break;
            tmpitem1.Second += 1;
            tmpitem2.Second += 1;
            result = CheckFourPoint(item1, item2, tmpitem1, tmpitem2, matrix);
            if (result) return true;
        }
        return false;
        
        
        

        
        #endregion


        
    }



    public bool CheckFourPoint(PairIndex item1, PairIndex item2, PairIndex mid1, PairIndex mid2, int[,] matrix)
    {
        if (matrix[mid1.First, mid1.Second] != -1) return false;
        if (matrix[mid2.First, mid2.Second] != -1) return false;

        bool result = false;
        var result1 = CheckOneLine(item1, mid1, matrix);
        var result2 = CheckOneLine(item2, mid2, matrix);
        var result3 = CheckOneLine(mid1, mid2, matrix);
        result = result1 && result2 && result3;
        if (result)
        {
            tracking.Clear();
            tracking.Add(item1);
            tracking.Add(mid1);
            tracking.Add(mid2);
            tracking.Add(item2);
        }
        return result;
        
    }
    
    


    public bool CheckTwoLines(PairIndex item1, PairIndex item2, int[,] matrix)
    {
       
        PairIndex midPair1 = new PairIndex(item2.First, item1.Second);
        PairIndex midPair2 = new PairIndex(item1.First, item2.Second);
       

        if ((CheckOneLine(midPair1, item1, matrix) && CheckOneLine(midPair1, item2, matrix)) && matrix[midPair1.First, midPair1.Second] == -1)
        {
            tracking.Add(item1);
            tracking.Add(midPair1);
            tracking.Add(item2);
            return true;
        }
        if((CheckOneLine(midPair2, item1, matrix) &&  CheckOneLine(midPair2, item2, matrix))&& matrix[midPair2.First, midPair2.Second] == -1)
        {
            tracking.Add(item1);
            tracking.Add(midPair2);
            tracking.Add(item2);
            return true;
        }
                  
        return false;

    }
    
    
    
    

    public bool CheckOneLine(PairIndex item1,  PairIndex item2, int[,] matrix)
    {
        
        
        
        int indexAdd = 1;
        if (item1.First == item2.First)
        {
            indexAdd = item1.Second >= item2.Second ? -1 : 1;
            int count = item1.Second + indexAdd;
          
            while (count != item2.Second)
            {
                if(matrix[item1.First, count] != -1) return false;
                count+=indexAdd;
            }
            return true;
        }
        else if (item1.Second == item2.Second)
        {
            indexAdd = item1.First >= item2.First ? -1 : 1;
            int count = item1.First + indexAdd;
            while (count != item2.First)
            {
                if(matrix[count, item1.Second] != -1) return false;
                count+=indexAdd;
            }
            return true;
        }
        
        
        return false;
    }
}
