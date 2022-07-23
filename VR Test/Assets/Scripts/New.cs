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
    public AudioClip reloadClip;
    public AudioSource _audioSource;

    public int maxammo = 40;
    public int currentammo;

    private XRGrabInteractable _grabInteractable;

    [SerializeField]
    public TextMeshPro ammoText;

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
        StartCoroutine(AutoShoot()); 
    }

    private IEnumerator AutoShoot()
    {
        while (true)
        {
            if (currentammo > 0)
            {
                Shoot();
                yield return new WaitForSeconds(0.3f);
                currentammo--;
                if (currentammo == 0)
                {
                    yield return new WaitForSeconds(0.1f);
                    _audioSource.PlayOneShot(reloadClip);
                    
                    currentammo = maxammo;
                }
            }
        }
    }
    
    private void GunDectivated(DeactivateEventArgs activateEventArgs)
    {
        Debug.Log("Stop shoot");
        StopAllCoroutines();

    }

    void Shoot()
    {
        ammoText.text = currentammo.ToString();
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

            if (hit.collider.gameObject.TryGetComponent<Health>(out var hp))
            {
                hp.TakeDamage(damage);
            }
            
        }
    }
}
