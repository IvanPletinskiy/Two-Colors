using UnityEngine;

public class DialogRate : MonoBehaviour
{

    public static GameObject dialogRate;

    //	public static bool isDialogRate=false;
    public static int ATTEMPTSFORDIALOG = 8;

    void Update()
    {
     	DialogRateShow ();

    }

    public static void DialogRateShow()
    {
        dialogRate.SetActive(true);
        Time.timeScale = 0;
    }
}