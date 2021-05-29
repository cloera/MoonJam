using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimingManager : MonoBehaviour
{
    [SerializeField]
    public int beatsPerMinute = 128;

    private float beatsPerSecond;
    private float beatLength;
    private float barLength;
    private int barCount;
    
    // Start is called before the first frame update
    void Start()
    {
        beatsPerSecond = beatsPerMinute / 60.0f;
        beatLength = 1.0f / beatsPerSecond;
        barLength = beatLength * 4.0f;
        barCount = 0;

        StartCoroutine(ExcecuteAfterTime(barLength));
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator ExcecuteAfterTime(float time)
    {
        while(true)
        {
            yield return new WaitForSeconds(time);

            barCount++;
            Debug.Log("Bar Count: " + barCount);
        }
    }

}
