using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndingText : MonoBehaviour
{
    public void Print(bool gameOver)
    {
        var tmp = GetComponent<TextMeshProUGUI>();
        tmp.text =
            $"<mark=#00000030>The family lasts <size=200%>{Services.Players.Count}<size=100%> generations\n" +
            $"with <size=200%>{Services.treeCount}<size=100%> trees planted! Beautiful.\n" +
            $"The family reached as high as <size=200%>{Services.ScoreBoard.Score}<size=100%>m.\n\n";
        if (gameOver)
        {
            tmp.text += "Press R to restart.";
        }
    }
}

