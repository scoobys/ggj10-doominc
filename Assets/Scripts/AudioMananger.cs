using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager instance;
        public List<AudioClip> audioClips;
        public Dictionary<string, AudioClip> audioMap;

        public float musicVolume = 1;
        public float soundVolume = 1;

        public int maxAudios = 16;
        private List<AudioSource> sourcePool;

        public AudioManager()
        {
            instance = this;
        }

        void Awake()
        {
            audioMap = new Dictionary<string, AudioClip>();

            foreach (AudioClip clip in audioClips)
            {
                audioMap.Add(clip.name, clip);
            }

            sourcePool = new List<AudioSource>();
            GameObject parent = new GameObject("Audios");

            for (int i = 0; i < maxAudios; i++)
            {
                GameObject gob = new GameObject("Audio");
                gob.transform.parent = parent.transform;
                AudioSource audioSource = gob.AddComponent<AudioSource>();
                sourcePool.Add(audioSource);
            }

        }

        public void setMusicVolume(float volume)
        {
            musicVolume = volume;
            if (sourcePool != null)
            {
                foreach (AudioSource source in sourcePool)
                {
                    if (source.isPlaying && source.loop)
                    {
                        source.volume = volume;
                    }
                }
            }
        }

        public void stopAllMusic()
        {
            if (sourcePool != null)
            {
                foreach (AudioSource source in sourcePool)
                {
                    if (source.isPlaying && source.loop)
                    {
                        source.Stop();
                    }
                }
            }
        }

        public void play(string audioName)
        {
            play(audioName, 1f, false);
        }

        public void play(string audioName, float soundVolume, bool isLooping)
        {
            //Debug.Log("audioname___" + audioName);
            if (audioName == null || audioName == "")
            {
                return;
            }
            AudioClip clip;
            bool succ = audioMap.TryGetValue(audioName, out clip);
            if (succ)
            {
                foreach (AudioSource source in sourcePool)
                {
                    if (!source.isPlaying)
                    {
                        source.clip = clip;
                        //source.clip = Resources.Load(name) as AudioClip;
                        source.volume = soundVolume * (isLooping ? musicVolume : soundVolume);
                        source.loop = isLooping;
                        source.Play();

                        break;
                    }
                }
            }
            else
            {
                Debug.LogWarning("Could not find audio: " + succ);
            }
        }


    }
}
