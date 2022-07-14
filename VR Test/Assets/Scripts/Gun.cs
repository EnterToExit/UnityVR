using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(XRGrabInteractable))]

public class Gun : MonoBehaviour
{
    // --- Audio ---
    public AudioClip GunShotClip;
    public AudioClip reload;
    public AudioSource source;
    public Vector2 audioPitch = new Vector2(.9f, 1.1f);

    [Header("Prefab Refrences")]
    public GameObject bulletPrefab;
    public GameObject casingPrefab;
    public GameObject muzzleFlashPrefab;

    [Header("Location Refrences")]
    [SerializeField] private Animator gunAnimator;
    [SerializeField] private Transform barrelLocation;
    [SerializeField] private Transform casingExitLocation;

    [Header("Settings")]
    [Tooltip("Specify time to destory the casing object")] [SerializeField] private float destroyTimer = 2f;
    [Tooltip("Bullet Speed")] [SerializeField] private float shotPower = 500f;
    [Tooltip("Casing Ejection Speed")] [SerializeField] private float ejectPower = 150f;

    public int maxammo = 10;
    private int currentammo = -1;
    //public float reloadTime = 1f;
    //private bool isReloading = false;


    private XRGrabInteractable _grabInteractable;

    [SerializeField]
    private TextMeshPro textMeshPro;

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


        if (currentammo > 0)
        {
            GetComponent<Animator>().SetTrigger("Fire");
            maxammo = maxammo - 1;
            textMeshPro.text = maxammo.ToString();
            
        }



        /*
        if (Vector3.Angle(transform.up, Vector3.up) > 100 && currentammo < maxammo)
            Reload();
        */



        if (barrelLocation == null)
            barrelLocation = transform;
       

        if (gunAnimator == null)
            gunAnimator = GetComponentInChildren<Animator>();

        if (muzzleFlashPrefab)
        {
            // �������� �������
            GameObject tempFlash;
            tempFlash = Instantiate(muzzleFlashPrefab, barrelLocation.position, barrelLocation.rotation);

            // ����������� �������
            Destroy(tempFlash, destroyTimer);
        }

        // ����������, ���� ��� ������� ����
        if (!bulletPrefab)
        { return; }

        // ������ ���� � ��������� � ��� ���� � ����������� ������
        Instantiate(bulletPrefab, barrelLocation.position, barrelLocation.rotation).GetComponent<Rigidbody>().AddForce(barrelLocation.forward * shotPower);

        if (source != null)
        {

            if (source.transform.IsChildOf(transform))
            {
                source.Play();
            }
            else
            {
                AudioSource newAS = Instantiate(source);
                if ((newAS = Instantiate(source)) != null && newAS.outputAudioMixerGroup != null && newAS.outputAudioMixerGroup.audioMixer != null)
                {
                    newAS.outputAudioMixerGroup.audioMixer.SetFloat("Pitch", Random.Range(audioPitch.x, audioPitch.y));
                    newAS.pitch = Random.Range(audioPitch.x, audioPitch.y);

                    newAS.PlayOneShot(GunShotClip);

                    Destroy(newAS.gameObject, 4);
                }
            }
        }
        

    }

    

    private void GunDectivated(DeactivateEventArgs activateEventArgs)
    {
        Debug.Log("Stop shoot");
        
        // ����� ������ ����
        if (Input.GetButtonDown("Fire1"))
        {
            // �������� �������� �� ������, ������� ��������������� ������� ��������, ����� ������� ������
            gunAnimator.SetTrigger("Fire");
        }
        
    }
    /*
    void Reload()
    {
        isReloading = true;

        Debug.Log("Reloading");

        //yield return new WaitForSeconds(reloadTime);

        currentammo = maxammo;

        isReloading = false;
    }
    */
    // ��� ������� ������� ����� � ������� �������
    void CasingRelease()
    {
        // �������� �������, ���� ������� ��� ������� �� ����������� ��� ����������� ������ 
        if (!casingExitLocation || !casingPrefab)
        { return; }

        // �������� �������
        GameObject tempCasing;
        tempCasing = Instantiate(casingPrefab, casingExitLocation.position, casingExitLocation.rotation) as GameObject;
        // ������������ �������
        tempCasing.GetComponent<Rigidbody>().AddExplosionForce(Random.Range(ejectPower * 0.7f, ejectPower), (casingExitLocation.position - casingExitLocation.right * 0.3f - casingExitLocation.up * 0.6f), 1f);

        tempCasing.GetComponent<Rigidbody>().AddTorque(new Vector3(0, Random.Range(100f, 500f), Random.Range(100f, 1000f)), ForceMode.Impulse);

        // ���������� ����� ����� X ������
        Destroy(tempCasing, destroyTimer);
    }
}
