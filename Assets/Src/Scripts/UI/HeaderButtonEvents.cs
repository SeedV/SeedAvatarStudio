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

// This class is the UI header panel buttons click events script
public class HeaderButtonEvents : MonoBehaviour {
    
  //Animator trigger Param
  private const string _animatorShow = "Show";
  //Animator trigger Param
  private const string _animatorHide = "Hide";
  //Header panel animator
  [SerializeField] private Animator _animatorHeader;
  //viewport panel animator
  [SerializeField] private Animator _animatorViewport;

  //HideUI button click event 
  public void OnHideUI() {
    _animatorHeader.SetTrigger(_animatorHide);
    _animatorViewport.SetTrigger(_animatorHide);
  }

  //ExithideUI button click event
  public void OnShowUI() {
    _animatorHeader.SetTrigger(_animatorShow);
    _animatorViewport.SetTrigger(_animatorShow);
  }

  //Close app button click event
  public void OnClose() {
    Application.Quit();
  }

  //Play button click event
  public void OnPlay() {
  }

  //Stop button click event
  public void OnStop() {
  }

  //Setting button click event
  public void OnSetting() {
  }

  //InputSetting button click event
  public void OnInputSet() {
  }
}
