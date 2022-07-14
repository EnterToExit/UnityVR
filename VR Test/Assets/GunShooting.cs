using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GunShooting : MonoBehaviour
{
    public float speed = 40;
    public GameObject bullet;
    public Transform barrel;
    public AudioSource audioSource;
    public AudioClip audioClip;

    // configure shots per second
    public float rate = 1;

    private Coroutine _current;

    public void BeginFire()
    {
        if (_current != null) 
            StopCoroutine(_current);

        _current = StartCoroutine(FireRoutine());
    }

    public void StopFire()
    {
        if (_current != null) 
            StopCoroutine(_current);
    }

    private IEnumerator FireRoutine()
    {
        while (true)
        {
            GameObject spawnedBullet = Instantiate(bullet, barrel.position, barrel.rotation);

            if (spawnedBullet != null)
            {
                Debug.Log("Создался объект " + spawnedBullet.name);
            }
            else { 
                Debug.LogError("Нечего создавать. Пустая ссылка!"); 
            }

            spawnedBullet.GetComponent<Rigidbody>().AddForce(speed * barrel.forward);
            

            audioSource.PlayOneShot(audioClip);
            Destroy(spawnedBullet, 2);

            yield return new WaitForSeconds(1f / rate);
        }
    }
}
