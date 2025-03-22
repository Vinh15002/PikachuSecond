using UnityEngine;
using UnityEngine.UI;

public class CreateNewBoardButton : MonoBehaviour
{
    
    private Button btn;
    [SerializeField]private BoardController boardController;
    private void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(NewGame);
    }

    private void NewGame()
    {
        boardController.ResetNewBoard();
    }
}
