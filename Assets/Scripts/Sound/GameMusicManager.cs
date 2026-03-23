using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class GameMusicManager : MonoBehaviour
{
    [SerializeField] private AudioClip backgroundClip; // Background music clip for the level

    private void Start()
    {
        // Start playing background music at a fixed volume
        SoundFXManager.Instance.PlayMusic(backgroundClip, 0.4f);
    }
}