// /******************************************************************************
//  * File: IndicatorLight.cs
//  * Copyright (c) 2022 Qualcomm Technologies, Inc. and/or its subsidiaries. All rights reserved.
//  *
//  * Confidential and Proprietary - Qualcomm Technologies, Inc.
//  *
//  ******************************************************************************/

using UnityEngine;

namespace QCHT.Samples.UIElements
{
    public class IndicatorLight : MonoBehaviour
    {
        [SerializeField] private bool _defaultStatus;
        [SerializeField] private GameObject indicatorOff;
        [SerializeField] private GameObject indicatorOn;

        private bool _isLightOn;

        public bool IsLightOn
        {
            set
            {
                _isLightOn = value;

                if (_isLightOn)
                {
                    indicatorOff.SetActive(false);
                    indicatorOn.SetActive(true);
                }
                else
                {
                    indicatorOff.SetActive(true);
                    indicatorOn.SetActive(false);
                }
            }
            get => _isLightOn;
        }

        private void Start()
        {
            IsLightOn = _defaultStatus;
        }

        private void OnValidate()
        {
            IsLightOn = _defaultStatus;
        }

        public void ToggleLight()
        {
            IsLightOn = !IsLightOn;
        }
    }
}