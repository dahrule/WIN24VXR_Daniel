using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChangeValuesUpdate : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    float count = 0;
    int timeDisplay = 0;

    void Update()
    {
        RunTimer(1);
    }

    private void RunTimer(int time)
    {
       
        count += Time.deltaTime;
        if(count>=time)
        {
            timeDisplay++;
            timerText.text=timeDisplay.ToString();
            count= 0;
        }

    }
}
