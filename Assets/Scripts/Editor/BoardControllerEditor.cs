using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BoardController))]

public class BoardControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        BoardController controller = (BoardController)target;
        
        GUILayout.Label("Show Matrix", EditorStyles.boldLabel);
        int [,] matrix = controller.Matrix;
      
        GUILayout.Label("Show Board", EditorStyles.boldLabel);
        for (int i = matrix.GetLength(0) -1 ; i >= 0 ; i--)
        {
            EditorGUILayout.BeginHorizontal();
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                EditorGUILayout.LabelField(matrix[i, j].ToString(), GUILayout.Width(20));
            }
            EditorGUILayout.EndHorizontal();
        }
        
    }
}
