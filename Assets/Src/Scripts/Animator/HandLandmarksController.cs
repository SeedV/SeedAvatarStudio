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
using Color = UnityEngine.Color;

using Mediapipe;
using Mediapipe.Unity;

namespace SeedUnityVRKit {
  public enum HandType { LeftHand, RightHand }
  public class HandLandmarksController : MonoBehaviour {
    [Tooltip("Set it to the character's animator game object.")]
    public Animator anim;
    [Tooltip("Left hand or right hand.")]
    public HandType handType;
    public int ScreenWidth;
    public int ScreenHeight;
    public NormalizedLandmarkList HandLandmarkList { private get; set; }
    public Quaternion InitWristRotation = Quaternion.Euler(-90, 90, 0);
    public Quaternion InitFingerRotation = Quaternion.Euler(90, 0, 0);

    // Total number of landmarks in HandPose model, per hand.
    private const int _landmarksNum = 21;
    // The thumb length of the model. To apply to a new model this value should be tweak.
    private const float _modelThumbLength = 0.02f;
    // The hand root to keep track of the position and rotation.
    private Transform _target;

    private GameObject[] _handLandmarks = new GameObject[_landmarksNum];
    private float _screenRatio = 1.0f;
    private Transform[] _fingerTargets = new Transform[15];
    private Vector3[] _fingerDirections = new Vector3[15];

    void Start() {
      // Note: HandPose use camera perspective to determine left and right hand, which is mirrored
      // from the animator's perspective.
      var bone =
          (handType == HandType.LeftHand) ? HumanBodyBones.RightHand : HumanBodyBones.LeftHand;
      _target = anim.GetBoneTransform(bone);

      for (int i = 0; i < _landmarksNum; i++) {
        _handLandmarks[i] = new GameObject($"HandLandmark{i}");
        _handLandmarks[i].transform.parent = transform;
      }
      _screenRatio = 1.0f * ScreenWidth / ScreenHeight;

      if (handType == HandType.LeftHand) {
        _fingerTargets[0] = anim.GetBoneTransform(HumanBodyBones.RightThumbProximal);
        _fingerTargets[1] = anim.GetBoneTransform(HumanBodyBones.RightThumbIntermediate);
        _fingerTargets[2] = anim.GetBoneTransform(HumanBodyBones.RightThumbDistal);
        _fingerTargets[3] = anim.GetBoneTransform(HumanBodyBones.RightIndexProximal);
        _fingerTargets[4] = anim.GetBoneTransform(HumanBodyBones.RightIndexIntermediate);
        _fingerTargets[5] = anim.GetBoneTransform(HumanBodyBones.RightIndexDistal);
        _fingerTargets[6] = anim.GetBoneTransform(HumanBodyBones.RightMiddleProximal);
        _fingerTargets[7] = anim.GetBoneTransform(HumanBodyBones.RightMiddleIntermediate);
        _fingerTargets[8] = anim.GetBoneTransform(HumanBodyBones.RightMiddleDistal);
        _fingerTargets[9] = anim.GetBoneTransform(HumanBodyBones.RightRingProximal);
        _fingerTargets[10] = anim.GetBoneTransform(HumanBodyBones.RightRingIntermediate);
        _fingerTargets[11] = anim.GetBoneTransform(HumanBodyBones.RightRingDistal);
        _fingerTargets[12] = anim.GetBoneTransform(HumanBodyBones.RightLittleProximal);
        _fingerTargets[13] = anim.GetBoneTransform(HumanBodyBones.RightLittleIntermediate);
        _fingerTargets[14] = anim.GetBoneTransform(HumanBodyBones.RightLittleDistal);
      } else {
        _fingerTargets[0] = anim.GetBoneTransform(HumanBodyBones.LeftThumbProximal);
        _fingerTargets[1] = anim.GetBoneTransform(HumanBodyBones.LeftThumbIntermediate);
        _fingerTargets[2] = anim.GetBoneTransform(HumanBodyBones.LeftThumbDistal);
        _fingerTargets[3] = anim.GetBoneTransform(HumanBodyBones.LeftIndexProximal);
        _fingerTargets[4] = anim.GetBoneTransform(HumanBodyBones.LeftIndexIntermediate);
        _fingerTargets[5] = anim.GetBoneTransform(HumanBodyBones.LeftIndexDistal);
        _fingerTargets[6] = anim.GetBoneTransform(HumanBodyBones.LeftMiddleProximal);
        _fingerTargets[7] = anim.GetBoneTransform(HumanBodyBones.LeftMiddleIntermediate);
        _fingerTargets[8] = anim.GetBoneTransform(HumanBodyBones.LeftMiddleDistal);
        _fingerTargets[9] = anim.GetBoneTransform(HumanBodyBones.LeftRingProximal);
        _fingerTargets[10] = anim.GetBoneTransform(HumanBodyBones.LeftRingIntermediate);
        _fingerTargets[11] = anim.GetBoneTransform(HumanBodyBones.LeftRingDistal);
        _fingerTargets[12] = anim.GetBoneTransform(HumanBodyBones.LeftLittleProximal);
        _fingerTargets[13] = anim.GetBoneTransform(HumanBodyBones.LeftLittleIntermediate);
        _fingerTargets[14] = anim.GetBoneTransform(HumanBodyBones.LeftLittleDistal);
      }
    }

    async void Update() {
      if (HandLandmarkList != null) {
        transform.position = _target.transform.position;
        _target.rotation = ComputeWristRotation() * InitWristRotation;
        NormalizedLandmark landmark0 = HandLandmarkList.Landmark[0];
        NormalizedLandmark landmark1 = HandLandmarkList.Landmark[1];
        var s = _modelThumbLength / (ToVector(landmark1) - ToVector(landmark0)).magnitude;
        var scale = new Vector3(s * _screenRatio, s, s * _screenRatio);
        for (int i = 1; i < HandLandmarkList.Landmark.Count; i++) {
          NormalizedLandmark landmark = HandLandmarkList.Landmark[i];
          Vector3 tip = Vector3.Scale(ToVector(landmark) - ToVector(landmark0), scale);
          _handLandmarks[i].transform.localPosition = tip;
        }

        _fingerDirections[0] = _handLandmarks[2].transform.position - _handLandmarks[1].transform.position;
        _fingerTargets[0].rotation = Quaternion.LookRotation(new Vector3(-_fingerDirections[0].x, _fingerDirections[0].y, -_fingerDirections[0].z));
        _fingerDirections[1] = _handLandmarks[3].transform.position - _handLandmarks[2].transform.position;
        _fingerTargets[1].rotation = Quaternion.LookRotation(new Vector3(-_fingerDirections[1].x, _fingerDirections[1].y, -_fingerDirections[1].z));
        _fingerDirections[2] = _handLandmarks[4].transform.position - _handLandmarks[3].transform.position;
        _fingerTargets[2].rotation = Quaternion.LookRotation(new Vector3(-_fingerDirections[2].x, _fingerDirections[2].y, -_fingerDirections[2].z));

        _fingerDirections[3] = _handLandmarks[6].transform.position - _handLandmarks[5].transform.position;
        _fingerTargets[3].rotation = Quaternion.LookRotation(new Vector3(_fingerDirections[3].x, _fingerDirections[3].y, _fingerDirections[3].z)) * InitFingerRotation;
        _fingerDirections[4] = _handLandmarks[7].transform.position - _handLandmarks[6].transform.position;
        _fingerTargets[4].rotation = Quaternion.LookRotation(new Vector3(_fingerDirections[4].x, _fingerDirections[4].y, _fingerDirections[4].z)) * InitFingerRotation;
        _fingerDirections[5] = _handLandmarks[8].transform.position - _handLandmarks[7].transform.position;
        _fingerTargets[5].rotation = Quaternion.LookRotation(new Vector3(_fingerDirections[5].x, _fingerDirections[5].y, _fingerDirections[5].z)) * InitFingerRotation;

        _fingerDirections[6] = _handLandmarks[10].transform.position - _handLandmarks[9].transform.position;
        _fingerTargets[6].rotation = Quaternion.LookRotation(new Vector3(_fingerDirections[6].x, _fingerDirections[6].y, _fingerDirections[6].z)) * InitFingerRotation;
        _fingerDirections[7] = _handLandmarks[11].transform.position - _handLandmarks[10].transform.position;
        _fingerTargets[7].rotation = Quaternion.LookRotation(new Vector3(_fingerDirections[7].x, _fingerDirections[7].y, _fingerDirections[7].z)) * InitFingerRotation;
        _fingerDirections[8] = _handLandmarks[12].transform.position - _handLandmarks[11].transform.position;
        _fingerTargets[8].rotation = Quaternion.LookRotation(new Vector3(_fingerDirections[8].x, _fingerDirections[8].y, _fingerDirections[8].z)) * InitFingerRotation;

        _fingerDirections[9] = _handLandmarks[14].transform.position - _handLandmarks[13].transform.position;
        _fingerTargets[9].rotation = Quaternion.LookRotation(new Vector3(_fingerDirections[9].x, _fingerDirections[9].y, _fingerDirections[9].z)) * InitFingerRotation;
        _fingerDirections[10] = _handLandmarks[15].transform.position - _handLandmarks[14].transform.position;
        _fingerTargets[10].rotation = Quaternion.LookRotation(new Vector3(_fingerDirections[10].x, _fingerDirections[10].y, _fingerDirections[10].z)) * InitFingerRotation;
        _fingerDirections[11] = _handLandmarks[16].transform.position - _handLandmarks[15].transform.position;
        _fingerTargets[11].rotation = Quaternion.LookRotation(new Vector3(_fingerDirections[11].x, _fingerDirections[11].y, _fingerDirections[11].z)) * InitFingerRotation;

        _fingerDirections[12] = _handLandmarks[18].transform.position - _handLandmarks[17].transform.position;
        _fingerTargets[12].rotation = Quaternion.LookRotation(new Vector3(_fingerDirections[12].x, _fingerDirections[12].y, _fingerDirections[12].z)) * InitFingerRotation;
        _fingerDirections[13] = _handLandmarks[19].transform.position - _handLandmarks[18].transform.position;
        _fingerTargets[13].rotation = Quaternion.LookRotation(new Vector3(_fingerDirections[13].x, _fingerDirections[13].y, _fingerDirections[13].z)) * InitFingerRotation;
        _fingerDirections[14] = _handLandmarks[20].transform.position - _handLandmarks[19].transform.position;
        _fingerTargets[14].rotation = Quaternion.LookRotation(new Vector3(_fingerDirections[14].x, _fingerDirections[14].y, _fingerDirections[14].z)) * InitFingerRotation;

      }
    }

    //void OnAnimatorIK(int layerIndex) {
      // if (handType == HandType.LeftHand) {
      //   anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
      //   anim.SetIKPosition(AvatarIKGoal.LeftHand, _handLandmarks[1].transform.position);
      // } else {
      //   anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
      //   anim.SetIKPosition(AvatarIKGoal.RightHand, _handLandmarks[1].transform.position);
      // }
    //}

    void OnDrawGizmos() {
      if (_target != null) {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(_target.position, 0.005f);
        Gizmos.color = Color.yellow;
        Vector3 direction = _target.TransformDirection(Vector3.forward) * 1;
        Gizmos.DrawRay(_target.position, direction);
      }
      Gizmos.color = Color.red;
      foreach (var handLandmark in _handLandmarks) {
        if (handLandmark != null)
          Gizmos.DrawSphere(handLandmark.transform.position, 0.005f);
      }
    }

    private Vector3 ToVector(NormalizedLandmark landmark) {
      return new Vector3(landmark.X, landmark.Y, landmark.Z);
    }

    private Quaternion ComputeWristRotation() {
      var wristTransform = transform;
      var indexFinger = _handLandmarks[5].transform.position;
      var middleFinger = _handLandmarks[9].transform.position;

      var vectorToMiddle = middleFinger - wristTransform.position;
      var vectorToIndex = indexFinger - wristTransform.position;
      Vector3.OrthoNormalize(ref vectorToMiddle, ref vectorToIndex);
      Vector3 normalVector = Vector3.Cross(vectorToIndex, vectorToMiddle);
      return Quaternion.LookRotation(normalVector, vectorToIndex);
    }
  }
}
