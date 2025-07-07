using UnityEngine;
using System.Collections.Generic;
using DefineEnum;
using DefineStructure;
using System.Collections;

public class SoundManager : TSingleton<SoundManager>
{
    Dictionary<BGMName, AudioClip> _bgmClipDic;
    Dictionary<SFXName, AudioClip> _sfxClipDic;

    AudioPlayerDESC _bgmDesc;
    AudioSource _bgmPlayer;
    AudioPlayerDESC _sfxDesc;
    AudioSource _sfxPlayer;
    Queue<AudioSource> _sfxQueue;


    public void LoadAllSound()
    {
        _bgmClipDic = new Dictionary<BGMName, AudioClip>();
        _sfxClipDic = new Dictionary<SFXName, AudioClip>();
        _sfxQueue = new Queue<AudioSource>(2);


        _bgmPlayer = gameObject.AddComponent<AudioSource>();
        _sfxPlayer = gameObject.AddComponent<AudioSource>();

        _bgmDesc = new AudioPlayerDESC(_bgmPlayer, 1, false);
        _bgmDesc = new AudioPlayerDESC(_sfxPlayer, 1, false, false);

        string path = "Sounds/";
        int count = (int)BGMName.Max;
        AudioClip clip;
        BGMName bgmName;

        for (int i = 0; i < count; i++)
        {
            bgmName = (BGMName)i;
            clip = Resources.Load<AudioClip>(path + "BGM/" + bgmName);
            _bgmClipDic.Add(bgmName, clip);
        }

        count = (int)SFXName.Max;
        SFXName sfxName;

        for (int i = 0; i < count; i++)
        {
            sfxName = (SFXName)i;
            clip = Resources.Load<AudioClip>(path + "SFX/" + sfxName);
            _sfxClipDic.Add(sfxName, clip);
        }
    }

    public void PlayBGM(BGMName name)
    {
        if (!_bgmClipDic.ContainsKey(name))
        {
            Debug.LogFormat("{0} AudioClip은 없습니다.", name);
            return;
        }
        _bgmPlayer.clip = _bgmClipDic[name];
        _bgmPlayer.Play();
    }
    public void PlaySFX(SFXName name)
    {
        if (!_sfxClipDic.ContainsKey(name))
        {
            Debug.LogFormat("{0} AudioClip은 없습니다.", name);
            return;
        }
        _sfxPlayer.PlayOneShot(_sfxClipDic[name]);

        //_sfxPlayer.clip = _sfxClipDic[name];
        //_sfxPlayer.Play();
        //StartCoroutine(SFXReturn(_sfxPlayer));

        //if (_sfxQueue.Count <= 0)
        //{
        //    _sfxPlayer = gameObject.AddComponent<AudioSource>();
        //}
        //else
        //{
        //    _sfxPlayer = _sfxQueue.Dequeue();
        //}
    }

    //public IEnumerator SFXReturn(AudioSource source)
    //{
    //    yield return new WaitForSeconds(source.clip.length);

    //    _sfxQueue.Enqueue(source);
    //}
}
