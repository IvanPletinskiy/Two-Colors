using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogRate : MonoBehaviour
{

    public static GameObject dialogRate;

    //	public static bool isDialogRate=false;
    public static int ATTEMPTSFORDIALOG = 3;

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