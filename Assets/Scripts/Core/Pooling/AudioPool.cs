using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class is responsible for pooling audio sources to optimize performance when playing multiple sounds.
public class AudioPool : MonoBehaviour
{
    [SerializeField] private AudioSource audioPrefab; // Prefab of the audio source to be pooled.
    [SerializeField] private int prewarmCount = 8; // Number of audio sources to pre-instantiate in the pool.

    // Queue to hold the free pooled audio sources.
    private readonly Queue<AudioSource> free = new();
    // List to hold all the audio sources in the pool for management.
    private readonly List<AudioSource> pool = new();

    //awake is called when the script instance is being loaded.
    private void Awake()
    {
        //Make sure the prefab is not null before prewarming the pool.
        if(!audioPrefab)
        {
            var go = new GameObject("AudioSourcePrefab_RunTime"); // Create a new GameObject to serve as the audio source prefab if one is not assigned.
            go.transform.SetParent(transform); // Set the parent of the new GameObject to be the same as this AudioPool.
            var src = go.AddComponent<AudioSource>(); // Add an AudioSource component to the new GameObject.
            src.playOnAwake = false; // Set the playOnAwake property to false to prevent the audio from playing immediately.
            audioPrefab = src; // Assign the newly created AudioSource as the prefab for the pool.
            go.SetActive(false); // Deactivate the GameObject to prevent it from being used directly.
        }

        //Prewarm the pool with the specified number of audio sources.
        Prewarm(prewarmCount);
    }

    // Method to pre-instantiate a specified number of audio sources and add them to the pool.
    private void Prewarm(int count)
    {
        for (int i = 0; i < count; i++)
        {
            var src = CreateOne(); // Create a new audio source and add it to the pool.
            ReturnToPool(src); // Return the newly created audio source to the pool to be available for use.
        }
    }

    // Method to create a new audio source instance from the prefab and add it to the pool.
    private AudioSource CreateOne()
    {
        var src = Instantiate(audioPrefab, transform); // Instantiate a new audio source from the prefab and set its parent to this AudioPool.
        src.playOnAwake = false; // Ensure the new audio source does not play on awake.
        src.gameObject.name = $"AudioSource_{pool.Count}"; // Name the new audio source for easier identification in the hierarchy.
        src.gameObject.SetActive(false); // Deactivate the new audio source to prevent it from being used immediately.
        pool.Add(src); // Add the new audio source to the pool list for management.
        return src; // Return the newly created audio source.
    }

    //Method to return an audio source to the pool after it has finished playing.
    public void ReturnToPool(AudioSource src)
    {
        src.Stop(); // Stop the audio source to ensure it is not playing when returned to the pool.
        src.clip = null; // Clear the audio clip to free up memory and prevent unintended playback.
        src.gameObject.SetActive(false); // Deactivate the audio source to make it available for reuse.
        free.Enqueue(src); // Add the audio source back to the queue of free audio sources.
    }

    //Method to play an audio clip using an audio source from the pool.
    public void Play(AudioClip clip, Vector3 position, float volume = 1f, float pitch = 1f)
    {
        // Check clip is not null before attempting to play it.
        if (!clip) return;

        var src = free.Count > 0 ? free.Dequeue() : CreateOne(); // Get an audio source from the pool or create a new one if the pool is empty.

        src.transform.position = position; // Set the position of the audio source to the specified position.
        src.clip = clip; // Assign the audio clip to the audio source.
        src.volume = volume; // Set the volume of the audio source to the specified volume.
        src.pitch = pitch; // Set the pitch of the audio source to the specified pitch. 

        src.gameObject.SetActive(true); // Activate the audio source to make it available for playback.
        src.Play(); // Play the audio source.

        // Start a coroutine to return the audio source to the pool after the clip has finished playing.
        StartCoroutine(ReturnAfterPlaying(src, clip.length / pitch)); // Calculate the duration of the clip based on its length and pitch to determine when to return it to the pool.
    }

    // Coroutine to return an audio source to the pool after it has finished playing.
    private IEnumerator ReturnAfterPlaying(AudioSource src, float delay)
    {
        yield return new WaitForSeconds(delay); // Wait for the specified delay (duration of the clip) before returning the audio source to the pool.
        ReturnToPool(src); // Return the audio source to the pool after it has finished playing.
    }
}
