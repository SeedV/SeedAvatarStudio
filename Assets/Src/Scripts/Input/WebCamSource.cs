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

using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

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
  private List<string> _devicesName = new List<string>();
  private List<string> _resolutionOpt = new List<string>(){"1280X720","1600X900","1920X1080"};
  private List<string> _fpsOpt = new List<string>(){"30Hz","60Hz"};
  private List<string> _flipOpt = new List<string>(){"默认","水平翻转"};
  //Start button event
  public void Play() {
    if(_webCam == null) {
      _webCam = new WebCamTexture(DeviceName, _width, _height, Fps);
      _webCam.Play();
    }
  }
  //Stop button event
  public void Stop() {
    if(_webCam != null) {
      _webCam.Stop();
      _webCam = null;
    }
  }
  //DropdownChange event
  public void CameraDropdownChange(int value) {
    //Debug.Log(_devicesName.ToArray()[value]);
    if(_devicesName.ToArray()[value] != null) {
      DeviceName = _devicesName.ToArray()[value];
    }
  }
  //DropdownChange event
  public void ResolutionDropdownChange(int value) {
    string _resolution = _resolutionOpt.ToArray()[value];
    if(_resolution != null) {
      switch(_resolution) {
        case "1280X720":
          _width = 1280;
          _height = 720;
          break;
        case "1600X900":
          _width = 1600;
          _height = 900;
          break;
        case "1920X1080":
          _width = 1920;
          _height = 1080;
          break;
        default:
          break;
      }
      //Debug.Log(_width);
    }
  }
  //DropdownChange event
  public void FpsDropdownChange(int value) {
    string _fps = _fpsOpt.ToArray()[value];
    if(_fps != null) {
      switch(_fps) {
        case "60Hz":
          Fps = 60;
          break;
        case "30Hz":
          Fps = 30;
          break;
        default:
          break;
      }
      //Debug.Log(Fps);
    }
  }
  //DropdownChange event
  public void FlipDropdownChange(int value) {
    string _flip = _flipOpt.ToArray()[value];
    if(_flip != null) {
      switch(_flip) {
        case "默认":
          Flip = false;
          break;
        case "水平翻转":
          Flip = true;
          break;
        default:
          break;
      }
      //Debug.Log(Flip);
    }
  }
  //Webcam and params init
  IEnumerator Start() {
    yield return Application.RequestUserAuthorization (UserAuthorization.WebCam);
    if(Application.HasUserAuthorization(UserAuthorization.WebCam)) {
      WebCamDevice[] _devices = WebCamTexture.devices;
      CameraDropdown.ClearOptions();
      for(int i = 0; i< _devices.Length; i++) {
        _devicesName.Add(_devices[i].name);
      }
      CameraDropdown.AddOptions(_devicesName);
      CameraDropdown.value = 0;
      CameraDropdown.captionText.text = CameraDropdown.options[0].text;
      CameraDropdown.onValueChanged.AddListener(CameraDropdownChange);
    }

    ResolutionDropdown.ClearOptions();
    ResolutionDropdown.AddOptions(_resolutionOpt);
    ResolutionDropdown.value = 0;
    ResolutionDropdown.captionText.text = ResolutionDropdown.options[0].text;
    ResolutionDropdown.onValueChanged.AddListener(ResolutionDropdownChange);

    FpsDropdown.ClearOptions();
    FpsDropdown.AddOptions(_fpsOpt);
    FpsDropdown.value = 0;
    FpsDropdown.captionText.text = FpsDropdown.options[0].text;
    FpsDropdown.onValueChanged.AddListener(FpsDropdownChange);

    FlipDropdown.ClearOptions();
    FlipDropdown.AddOptions(_flipOpt);
    FlipDropdown.value = 0;
    FlipDropdown.captionText.text = FlipDropdown.options[0].text;
    FlipDropdown.onValueChanged.AddListener(FlipDropdownChange);
  }
  void Update() {
    if(_webCam != null) {
      if (!_webCam.didUpdateThisFrame) return;
      bool vFlip = _webCam.videoVerticallyMirrored;
      Vector2 scale = new Vector2(Flip ? -1 : 1, vFlip ? -1 : 1);
      Vector2 offset = new Vector2(Flip ? 1 : 0 / 2, vFlip ? 1 : 0);
      Graphics.Blit(_webCam, _buffer, scale, offset);
    }
  }
}
