using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogRate : MonoBehaviour
{

    public static GameObject dialogRate;

    public Camera mainCamera;

    //	public static bool isDialogRate=false;
    public static int ATTEMPTSFORDIALOG = 10;

    void Update()
    {
        //		if (isDialogRate && PlayerPrefs.GetInt("IsRate") != 1 && RespawnScript.isHeard == false)
        //			DialogRateShow ();

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            Ray ray = mainCamera.ScreenPointToRay(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "Button")
                {
                    dialogRate.SetActive(false);
                    Preferences.resetAttempts();
                    Time.timeScale = 1;
                }
                if (hit.collider.name == "Yes")
                {
                    Application.OpenURL("https://play.google.com/store/apps/details?id=com.handen.twocolors");
                    dialogRate.SetActive(false);
                    Preferences.setRateShown(true);
                }
                if (hit.collider.name == "No, thanks")
                {
                    Preferences.setRateShown(true);
                    dialogRate.SetActive(false);
                    //					isDialogRate = false;
                    Time.timeScale = 1;
                }
            }
        }
    }

    public static void DialogRateShow()
    {
        dialogRate.SetActive(true);
        Time.timeScale = 0;
    }
}