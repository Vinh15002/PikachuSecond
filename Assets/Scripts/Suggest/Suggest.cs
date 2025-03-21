using System.Collections.Generic;


public class Suggest
{
    public static List<PairIndex> GetSuggest(int[,] matrix)
    {
        List<PairIndex> suggestions = new List<PairIndex>();
        Dictionary<int, List<PairIndex>> dict = new Dictionary<int, List<PairIndex>>();
        for (int i = 1; i < matrix.GetLength(0) - 1; i++)
        {
            for (int j = 1; j < matrix.GetLength(1) - 1; j++)
            {
                int value = matrix[i, j];
                if (value != -1)
                {
                    if (!dict.ContainsKey(value))
                    {
                        dict.Add(value, new List<PairIndex>());
                        dict[value].Add(new PairIndex(i, j));
                    }
                    else
                    {
                        dict[value].Add(new PairIndex(i, j));
                    }
                }
            }
        }

        foreach (var item in dict)
        {
            List<PairIndex> itemIndexs = item.Value;
            for (int i = 0; i < itemIndexs.Count - 1; i++)
            {
                for (int j = i + 1; j < itemIndexs.Count; j++)
                {
                    if (LineSearching.CheckConnectItem(itemIndexs[i], itemIndexs[j], matrix))
                    {
                        suggestions.Add(itemIndexs[i]);
                        suggestions.Add(itemIndexs[j]);
                        return suggestions;
                    }
                }
            }
        }
        return suggestions;
    }
}
