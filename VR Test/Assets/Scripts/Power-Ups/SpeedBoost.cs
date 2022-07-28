using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SpeedBoost : MonoBehaviour
{
    [SerializeField] private float duration;
    [SerializeField] private float speedMultiplier;
    private ActionBasedContinuousMoveProvider _moveProvider;
    private XRGrabInteractable _grabInteractable;
    private Rigidbody _rigidbody;
    private PlayerSFXController _playerSFXController;

    private void Awake()
    {
        _moveProvider = GameObject.FindWithTag("Locomotion System").GetComponent<ActionBasedContinuousMoveProvider>();
        _grabInteractable = GetComponent<XRGrabInteractable>();
        _rigidbody = GetComponent<Rigidbody>();
        _playerSFXController = GameObject.FindWithTag("Player").GetComponent<PlayerSFXController>();
    }

    private void OnEnable()
    {
        if (_grabInteractable == null) return;
        _grabInteractable.selectEntered.AddListener(Boost);
    }

    private void Boost(SelectEnterEventArgs selectEnterEventArgs)
    {
        // Prevents effect duplication by grabbing with 2 hands simultaneously
        _grabInteractable.selectEntered.RemoveListener(Boost);

        _moveProvider.moveSpeed *= speedMultiplier;
        _playerSFXController.SpeedBoostSound();

        // Disables physics and makes the object invisible so that it appears consumed
        gameObject.transform.localScale = new Vector3(0, 0, 0);
        _rigidbody.constraints = RigidbodyConstraints.FreezeAll;

        Invoke("RemoveBoost", duration);
    }

    private void RemoveBoost()
    {
        _moveProvider.moveSpeed /= speedMultiplier;
        _playerSFXController.SpeedBoostRemovedSound();
        Destroy(gameObject);
    }
}
