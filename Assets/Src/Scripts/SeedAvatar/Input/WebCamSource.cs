// Copyright 2021-2022 The SeedV Lab.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Debug = UnityEngine.Debug;

namespace SeedAvatar {
  public enum ResolutionOptions {
    LOW = 0,
    MIDDLE = 1,
    HIGH = 2,
  }

  public enum FpsOptions {
    LOW = 0,
    HIGH = 1,
  }

  public enum FlipOptions {
    NORMAL = 0,
    MIRRORING = 1,
  }

  public class WebCamSource : MonoBehaviour {
    public Dropdown CameraDropdown;
    public Dropdown ResolutionDropdown;
    public Dropdown FpsDropdown;
    public Dropdown FlipDropdown;
    public string DeviceName;
    public int Fps = 30;
    public bool Flip = false;
    [SerializeField] private RenderTexture _buffer;
    private WebCamTexture _webCam;
    private int _width = 1280;
    private int _height = 720;
    private static readonly SortedDictionary<int, string> _resolutionDict =
    new SortedDictionary<int, string> {
      { (int)ResolutionOptions.LOW , "1280x720" },
      { (int)ResolutionOptions.MIDDLE, "1600x900" },
      { (int)ResolutionOptions.HIGH , "1920x1080" },
    };
    private static readonly SortedDictionary<int, string> _fpsDict =
    new SortedDictionary<int, string> {
      { (int)FpsOptions.LOW , "30Hz" },
      { (int)FpsOptions.HIGH , "60Hz" },
    };
    private static readonly SortedDictionary<int, string> _flipDict =
    new SortedDictionary<int, string> {
      { (int)FlipOptions.NORMAL , "Normal" },
      { (int)FlipOptions.MIRRORING , "Mirroring" },
    };
    private SortedDictionary<int, string> _devicesDict = new SortedDictionary<int, string>();

    // Start button event.
    public void Play() {
      if (_webCam == null) {
        _webCam = new WebCamTexture(DeviceName, _width, _height, Fps);
        _webCam.Play();
      }
    }

    // Stop button event.
    public void Stop() {
      if (_webCam != null) {
        _webCam.Stop();
        _webCam = null;
      }
    }

    // DropdownChange event.
    public void OnCameraDropdownChange(int value) {
      if (_devicesDict.ContainsKey(value)) {
        DeviceName = _devicesDict[value];
      }
    }

    // DropdownChange event.
    public void OnResolutionDropdownChange(int value) {
      switch (value) {
        case (int)ResolutionOptions.LOW:
          _width = 1280;
          _height = 720;
          break;
        case (int)ResolutionOptions.MIDDLE:
          _width = 1600;
          _height = 900;
          break;
        case (int)ResolutionOptions.HIGH:
          _width = 1920;
          _height = 1080;
          break;
        default:
          break;
      }
    }

    // DropdownChange event.
    public void OnFpsDropdownChange(int value) {
      switch (value) {
        case (int)FpsOptions.LOW:
          Fps = 60;
          break;
        case (int)FpsOptions.HIGH:
          Fps = 30;
          break;
        default:
          break;
      }
    }

    // DropdownChange event.
    public void OnFlipDropdownChange(int value) {
      switch (value) {
        case (int)FlipOptions.NORMAL:
          Flip = false;
          break;
        case (int)FlipOptions.MIRRORING:
          Flip = true;
          break;
        default:
          break;
      }
    }

    private void InitDropdown(Dropdown target, SortedDictionary<int, string> optDict, UnityAction<int> onChangedCallback) {
      target.ClearOptions();
      List<string> optStrings = optDict.Count == 0 ? new List<string>() : new List<string>(optDict.Values);
      target.AddOptions(optStrings);
      target.value = 0;
      target.captionText.text = target.options[0].text;
      target.onValueChanged.AddListener(onChangedCallback);
    }

    // Webcam params init.
    IEnumerator Start() {
      yield return Application.RequestUserAuthorization(UserAuthorization.WebCam);
      if (Application.HasUserAuthorization(UserAuthorization.WebCam)) {
        WebCamDevice[] devices = WebCamTexture.devices;
        int dictKey = 0;
        _devicesDict.Clear();
        foreach (var device in devices) {
          _devicesDict.Add(dictKey, device.name);
          dictKey++;
        }
        InitDropdown(CameraDropdown, _devicesDict, OnCameraDropdownChange);
      }
      InitDropdown(ResolutionDropdown, _resolutionDict, OnResolutionDropdownChange);
      InitDropdown(FpsDropdown, _fpsDict, OnFpsDropdownChange);
      InitDropdown(FlipDropdown, _flipDict, OnFlipDropdownChange);
    }

    void Update() {
      if (_webCam != null) {
        if (!_webCam.didUpdateThisFrame) return;
        bool vFlip = _webCam.videoVerticallyMirrored;
        float ratio = 1920 / _width;
        var scale = new Vector2(Flip ? -ratio : ratio, vFlip ? -ratio : ratio);
        var offset = new Vector2(Flip ? 1 : 0, vFlip ? 1 : 0);
        Graphics.Blit(_webCam, _buffer, scale, offset);
      }
    }
  }
}
