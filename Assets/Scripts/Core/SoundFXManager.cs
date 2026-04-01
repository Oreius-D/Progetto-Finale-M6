using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundFXManager : Singleton<SoundFXManager>
{
    // Reference to the AudioPool component that manages the pooling of audio sources.
    [Header("Audio Pool Reference")]
    [SerializeField] private AudioPool audioPool;

    //Default volume and pitchfor sound effects, can be adjusted in the inspector.
    [Header("Sound Effect Settings")]
    [Range(0f, 1f)][SerializeField] private float defaultVolume = 1f;
    [SerializeField] private Vector2 pitchRange = new Vector2(0.95f, 1.05f); // Range for random pitch variation to add variety to sound effects.

    // Awake is called when the script instance is being loaded.
    protected override void Awake()
    {
        base.Awake(); // Call the Awake method of the base Singleton class to ensure proper initialization.
        if (!audioPool) audioPool = GetComponentInChildren<AudioPool>(true); // Attempt to find an AudioPool component in the children of this GameObject.
    }

    private void OnEnable()
    {
        // Subscribe to the sceneLoaded event of the SceneManager to ensure that we can find the AudioPool component when a new scene is loaded.
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // Unsubscribe from the sceneLoaded event to prevent potential memory leaks or unintended behavior when this object is disabled or destroyed.
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // When a new scene is loaded, attempt to find the AudioPool component again. This is important because the AudioPool might be part of the new scene and we need to ensure we have a reference to it.
        if (!audioPool)
            audioPool = GetComponentInChildren<AudioPool>(true);
    }

    // Method to play a sound effect at a specific position in the world.
    public void PlaySoundEffect(AudioClip clip, Vector3 position)
    {
        if (!audioPool) audioPool = GetComponentInChildren<AudioPool>(true); // Try get audiopool on the fly
        if (!clip || !audioPool) return; // If the clip or audio pool is not set, exit the method to avoid errors.

        float randomPitch = Random.Range(pitchRange.x, pitchRange.y); // Generate a random pitch within the specified range for variety.
        audioPool.Play(clip, position, defaultVolume, randomPitch); // Use the audio pool to play the clip at the specified position with the default volume and random pitch.
    }

    public void PlaySoundEffect(AudioClip clip, Vector3 position, float volume, float pitch)
    {
        if (!clip || !audioPool) return; // If the clip or audio pool is not set, exit the method to avoid errors..
        audioPool.Play(clip, Vector3.zero, volume, pitch); // Use the audio pool to play the clip at the origin (or you could choose a different default position) with the default volume and random pitch.
    }
}
