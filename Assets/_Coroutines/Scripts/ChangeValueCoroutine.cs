using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;

public class ChangeValueCoroutine : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    Coroutine timerCoroutine;
    Coroutine timerCoroutine2;


    void Start()
    {
        timerCoroutine2 = StartCoroutine(RunTimer(15, 2));
        timerCoroutine =StartCoroutine(RunTimer(10, 3));
        
    }

    private IEnumerator RunTimer(int maxCount, int quitWaitTime)
    {
        int count = 0;
        while(count<maxCount)
        {
            yield return new WaitForSeconds(1);
            count++;
            timerText.text = count.ToString();
            print("I am runnig");
        }
        yield return new WaitForSeconds(quitWaitTime);
        print("corountine ends");

    }

    private void OnDisable()
    {
        StopCoroutine("RunTimer");
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) 
        {
            StopCoroutine(timerCoroutine);
        
        }
    }
}
