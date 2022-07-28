using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class PlayerSFXController : MonoBehaviour
{
    [SerializeField] private AudioClip _heal;
    [SerializeField] private AudioClip _speedBoost;
    [SerializeField] private AudioClip _speedBoostRemoved;
    [SerializeField] private AudioClip _damageReduction;
    [SerializeField] private AudioClip _damageReductionRemoved;
    [SerializeField] private AudioClip _step;
    [SerializeField] private AudioClip _takeDamage1;
    [SerializeField] private AudioClip _takeDamage2;
    [SerializeField] private AudioClip _takeDamage3;
    [SerializeField] private AudioClip _death1;
    [SerializeField] private AudioClip _death2;
    [SerializeField] private AudioClip _death3;
    private AudioSource _audioSource;
    private Random _random = new Random();

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void HealSound()
    {
        ResetPitchAndVolume();
        _audioSource.PlayOneShot(_heal);
    }

    public void SpeedBoostSound()
    {
        ResetPitchAndVolume();
        _audioSource.PlayOneShot(_speedBoost);
    }

    public void SpeedBoostRemovedSound()
    {
        ResetPitchAndVolume();
        _audioSource.PlayOneShot(_speedBoostRemoved);
    }

    public void DamageReductionSound()
    {
        ResetPitchAndVolume();
        _audioSource.PlayOneShot(_damageReduction);
    }

    public void DamageReductionRemovedSound()
    {
        ResetPitchAndVolume();
        _audioSource.PlayOneShot(_damageReductionRemoved);
    }

    public void StepSound()
    {
        _audioSource.pitch = NextFloat(0.8f, 1.2f);
        _audioSource.volume = NextFloat(0.8f, 1);
        _audioSource.PlayOneShot(_step);
    }

    public void TakeDamageSound()
    {
        ResetPitchAndVolume();
        switch (_random.Next(3))
        {
            case 0:
                _audioSource.PlayOneShot(_takeDamage1);
                break;
            case 1:
                _audioSource.PlayOneShot(_takeDamage2);
                break;
            case 2:
                _audioSource.PlayOneShot(_takeDamage3);
                break;
        }
    }

    public void DeathSound()
    {
        ResetPitchAndVolume();
        switch (_random.Next(3))
        {
            case 0:
                _audioSource.PlayOneShot(_death1);
                break;
            case 1:
                _audioSource.PlayOneShot(_death2);
                break;
            case 2:
                _audioSource.PlayOneShot(_death3);
                break;
        }
    }

    private float NextFloat(float min, float max)
    {
        double val = (_random.NextDouble() * (max - min) + min);
        return (float)val;
    }

    private void ResetPitchAndVolume()
    {
        _audioSource.pitch = 1f;
        _audioSource.volume = 1f;
    }
}
