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
using Debug = UnityEngine.Debug;

namespace SeedAvatar {
  public enum DropdownKey {
    CAMERA = 0,
    RESOLUTION = 1,
    FPS = 2,
    FLIP = 3
  }
  public enum ResolutionOptions {
    LOW = 0,
    MIDDLE = 1,
    HIGH = 2,
  }
  public enum FpsOptions {
    LOW = 0,
    HiGH = 1
  }
  public enum FlipOptions {
    NORMAL = 0,
    MIRRORING = 1
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
    private static readonly Dictionary<ResolutionOptions, string> _resolutionDic =
    new Dictionary<ResolutionOptions, string>
    {
      { ResolutionOptions.LOW , "1280x720" } , {  ResolutionOptions.MIDDLE, "1600x900" } , { ResolutionOptions.HIGH , "1920x1080" }
    };
    private static readonly Dictionary<FpsOptions, string> _fpsDic =
    new Dictionary<FpsOptions, string>
    {
      { FpsOptions.LOW , "30Hz" } , { FpsOptions.HiGH , "60Hz" }
    };
    private static readonly Dictionary<FlipOptions, string> _flipDic =
    new Dictionary<FlipOptions, string>
    {
      { FlipOptions.NORMAL , "Normal" } , { FlipOptions.MIRRORING , "Mirroring" }
    };
    private List<string> _devicesName = new List<string>();
    private List<string> _resolutionOpt = new List<string>();
    private List<string> _fpsOpt = new List<string>();
    private List<string> _flipOpt = new List<string>();

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
    public void CameraDropdownChange(int value) {
      //Debug.Log(_devicesName.ToArray()[value]);
      if (_devicesName.ToArray()[value] != null) {
        DeviceName = _devicesName.ToArray()[value];
      }
    }

    // DropdownChange event.
    public void ResolutionDropdownChange(int value) {
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
    public void FpsDropdownChange(int value) {
      switch (value) {
        case (int)FpsOptions.LOW:
          Fps = 60;
          break;
        case (int)FpsOptions.HiGH:
          Fps = 30;
          break;
        default:
          break;
      }
    }

    // DropdownChange event.
    public void FlipDropdownChange(int value) {
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

    private void InitDropdown(Dropdown target, List<string> optarr, DropdownKey dropdownkey) {
      target.ClearOptions();
      switch (dropdownkey) {
        case DropdownKey.CAMERA:
          target.AddOptions(optarr);
          target.onValueChanged.AddListener(CameraDropdownChange);
          break;
        case DropdownKey.RESOLUTION:
          foreach (ResolutionOptions opt in Enum.GetValues(typeof(ResolutionOptions))) {
            optarr.Add(_resolutionDic[opt]);
          }
          target.AddOptions(optarr);
          target.onValueChanged.AddListener(ResolutionDropdownChange);
          break;
        case DropdownKey.FPS:
          foreach (FpsOptions opt in Enum.GetValues(typeof(FpsOptions))) {
            optarr.Add(_fpsDic[opt]);
          }
          target.AddOptions(optarr);
          target.onValueChanged.AddListener(FpsDropdownChange);
          break;
        case DropdownKey.FLIP:
          foreach (FlipOptions opt in Enum.GetValues(typeof(FlipOptions))) {
            optarr.Add(_flipDic[opt]);
          }
          target.AddOptions(optarr);
          target.onValueChanged.AddListener(FlipDropdownChange);
          break;
        default:
          break;
      }
      target.value = 0;
      target.captionText.text = target.options[0].text;
    }

    // Webcam params init.
    IEnumerator Start() {
      yield return Application.RequestUserAuthorization(UserAuthorization.WebCam);
      if (Application.HasUserAuthorization(UserAuthorization.WebCam)) {
        WebCamDevice[] _devices = WebCamTexture.devices;
        foreach (var device in _devices) {
          _devicesName.Add(device.name);
        }
        InitDropdown(CameraDropdown, _devicesName, DropdownKey.CAMERA);
      }
      InitDropdown(ResolutionDropdown, _resolutionOpt, DropdownKey.RESOLUTION);
      InitDropdown(FpsDropdown, _fpsOpt, DropdownKey.FPS);
      InitDropdown(FlipDropdown, _flipOpt, DropdownKey.FLIP);
    }

    void Update() {
      if (_webCam != null) {
        if (!_webCam.didUpdateThisFrame) return;
        bool vFlip = _webCam.videoVerticallyMirrored;
        Vector2 scale = new Vector2(Flip ? -1 : 1, vFlip ? -1 : 1);
        Vector2 offset = new Vector2(Flip ? 1 : 0, vFlip ? 1 : 0);
        Graphics.Blit(_webCam, _buffer, scale, offset);
      }
    }
  }
}
