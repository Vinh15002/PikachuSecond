
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AutoPlayButton : MonoBehaviour
{
    private Button btn;
    [SerializeField]private BoardController boardController;

    private void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(AutoPlay);
    }

    private void AutoPlay()
    {
        StartCoroutine(AutoPlayCourutine());
       
        
    }

    private IEnumerator AutoPlayCourutine()
    {
        while (boardController.Counter != 0)
        {
            boardController.SuggestItem();
            yield return new WaitForSeconds(0.5f);
        }
    }
}
