using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public int time = 300;

    public Text timer;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TimeReduce());
    }

    // Update is called once per frame
    void Update()
    {
        

        timer.text = time.ToString("0");
    }

    IEnumerator TimeReduce()
    {
        while(time > 0)
        {
            time--;
            yield return new WaitForSeconds(1);
        }
    }
}
