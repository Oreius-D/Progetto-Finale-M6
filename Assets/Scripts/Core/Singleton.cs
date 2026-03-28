using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Singleton class template, to be inherited by any class that needs to be a singleton.
public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    //The instance of the singleton. With properties, so it can only be set privately.
    public static T Instance { get; private set; }

    //Awake is called when the script instance is being loaded.
    protected virtual void Awake()
    {
        // If an instance of the singleton already exists and it's not this one, destroy this game object to enforce the singleton pattern.
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // Set the instance to this singleton and make it persist across scenes.
        Instance = (T)(MonoBehaviour)this;

        // IMPORTANT: must be root for DontDestroyOnLoad
        if (transform.parent != null)
            transform.SetParent(null);

        //DontDestroyOnLoad makes sure that the singleton is not destroyed when loading a new scene.
        DontDestroyOnLoad(gameObject);
    }

    // OnDestroy is called when the MonoBehaviour will be destroyed.
    protected virtual void OnDestroy()
    {
        // If this instance is the current singleton instance, set it to null when destroyed.
        if (Instance == this)
            Instance = null;
    }
}
