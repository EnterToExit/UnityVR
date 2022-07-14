using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
//using TMPro;

[RequireComponent(typeof(XRGrabInteractable))]
public class ShootGunAU : MonoBehaviour
{

    public float speed = 40;
    public GameObject bullet;
    public Transform barrel;
    public AudioSource audioSource;
    public AudioClip audioClip;

    public float rate = 1;

    private Coroutine _current;

    private XRGrabInteractable _grabInteractable;

    private void Awake()
    {
        _grabInteractable = GetComponent<XRGrabInteractable>();
    }

    private void OnEnable()
    {
        if (_grabInteractable == null)
            return;

        _grabInteractable.activated.AddListener(GunActivated);
        _grabInteractable.deactivated.AddListener(GunDectivated);
    }

    private void OnDisable()
    {
        if (_grabInteractable == null)
            return;

        _grabInteractable.activated.RemoveListener(GunActivated);
        _grabInteractable.deactivated.RemoveListener(GunDectivated);
    }

    private void GunActivated(ActivateEventArgs activateEventArgs)
    {
        Debug.Log("Start shoot");
        BeginFire();
        

    }

    private void GunDectivated(DeactivateEventArgs activateEventArgs)
    {
        Debug.Log("Stop shoot");

        StopFire();

    }

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

    public IEnumerator FireRoutine()
    {
        while (true)
        {
            GameObject spawnedBullet = Instantiate(bullet, barrel.position, barrel.rotation);
            spawnedBullet.GetComponent<Rigidbody>().AddForce(speed * barrel.forward);
            audioSource.PlayOneShot(audioClip);
            Destroy(spawnedBullet, 2);

            yield return new WaitForSeconds(1f / rate);
        }
    }
}