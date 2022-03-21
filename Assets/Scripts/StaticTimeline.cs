using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class StaticTimeline : MonoBehaviour
{
    PlayableDirector _Playable;
    Rule rule = new Rule();
    void Start()
    {
        _Playable = GetComponent<PlayableDirector>();
        _Playable.time = 0d;
    }
    void Update()
    {
        if (_Playable.time >= 5.00d)
        {
            Pause();
            rule.win();
            _Playable.time = 0d;
            SceneManager.LoadScene("Over", LoadSceneMode.Additive);
        }
    }

    public void Play()
    {
        _Playable.Play();
    }

    public void Pause()
    {
        _Playable.Pause();
    }
}
