using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundControl : Singleton<SoundControl>
{
    [SerializeField] private AudioSource attack;
    [SerializeField] private AudioSource baseWin;
    [SerializeField] private AudioSource coin;
    [SerializeField] private AudioSource damage;
    [SerializeField] private AudioSource jump;
    [SerializeField] private AudioSource loose;
    [SerializeField] private AudioSource music;
    [SerializeField] private AudioSource click;
    [SerializeField] private AudioSource win;
    [SerializeField] private AudioSource takeObj;

    [SerializeField] private AudioSource shoot;

    [SerializeField] private List<AudioClip> musicList;
    private int currTrack = 0;

    private void Start()
    {
        if (GameStats.useMusic)
        {
            Music();
        }
    }

    public void Attack()
    {
        if (Time.timeScale!=0)
        {
            attack.Play();
        }
    }


    public void BaseWin()
    {
        baseWin.Play();
    }

    public void TakeObj()
    {
        takeObj.Play();
    }

    public void Shoot()
    {
        shoot.Play();
    }

    public void Win()
    {
        win.Play();
    }


    public void Coin()
    {
        coin.Play();
    }


    public void Damage()
    {
        damage.Play();
    }
  

    public void Jump()
    {
        if (!jump.isPlaying)
        {
            jump.PlayOneShot(jump.clip);
        }
        
        
    }


    public void Loose()
    {
        loose.Play();
    }

    public void Music()
    {
        if (musicList.Count>0)
        {
            StartCoroutine(ControlMusic());
        }
        else
        {
            music.Play();
        }
        
    }
    public void StopMusic()
    {
        music.Stop();
    }

    public void Click()
    {
        if (GameStats.playClick)
        {
            click.Play();
        }
    }



    IEnumerator ControlMusic()
    {
        if (musicList.Count==0)
        {
            yield break;
        }

        music.clip = musicList[Random.Range(0, musicList.Count)];
        music.Play();

        while (GameStats.useMusic)
        {
            yield return new WaitForSecondsRealtime(1f);
            if (music.isPlaying == false)
            {
                currTrack++;
                if (currTrack>= musicList.Count )
                {
                    currTrack = 0;
                }

                music.clip = musicList[currTrack];
                music.Play();
            }
        }

        music.Stop();


    }
}
