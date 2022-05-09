using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMusicLoop : MonoBehaviour
{
    [SerializeField] AudioClip menuMusic;
    private bool isPlaying;
    private float timeElapsed = 0;

    private void Update()
    {

        if(timeElapsed > 95)
        {
            isPlaying = false;
        }
        if(isPlaying == false)
        {
            timeElapsed = 0f;
            AudioHelper.PlayClip2D(menuMusic, 1f);
            isPlaying = true;
        }
        timeElapsed += Time.deltaTime;
    }
}
