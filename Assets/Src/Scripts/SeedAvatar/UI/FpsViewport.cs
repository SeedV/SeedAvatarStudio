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

namespace SeedAvatar {
  public class FpsViewport : MonoBehaviour {
    public int HighLevel = 20;
    public int LowLevel = 15;
    public Color HighColor = Color.green;
    public Color MiddleColor = Color.yellow;
    public Color LowColor = Color.red;
    private int _frameCount = 0;
    private Text _fpsLabel;

    void Start() {
      _fpsLabel = GetComponent<Text>();
      StartCoroutine(Loop());
    }

    void Update() {
      ++_frameCount;
    }

    private IEnumerator Loop() {
      while (true) {
        yield return new WaitForSeconds(1);
        int fps = _frameCount;
        _fpsLabel.text = $"fps: {fps}";
        _fpsLabel.color = fps >= HighLevel ? HighColor : (fps > LowLevel ? MiddleColor : LowColor);
        _frameCount = 0;
      }
    }
  }
}
