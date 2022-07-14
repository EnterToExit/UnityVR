using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(XRGrabInteractable))]
public class New : MonoBehaviour
{
    public float damage = 21f;
    public float fireRate = 1f;
    public float force = 155f;
    public float range = 15f;
    public GameObject muzzleFlash;
    public Transform bulletSpawn;
    public AudioClip shotClip;
    public AudioSource _audioSource;
    public GameObject hitEffect;

    private float nextFire = 0f;

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
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + 1f / fireRate;
            Shoot();
        }
    }

    private void GunDectivated(DeactivateEventArgs activateEventArgs)
    {
        Debug.Log("Stop shoot");

    }

    void Shoot()
    {
        _audioSource.PlayOneShot(shotClip);
        if (muzzleFlash)
        {
            // Создание эффекта
            GameObject tempFlash;
            tempFlash = Instantiate(muzzleFlash, bulletSpawn.position, bulletSpawn.rotation);

            // Уничтожение эффекта
            Destroy(tempFlash, 2f);
        }

        RaycastHit hit;

        if (Physics.Raycast(bulletSpawn.transform.position, bulletSpawn.transform.forward, out hit, range))
        {
            GameObject impact = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impact, 2f);

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * force);
            }
        }
    }
}
