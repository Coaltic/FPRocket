using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    public static GameManager gameManager;
    public static GameObject soundManager;

    private static GameObject oneShotGameObject;
    private static AudioSource oneShotAudioSource;
    public static GameObject oneShotMusicGameObject;
    private static AudioSource oneShotMusicAudioSource;

    private static Sound currentMusic;
    private static Sound previousMusic;

    private static Dictionary<Sound, float> soundTimeDictionary;
    public static Dictionary<Sound, float> soundLengthDictionary;
    private static Dictionary<Sound, float> soundVolumeDictionary;
    //public static Dictionary<Sound, float> musicCutOffDictionary;
    private static SoundAssets.SoundAudioClip currentSoundAudioClip;
    private static float currentArrayAudioClipLength;

    [HideInInspector] public static bool canMusicPlay = true;

    private void Start()
    {
        oneShotMusicGameObject = new GameObject("One Shot Music Sound");
        oneShotMusicAudioSource = oneShotMusicGameObject.AddComponent<AudioSource>();
        oneShotMusicGameObject.transform.parent = GameObject.Find("SoundManager").transform;
        //if (oneShotMusicGameObject != null) { oneShotAudioSource = oneShotMusicGameObject.GetComponent<AudioSource>(); }
    }

    public enum Sound
    {
        BackgroundMusic,

    }

    public static void InitializeDictionary()
    {
        soundTimeDictionary = new Dictionary<Sound, float>();
        soundLengthDictionary = new Dictionary<Sound, float>();
        soundVolumeDictionary = new Dictionary<Sound, float>();
        //musicCutOffDictionary = new Dictionary<Sound, float>();

        SetDirectory(Sound.BackgroundMusic, 0, 0, 1);
        
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private static void SetDirectory(Sound sound, float length, float delay, float volume)
    {
        soundTimeDictionary[sound] = delay;
        soundLengthDictionary[sound] = length;
        soundVolumeDictionary[sound] = volume;
    }

    /*private static void SetDirectory(Sound sound, float length, float delay, float volume, float cutOff)
    {
        soundTimeDictionary[sound] = delay;
        soundLengthDictionary[sound] = length;
        soundVolumeDictionary[sound] = volume;
        musicCutOffDictionary[sound] = cutOff;
    }*/

    /*public static void SetMusicClipCuttOff()
    {
        musicCutOffDictionary[previousMusic] = oneShotMusicAudioSource.time;
        Debug.LogError("cut off set: " + musicCutOffDictionary[previousMusic]);
    }*/

    public static void PlaySound(Sound sound, Vector3 position)
    {
        //GetAudioClip(sound);

        if (CanPlaySound(sound))
        {
            GameObject soundGameObject = new GameObject("Sound");
            soundGameObject.transform.position = position;
            AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
            audioSource.spatialBlend = 1.0f;
            audioSource.rolloffMode = AudioRolloffMode.Linear;
            audioSource.volume = soundVolumeDictionary[sound];
            audioSource.clip = GetAudioClip(sound);
            audioSource.Play();

            //Debug.Log("CLIP PLAYED: " + audioSource.clip.name);
            Object.Destroy(soundGameObject, audioSource.clip.length);
        }
    }

    public static void PlaySound(Sound sound)
    {
        //Debug.Log("SOUND DELAY TIMER: " + soundTimerDictionary[sound]);
        //

        if (CanPlaySound(sound))
        {
            if (oneShotGameObject == null)
            {
                oneShotGameObject = new GameObject("One Shot Sound");
                oneShotAudioSource = oneShotGameObject.AddComponent<AudioSource>();
                oneShotAudioSource.PlayOneShot(GetAudioClip(sound), soundVolumeDictionary[sound]);
            }
            else
            {
                if (oneShotAudioSource != null)
                {
                    oneShotAudioSource = oneShotGameObject.AddComponent<AudioSource>();
                    oneShotAudioSource.PlayOneShot(GetAudioClip(sound), soundVolumeDictionary[sound]);
                }
            }
        }
    }

    public static void PlayMusic(Sound sound)
    {
        GetAudioClip(sound);
        if (CanPlaySound(sound))
        {

            //if (oneShotMusicGameObject == null)
            //{
            //    oneShotMusicGameObject = new GameObject("One Shot Music Sound");
            //    oneShotMusicAudioSource = oneShotMusicGameObject.AddComponent<AudioSource>();
            //    oneShotMusicAudioSource.PlayOneShot(GetAudioClip(sound), soundVolumeDictionary[sound]);
            //    previousMusic = sound;

            //}
            //else
            //{
            oneShotMusicAudioSource.Stop();
            //soundLengthDictionary[previousMusic] = 0;
            if (sound != previousMusic)
            { soundLengthDictionary[previousMusic] = 0; previousMusic = sound; }
            oneShotMusicAudioSource.PlayOneShot(GetAudioClip(sound), soundVolumeDictionary[sound]);
            //oneShotMusicAudioSource.time = musicCutOffDictionary[sound];
            //}
        }
    }

    public static void StopOneShotSound(Sound sound)
    {
        oneShotAudioSource.clip = GetAudioClip(sound);
        oneShotAudioSource.Stop();
    }
    public static bool IsSoundPlaying(Sound sound)
    {
        oneShotAudioSource.clip = GetAudioClip(sound);
        if (oneShotAudioSource.isPlaying) { return true; }
        return false;
    }

    private static bool CanPlaySound(Sound sound)
    {
        switch (sound)
        {
            default:
                return true;

            case Sound.BackgroundMusic:
                if (soundTimeDictionary.ContainsKey(sound))
                {
                    if (gameManager.currentState != GameManager.GameState.InGameplay) { return false; }
                    if (!oneShotMusicAudioSource.isPlaying) { canMusicPlay = true; }
                    if (canMusicPlay)
                    {
                        canMusicPlay = false;
                        return true;
                    }
                    else { return false; }
                }
                else { return false; }
                

            // EXAMPLE OF RANDOMLY OCCURING NOISES 
                /*case Sound.CaveAmbience:
                    if (soundTimeDictionary.ContainsKey(sound))
                    {
                        float lastTimePlayed = soundTimeDictionary[sound];
                        float delayTimerMax = soundLengthDictionary[sound];
                        if (lastTimePlayed + delayTimerMax < Time.time)
                        {
                            soundLengthDictionary[sound] = currentArrayAudioClipLength + Random.Range(10.0f, 20.0f);
                            soundTimeDictionary[sound] = Time.time;
                            return true;
                        }
                        else { return false; }
                    }
                    else { return true; }*/

                // EXAMPLE OF REPEATING MUSIC
                /*case Sound.CharacterSelectionMusic:
                    if (soundTimeDictionary.ContainsKey(sound))
                    {
                        if (gameManager.currentState != GameManager.GameState.InGameplay) { return false; }
                        if (!oneShotMusicAudioSource.isPlaying) { canMusicPlay = true; }
                        if (canMusicPlay)
                        {
                            canMusicPlay = false;
                            return true;
                        }
                        else { return false; }
                    }
                    else { return true; }*/

                // EXAMPLE OF ONESHOT SOUND - NO NEED FOR A DELAY (PLAYS AS NEEDED)
                /*case Sound.Clang:
                    if (soundTimeDictionary.ContainsKey(sound))
                    {
                        return true;
                    }
                    else { return true; }*/

        }
    }

    private static AudioClip GetAudioClip(Sound sound)
    {
        foreach (SoundAssets.SoundAudioClip soundAudioClip in SoundAssets.i.soundAudioClipArray)
        {
            if (soundAudioClip.sound == sound)
            {
                currentSoundAudioClip = soundAudioClip;
                int i = Mathf.FloorToInt(Random.Range(0, soundAudioClip.audioClipArray.Length));
                //Debug.Log("currentArrayAudioClip: " + i);
                currentArrayAudioClipLength = soundAudioClip.audioClipArray[i].length;

                return soundAudioClip.audioClipArray[i];
            }
        }
        return null;
    }
}