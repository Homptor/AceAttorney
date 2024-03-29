using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private List<Music> allMusic;
    
    public static MusicManager instance;

    [HideInInspector] public Music musicPlaying = null;

    public static float currentPoint = 0;

    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;

        Dictionary<string, ArrayList> musicDict = new Dictionary<string, ArrayList>();
        musicDict = Globals.LoadTSV("Music Data");

        int i = 0;
        foreach(KeyValuePair<string, ArrayList> entry in musicDict) {
            if (i != 0) {
                Music music = new Music();
                
                music.name = entry.Key;
                music.loopStart = Convert.ToSingle(entry.Value[0]);
                music.loopEnd = Convert.ToSingle(entry.Value[1]);

                String path = "Music/" + music.name;

                music.source = gameObject.AddComponent<AudioSource>();
                music.source.clip = Resources.Load(path) as AudioClip;
                music.source.pitch = music.pitch;
                music.source.loop = true;

                allMusic.Add(music);
            }

            i++;
        }
    }

    public void setPoint()
    {
        currentPoint = musicPlaying.source.time;
    }

    public void goToPoint()
    {
        musicPlaying.source.time = currentPoint;
    }
    
    public void Stop()
    {
        musicPlaying?.source.Stop();
    }

    public Music Play (string name)
    {
        Debug.Log(name);
        Music s = allMusic.Find(x => x.name == name);
        if (s == null)
            return null;

        if (musicPlaying == s && Math.Abs(musicPlaying.source.volume - 1) < 0.1 && musicPlaying.source.isPlaying)
        {
            return musicPlaying;
        }

        try
        {
            if (musicPlaying.source.isPlaying)
            {
                fadeOut();
            }
        } catch {}

        musicPlaying = s;

        s.source.volume = 1;
        s.source.time = 0;
        s.source.Play();
        Debug.Log(s.source.isPlaying);

        return s;
    }

    private void Update()
    {
        if (musicPlaying.name != "")
        {
            if (musicPlaying.source.time > musicPlaying.loopEnd && musicPlaying.loopEnd != -1)
            {
                musicPlaying.source.time -= (musicPlaying.loopEnd - musicPlaying.loopStart);
            }
        }
    }

    public void fadeOut(float length=0.1f)
    {
        try
        {
            setPoint();
        }
        catch
        {
            return;
        }

        StartCoroutine(fadeTo(length, 0, musicPlaying));
    }
    
    public void fadeIn()
    {
        goToPoint();
        StartCoroutine(fadeTo(0.1f, 1, musicPlaying));
    }
    
    public IEnumerator fadeTo(float duration, float targetVolume, Music audioSource=null)
    {
        if (audioSource == null)
        {
            audioSource = musicPlaying;
        }
        
        float currentTime = 0;
        float start = audioSource.source.volume;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.source.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }

        audioSource.source.volume = targetVolume;

        if (audioSource.source.volume <= 0.1)
        {
            audioSource.source.Stop();
        }
    }

    public Music GetMusicPlaying() {
        return musicPlaying;
    }

    public void setMusicPlaying(Music music)
    {
        musicPlaying = music;
    }
}
