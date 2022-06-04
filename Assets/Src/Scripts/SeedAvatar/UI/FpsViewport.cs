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
    public float UpdateInterval = 0.5F;
    private float _lastInterval;
    private int _frameCount;
    private Text _fpsLabel;

    void Start() {
      _lastInterval = Time.realtimeSinceStartup;
      _frameCount = 0;
      _fpsLabel = GetComponent<Text>();
    }

    void Update() {
      ++_frameCount;
      if (Time.realtimeSinceStartup > _lastInterval + UpdateInterval) {
        float fps = _frameCount / (Time.realtimeSinceStartup - _lastInterval);
        _frameCount = 0;
        _lastInterval = Time.realtimeSinceStartup;
        _fpsLabel.text = string.Format("fps: {0:0.##}", fps);//#0.00
        _fpsLabel.color = fps >= HighLevel ? HighColor : (fps > LowLevel ? MiddleColor : LowColor);
      }
    }
  }
}
