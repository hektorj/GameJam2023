using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChangeTextInAnimator : MonoBehaviour
{
    public TextMeshPro countDownText;
    private static int intText = 4;

    //esta funcion es llamada cada vez que inicia la animacion en loop de countDownText
    void ChangeTextHere()
    {
        intText--;

        if (intText > 0)
            countDownText.text = intText.ToString();
        else if (intText == 0)
            countDownText.text = "Adelante!";
        else if (intText < -1)
        {
            GameManager.instance.countdownDone = true;
            intText = 4;
            Debug.Log("termina conteo");
        }
        else
            return;
    }

}
