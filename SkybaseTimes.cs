using OWML.ModHelper;
using System.Collections.Generic;
using UnityEngine;

namespace SkybaseTimes
{
    public class SkybaseTimes : ModBehaviour
    {
        private static SkybaseTimes instance;
        private const int EndOfTime = (int)AudioType.EndOfTime;
        private const float DefaultVolume = 0.2f;

        private static SkybaseTimes Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<SkybaseTimes>();
                }
                return instance;
            }
        }

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            ModHelper.Console.Log($"{nameof(SkybaseTimes)} has loaded!");
            ReplaceInAudioManager();
            ReplaceInAudioLibrary();
        }

        private static void ReplaceAudioEntry(Dictionary<int, AudioLibrary.AudioEntry> dictionary)
        {
            if (dictionary.TryGetValue(EndOfTime, out var audioEntry))
            {
                audioEntry = new AudioLibrary.AudioEntry(AudioType.EndOfTime, GetClips(), DefaultVolume);
            }
            else
            {
                dictionary.Add(EndOfTime, new AudioLibrary.AudioEntry(AudioType.EndOfTime, GetClips(), DefaultVolume));
            }
        }

        public static void ReplaceInAudioManager()
        {
            ReplaceAudioEntry(Instance.ModHelper.AudioManager.AudioLibraryDict);
        }

        public static void ReplaceInAudioLibrary()
        {
            ReplaceAudioEntry(Instance.ModHelper.AudioManager.AudioLibraryDict);
        }

        private static AudioClip[] GetClips() => new AudioClip[1] { GetClip() };

        private static AudioClip GetClip()
        {
            return instance.ModHelper.Assets.GetAudio("Skybase_Times.mp3");
        }
    }
}