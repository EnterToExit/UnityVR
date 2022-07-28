using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FootstepSFXTrigger : MonoBehaviour
{
    [Tooltip("Minimum position change required to play a footstep sound.")]
    [SerializeField] private float minimumDistance;
    private CharacterController _characterController;
    private PlayerSFXController _playerSFXController;
    private ActionBasedContinuousMoveProvider _moveProvider;
    private Vector3 _startingPos;
    private Vector3 _currentPos;
    private bool _startingPosMustReset = true;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _playerSFXController = GetComponent<PlayerSFXController>();
        _moveProvider = GameObject.FindWithTag("Locomotion System").GetComponent<ActionBasedContinuousMoveProvider>();
    }


    private void Update()
    {
        if (_characterController.isGrounded)
        {
            if (_startingPosMustReset)
            {
                _startingPos = gameObject.transform.position;
                _startingPosMustReset = false;
            }
            _currentPos = gameObject.transform.position;
            if (Vector3.Distance(_currentPos, _startingPos) >= minimumDistance)
            {
                _startingPosMustReset = true;
                _playerSFXController.StepSound();
            }
        }
    }
}
