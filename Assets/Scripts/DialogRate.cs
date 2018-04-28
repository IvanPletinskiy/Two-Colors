using UnityEngine;

public class DialogRate : MonoBehaviour
{

    

    //	public static bool isDialogRate=false;
    public static int ATTEMPTSFORDIALOG = 8;

    void Update()
    {

    }

	public static void DialogRateShow(GameObject dialogRate)
    {
		print ("Rate");
        dialogRate.SetActive(true);
        Time.timeScale = 0;
    }
}