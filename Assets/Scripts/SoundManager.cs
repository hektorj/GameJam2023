using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance = null;

    public AudioClip[] audioCollection;
    public AudioSource audioSource;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != null)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        audioSource.GetComponent<AudioSource>();
    }
    /// <summary>
    /// Reproduce un sonido una vez segun un numero entero 0: sonido de daga con daga, 1: sonido de daga con madera, 2: sonido de daga con madera 2, 3:sonido de manzana cortada, 4: sonido de madera rompiendose
    /// </summary>
    /// <param name="opcion"></param>
    public void PlaySound(int opcion)
    {
        audioSource.PlayOneShot(audioCollection[opcion]);
    }
}
