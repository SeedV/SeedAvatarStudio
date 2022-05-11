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
  public string DeviceName;
  public int Fps = 30;
  public RawImage OutScreen;
  private WebCamTexture _webCam;
  private int _width = 1280;
  private int _height = 720;
  private List<string> _devicesName = new List<string>();

  public void Play() {
    if(_webCam == null) {
      _webCam = new WebCamTexture(DeviceName, _width, _height, Fps);
      _webCam.Play();
    }
  }
  public void Stop() {
    if(_webCam != null) {
      _webCam.Stop();
      _webCam = null;
    }
  }
  public void CameraDropdownChange(int value) {
    //Debug.Log(_devicesName.ToArray()[value]);
    if(_devicesName.ToArray()[value] != null) {
      DeviceName = _devicesName.ToArray()[value];
    }
  }
  IEnumerator Start() {
    yield return Application.RequestUserAuthorization (UserAuthorization.WebCam);
    if(Application.HasUserAuthorization(UserAuthorization.WebCam)) {
      WebCamDevice[] devices = WebCamTexture.devices;
      CameraDropdown.ClearOptions();
      for(int i = 0; i< devices.Length; i++) {
        _devicesName.Add(devices[i].name);
      }
      CameraDropdown.AddOptions(_devicesName);
      CameraDropdown.value = 0;
      CameraDropdown.captionText.text = CameraDropdown.options[0].text;
      CameraDropdown.onValueChanged.AddListener(CameraDropdownChange);
    }
  }
  void Update() {
    if(_webCam != null) {
      OutScreen.texture = _webCam;
    }
  }
}
