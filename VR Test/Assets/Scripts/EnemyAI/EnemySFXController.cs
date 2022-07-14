using UnityEngine;
using Random = System.Random;

public class EnemySFXController : MonoBehaviour
{
    [SerializeField] private AudioClip _step;
    [SerializeField] private AudioClip _punch;
    [SerializeField] private AudioClip _cast;
    [SerializeField] private AudioClip _dying;
    [SerializeField] private AudioClip _casterDying;
    [SerializeField] private AudioClip _takeDamage;
    [SerializeField] private AudioClip _casterReact;
    [SerializeField] private AudioClip _eating;
    [SerializeField] private AudioClip _castingCaster;
    private AudioSource _audioSource;


    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void StepSound()
    {
        _audioSource.pitch = NextFloat(0.8f, 1.2f);
        _audioSource.volume = NextFloat(0.8f, 1);
        _audioSource.PlayOneShot(_step);
    }

    private void PunchSound()
    {
        _audioSource.pitch = NextFloat(0.8f, 1.2f);
        _audioSource.volume = NextFloat(0.8f, 1);
        _audioSource.PlayOneShot(_punch);
    }

    private void CastSound()
    {
        _audioSource.PlayOneShot(_cast);
    }

    private void TakeDamageSound()
    {
        _audioSource.pitch = NextFloat(0.9f, 1.1f);
        _audioSource.volume = NextFloat(0.8f, 1);
        _audioSource.PlayOneShot(_takeDamage);
    }
    
    private void CasterReactSound()
    {
        _audioSource.pitch = NextFloat(0.9f, 1.1f);
        _audioSource.volume = NextFloat(0.8f, 1);
        _audioSource.PlayOneShot(_casterReact);
    }

    private void DeathSound()
    {
        _audioSource.pitch = NextFloat(0.9f, 1.1f);
        _audioSource.PlayOneShot(_dying);
    }
    
    private void CasterDeathSound()
    {
        _audioSource.pitch = NextFloat(0.9f, 1.1f);
        _audioSource.PlayOneShot(_casterDying);
    }
    
    private void EatingSound()
    {
        _audioSource.pitch = NextFloat(0.9f, 1.1f);
        _audioSource.volume = NextFloat(0.8f, 1);
        _audioSource.PlayOneShot(_eating);
    }

    private void CasterCastingSound()
    {
        _audioSource.pitch = 1f;
        _audioSource.PlayOneShot(_castingCaster);
    }

    private static float NextFloat(float min, float max)
    {
        var random = new Random();
        var val = (random.NextDouble() * (max - min) + min);
        return (float)val;
    }
}