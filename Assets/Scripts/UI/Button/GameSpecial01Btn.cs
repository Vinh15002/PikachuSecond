using UnityEngine;
using UnityEngine.UI;


public class GameSpecial01Btn : MonoBehaviour
{
    private Button btn;
    [SerializeField]private BoardController boardController;

    private void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(PlayNewBoard);
    }

    private void PlayNewBoard()
    {
        boardController.SetNewTypeGame(TypeGame.Special01);
    }

    
}
