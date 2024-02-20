using OWML.ModHelper;
using System.Collections.Generic;
using UnityEngine;

namespace SkybaseTimes
{
    public class SkybaseTimes : ModBehaviour
    {
        private static SkybaseTimes instance;

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

        private void Start()
        {
            ModHelper.Console.WriteLine(nameof(SkybaseTimes) + " has loaded!");

            LoadAudio();

            ModHelper.HarmonyHelper.AddPostfix<AudioManager>(nameof(AudioManager.Awake), typeof(SkybaseTimes), nameof(ReplaceInAudioManager));
            ModHelper.HarmonyHelper.AddPostfix<AudioLibrary>(nameof(AudioLibrary.BuildAudioEntryDictionary), typeof(SkybaseTimes), nameof(ReplaceInAudioLibrary));
        }

        private const int EndOfTime = (int)AudioType.EndOfTime;
        private AudioClip clip;

        private void LoadAudio()
        {
            clip = GetClip();
        }

        private static AudioClip GetClip()
        {
            return instance?.ModHelper.Assets.GetAudio("Skybase_Times.mp3") ?? null;
        }

        private static void ReplaceAudioEntry(ref Dictionary<int, AudioLibrary.AudioEntry> dictionary)
        {
            if (dictionary.ContainsKey(EndOfTime))
                dictionary[EndOfTime] = new AudioLibrary.AudioEntry(AudioType.EndOfTime, GetClips(), 0.2f);
            else
                dictionary.Add(EndOfTime, new AudioLibrary.AudioEntry(AudioType.EndOfTime, GetClips(), 0.2f));
        }

        private static AudioClip[] GetClips() => new[] { GetClip() };

        private static void ReplaceInAudioManager(AudioManager instance)
        {
            ReplaceAudioEntry(ref instance._audioLibraryDict);
        }

        private static void ReplaceInAudioLibrary(ref Dictionary<int, AudioLibrary.AudioEntry> result)
        {
            ReplaceAudioEntry(ref result);
        }
    }
}
