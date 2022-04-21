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



public class HeaderButtonEvents : MonoBehaviour
{
    private const string ANIMATOR_SHOW = "Show";
    private const string ANIMATOR_HIDE = "Hide";
    [SerializeField] private Animator _animator_header;
    [SerializeField] private Animator _animator_viewport;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnHideUI()
    {
        _animator_header.SetTrigger(ANIMATOR_HIDE);
        _animator_viewport.SetTrigger(ANIMATOR_HIDE);
    }

    public void OnShowUI()
    {
        _animator_header.SetTrigger(ANIMATOR_SHOW);
        _animator_viewport.SetTrigger(ANIMATOR_SHOW);
    }

    public void OnClose()
    {
        Application.Quit();
    }

    public void OnStart()
    {

    }

    public void OnStop()
    {

    }

    public void OnSetting()
    {

    }

    public void OnInputSet()
    {

    }

    
}
