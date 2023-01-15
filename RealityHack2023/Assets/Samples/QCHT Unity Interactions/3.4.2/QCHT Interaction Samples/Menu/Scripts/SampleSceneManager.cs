// /******************************************************************************
//  * File: SampleSceneManager.cs
//  * Copyright (c) 2022 Qualcomm Technologies, Inc. and/or its subsidiaries. All rights reserved.
//  *
//  * Confidential and Proprietary - Qualcomm Technologies, Inc.
//  *
//  ******************************************************************************/

using QCHT.Interactions.Hands;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace QCHT.Samples.Menu
{
    public class SampleSceneManager : MonoBehaviour
    {
        [SerializeField]
        private SampleSettings startSample;

        [SerializeField]
        private HandPresenter leftHandPresenter;

        [SerializeField]
        private HandPresenter rightHandPresenter;

        private SampleSettings _currentSampleToLoad;
        private SampleSettings _currentSample;
        private Scene _currentScene;

        public void Start()
        {
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            if (startSample)
                LoadSample(startSample);
        }

        public void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        public void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        public void Quit()
        {
            Application.Quit();
        }

        #region Sample Loading

        /// <summary>
        /// Loads a sample scene and unload the current sample scene if exists.
        /// </summary>
        public void LoadSample(SampleSettings sample)
        {
            if (_currentSampleToLoad || sample.SceneName.Equals(_currentScene.name))
                return;

            // Unload current scene if exists
            if (_currentScene.IsValid() && _currentScene.isLoaded)
            {
                SceneManager.UnloadSceneAsync(_currentScene);
                _currentSample = null;
            }

            // Load the new sample scene by name
            _currentSampleToLoad = sample;
            SceneManager.LoadScene(sample.SceneName, LoadSceneMode.Additive);
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode)
        {
            if (!_currentSampleToLoad || scene.name != _currentSampleToLoad.SceneName)
                return;

            _currentScene = scene;
            _currentSample = _currentSampleToLoad;
            _currentSampleToLoad = null;

            // Sets the current sample scene as active once loaded
            SceneManager.SetActiveScene(_currentScene);

            leftHandPresenter.EnablePhysicsRaycast = _currentSample.EnablePhysicRaycast;
            rightHandPresenter.EnablePhysicsRaycast = _currentSample.EnablePhysicRaycast;
            
            leftHandPresenter.SetupHandType(_currentSample.HandType);
            rightHandPresenter.SetupHandType(_currentSample.HandType);
        }

        #endregion
    }
}