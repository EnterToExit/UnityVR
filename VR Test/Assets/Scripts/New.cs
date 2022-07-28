using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections;
using TMPro;

[RequireComponent(typeof(XRGrabInteractable))]
public class New : MonoBehaviour
{
    // public float damage = 21f;
    // public float fireRate = 1f;
    // public float force = 155f;
    // public float range = 150f;
    public GameObject muzzleFlash;
    public Transform bulletSpawn;
    public AudioClip shotClip;
    public AudioClip reloadClip;
    public AudioSource _audioSource;
    public int maxammo = 40;
    public int currentammo;
    private XRGrabInteractable _grabInteractable;

    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _barrelLocation;
    private float _shotPower = 10f;


    [SerializeField] public TextMeshPro ammoText;

    private void Awake()
    {
        _grabInteractable = GetComponent<XRGrabInteractable>();
    }

    private void OnEnable()
    {
        if (_grabInteractable == null)
            return;

        _grabInteractable.activated.AddListener(GunActivated);
        _grabInteractable.deactivated.AddListener(GunDeactivated);
    }

    private void OnDisable()
    {
        if (_grabInteractable == null)
            return;

        _grabInteractable.activated.RemoveListener(GunActivated);
        _grabInteractable.deactivated.RemoveListener(GunDeactivated);
    }

    private void GunActivated(ActivateEventArgs activateEventArgs)
    {
        StartCoroutine(AutoShoot());
    }

    private IEnumerator AutoShoot()
    {
        while (true)
        {
            if (currentammo <= 0) continue;
            Shoot();
            currentammo--;
            yield return new WaitForSeconds(0.3f);
            if (currentammo == 0)
            {
                yield return new WaitForSeconds(0.1f);
                _audioSource.PlayOneShot(reloadClip);

                currentammo = maxammo;
            }
        }
    }

    private void GunDeactivated(DeactivateEventArgs activateEventArgs)
    {
        StopAllCoroutines();
    }

    private void Shoot()
    {
        ammoText.text = currentammo.ToString();
        _audioSource.PlayOneShot(shotClip);
        if (muzzleFlash)
        {
            // �������� �������
            GameObject tempFlash;
            tempFlash = Instantiate(muzzleFlash, bulletSpawn.position, bulletSpawn.rotation);

            // ����������� �������
            Destroy(tempFlash, 2f);
        }

        Instantiate(_bulletPrefab, _barrelLocation.position, _barrelLocation.rotation).GetComponent<Rigidbody>()
            .AddForce(_barrelLocation.forward * _shotPower, ForceMode.Impulse);

        // RaycastHit hit;
        // if (Physics.Raycast(bulletSpawn.transform.position, bulletSpawn.transform.forward, out hit, range))
        // {
        //     Debug.Log("Raycast");
        //     if (hit.collider.gameObject. == "Cube")
        //         Debug.Log("huibox");
        //
        //     if (hit.collider.gameObject.TryGetComponent<Health>(out var health))
        //     {
        //         Debug.Log("eblan");
        //         health.TakeDamage(damage);
        //     }
        // }
    }
}