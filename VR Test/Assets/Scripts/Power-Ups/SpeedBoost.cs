using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SpeedBoost : MonoBehaviour
{
    [SerializeField] private float speedMultiplier;
    [SerializeField] private float duration;
    private ActionBasedContinuousMoveProvider _moveProvider;
    private XRGrabInteractable _grabInteractable;

    private void Awake()
    {
        _moveProvider = GameObject.Find("Locomotion System").GetComponent<ActionBasedContinuousMoveProvider>();
        _grabInteractable = GetComponent<XRGrabInteractable>();
    }

    private void OnEnable()
    {
        if (_grabInteractable == null) return;
        _grabInteractable.selectEntered.AddListener(Boost);
    }

    private void Boost(SelectEnterEventArgs selectEnterEventArgs)
    {
        _moveProvider.moveSpeed *= speedMultiplier;

        // Makes the object invisible so that it appears consumed
        gameObject.transform.localScale = new Vector3(0, 0, 0);

        Invoke("RemoveBoost", duration);
    }

    private void RemoveBoost()
    {
        _moveProvider.moveSpeed /= speedMultiplier;
        Destroy(gameObject);
    }
}
