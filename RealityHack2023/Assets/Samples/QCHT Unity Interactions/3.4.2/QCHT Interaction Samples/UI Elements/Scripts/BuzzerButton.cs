// /******************************************************************************
//  * File: BuzzerButton.cs
//  * Copyright (c) 2022 Qualcomm Technologies, Inc. and/or its subsidiaries. All rights reserved.
//  *
//  * Confidential and Proprietary - Qualcomm Technologies, Inc.
//  *
//  ******************************************************************************/

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using QCHT.Core;

namespace QCHT.Samples.UIElements
{
    public sealed class BuzzerButton : MonoBehaviour
    {
        [SerializeField] private ConfigurableJoint movablePart;
        [SerializeField] private float restYPosition;
        [SerializeField] private float pressedYPosition;
        [SerializeField] private float desactivationYPosition;

        [SerializeField] private List<Collider> otherColliders;
        
        [Header("Audio")]
        [SerializeField, CanBeEmpty] private AudioSource _audioSource;

        [Header("Debug")]
        [SerializeField] private KeyCode _keyCode;

        [Space]
        public UnityEvent OnButtonPressed = new UnityEvent();
        public UnityEvent OnButtonReleased = new UnityEvent();

        private bool _pressed;

        public float NormalizedPressedValue =>
            Mathf.Clamp01(movablePart.transform.localPosition.y / (pressedYPosition - restYPosition));

        private void Start()
        {
            if (!_audioSource)
                _audioSource = GetComponent<AudioSource>();

            SetupPosition();

            // Ignore collisions between movable part and other child colliders
            var col = movablePart.GetComponent<Collider>();
            foreach (var co in GetComponentsInChildren<Collider>())
                Physics.IgnoreCollision(col, co);
        }
        
        private void Update()
        {
            var position = movablePart.transform.localPosition;

            if (position.y < pressedYPosition)
            {
                position.y = pressedYPosition;

                if (!_pressed)
                {
                    if (_audioSource)
                        _audioSource.PlayOneShot(_audioSource.clip);

                    OnButtonPressed.Invoke();
                    _pressed = true;
                }
            }

            if (position.y >= desactivationYPosition)
            {
                if (_pressed)
                {
                    OnButtonReleased?.Invoke();
                }

                _pressed = false;
            }

            if (position.y > restYPosition)
            {
                position.y = restYPosition;
            }

            movablePart.transform.localPosition = position;

#if UNITY_EDITOR && ENABLE_LEGACY_INPUT_MANAGER
            if (Input.GetKeyDown(_keyCode))
            {
                OnButtonPressed.Invoke();
            }
#endif
        }

#if UNITY_EDITOR
        public void OnValidate()
        {
            SetupPosition();
        }
#endif
        
        private void SetupPosition()
        {
            if (!movablePart)
                return;

            var movablePartTransform = movablePart.transform;
            var position = movablePartTransform.localPosition;
            position.y = restYPosition;
            movablePartTransform.localPosition = position;
            movablePart.connectedAnchor = movablePartTransform.position;
        }
    }
}