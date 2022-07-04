using UnityEngine;
using Random = System.Random;

public class EnemySFXController : MonoBehaviour
{
    [SerializeField] private AudioClip _step;
    [SerializeField] private AudioClip _punch;
    [SerializeField] private AudioClip _cast;
    [SerializeField] private AudioClip _dying;
    [SerializeField] private AudioClip _takeDamage;
    private AudioSource _audioSource;


    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Step()
    {
        _audioSource.pitch = NextFloat(0.8f, 1.2f);
        _audioSource.volume = NextFloat(0.8f, 1);
        _audioSource.PlayOneShot(_step);
    }

    private void Punch()
    {
        _audioSource.pitch = NextFloat(0.8f, 1.2f);
        _audioSource.volume = NextFloat(0.8f, 1);
        _audioSource.PlayOneShot(_punch);
    }

    private void Cast()
    {
        _audioSource.pitch = NextFloat(0.9f, 1.1f);
        _audioSource.volume = NextFloat(0.8f, 1);
        _audioSource.PlayOneShot(_cast);
    }

    private void TakeDamage()
    {
        _audioSource.pitch = NextFloat(0.9f, 1.1f);
        _audioSource.volume = NextFloat(0.8f, 1);
        _audioSource.PlayOneShot(_takeDamage);
    }

    private void Death()
    {
        _audioSource.pitch = NextFloat(0.9f, 1.1f);
        _audioSource.volume = NextFloat(0.8f, 1);
        _audioSource.PlayOneShot(_dying);
    }

    private static float NextFloat(float min, float max)
    {
        var random = new Random();
        var val = (random.NextDouble() * (max - min) + min);
        return (float)val;
    }
}