using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ResetButton : MonoBehaviour
{
    private Button btn;
    [SerializeField]private BoardController boardController;

    private void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(ResetBoard);
    }

    private void ResetBoard()
    {
        boardController.ResetMatrix();
    }
}
