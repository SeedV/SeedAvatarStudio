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


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WebCamSource : MonoBehaviour {
  public int Width = 1200;
  public int Height = 720;
  public int Fps = 30;
  public RawImage OutScreen;
  private WebCamTexture _webCam;

  IEnumerator Start() {
    yield return Application.RequestUserAuthorization (UserAuthorization.WebCam);
    if(Application.HasUserAuthorization(UserAuthorization.WebCam)) {
      WebCamDevice[] devices = WebCamTexture.devices;
      _webCam = new WebCamTexture(devices[2].name, Width, Height, Fps);
      _webCam.Play();
    }
  }
  void Update() {
    OutScreen.texture = _webCam;
  }
}
