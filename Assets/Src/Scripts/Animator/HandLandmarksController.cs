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
    private KalmanFilter[] _kalmanFilters = new KalmanFilter[_landmarksNum];
    private Quaternion _wristRotation = Quaternion.Euler(0, 0, 0);
    private Vector3 _forwardVector;
    private Vector3 _directVector;

    void Start() {
      // Note: HandPose use camera perspective to determine left and right hand, which is mirrored
      // from the animator's perspective.
      var bone =
          (handType == HandType.LeftHand) ? HumanBodyBones.RightHand : HumanBodyBones.LeftHand;
      _target = anim.GetBoneTransform(bone);

      for (int i = 0; i < _landmarksNum; i++) {
        _handLandmarks[i] = new GameObject($"HandLandmark{i}");
        _handLandmarks[i].transform.parent = transform;
        _kalmanFilters[i] = new KalmanFilter(0.125f, 1f);
      }
      _screenRatio = 1.0f * ScreenWidth / ScreenHeight;
      // The output of HandPose detection is mirrored here
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

    void Update() {
      if (HandLandmarkList != null) {
        NormalizedLandmark landmark0 = HandLandmarkList.Landmark[0];
        NormalizedLandmark landmark1 = HandLandmarkList.Landmark[1];
        var s = _modelThumbLength / (ToVector(landmark1) - ToVector(landmark0)).magnitude;
        var scale = new Vector3(s * _screenRatio, s, s * _screenRatio);
        for (int i = 1; i < HandLandmarkList.Landmark.Count; i++) {
          NormalizedLandmark landmark = HandLandmarkList.Landmark[i];
          Vector3 tip = Vector3.Scale(ToVector(landmark) - ToVector(landmark0), scale);
          _handLandmarks[i].transform.localPosition = _kalmanFilters[i].Update(tip);
        }
        ComputeWristRotation();
        if (handType == HandType.LeftHand &&
                Vector3.Angle(_forwardVector.normalized, Vector3.forward) < 90.0f ||
            handType == HandType.RightHand &&
                Vector3.Angle(_forwardVector.normalized, Vector3.forward) > 90.0f) {
          transform.position = _target.transform.position;
          _target.rotation = _wristRotation * InitWristRotation;
          ComputeFingerRotation();
        }
      }
    }

    void OnDrawGizmos() {
      if (_target != null) {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(_target.position, 0.005f);
        Gizmos.color = Color.yellow;
        Vector3 direction = _target.TransformDirection(Vector3.forward) * 1;
        Gizmos.DrawRay(_target.position, direction);

        Gizmos.color = Color.green;
        var wristTransform = transform;
        var indexFinger = _handLandmarks[5].transform.position;
        var middleFinger = _handLandmarks[9].transform.position;

        var vectorToMiddle = middleFinger - wristTransform.position;
        var vectorToIndex = handType == HandType.LeftHand ? wristTransform.position - indexFinger
                                                          : indexFinger - wristTransform.position;
        Vector3.OrthoNormalize(ref vectorToMiddle, ref vectorToIndex);
        Vector3 normalVector = Vector3.Cross(vectorToIndex, vectorToMiddle);
        Gizmos.DrawRay(_target.position, normalVector);
      }
      Gizmos.color = Color.red;
      foreach (var handLandmark in _handLandmarks) {
        if (handLandmark != null)
          Gizmos.DrawSphere(handLandmark.transform.position, 0.005f);
      }
    }

    private void ComputeFingerRotation() {
      var rotationTable = new(int fingerId, int landmarkId1, int landmarkId2)[] {
        (0, 1, 2),    (1, 2, 3),    (2, 3, 4),    (3, 5, 6),    (4, 6, 7),
        (5, 7, 8),    (6, 9, 10),   (7, 10, 11),  (8, 11, 12),  (9, 13, 14),
        (10, 14, 15), (11, 15, 16), (12, 17, 18), (13, 18, 19), (14, 19, 20)
      };
      foreach (var (fingerId, landmarkId1, landmarkId2) in rotationTable) {
        _fingerTargets[fingerId].rotation =
            Quaternion.LookRotation(
                ReflectIKFingerPosition(_handLandmarks[landmarkId2].transform.position) -
                    ReflectIKFingerPosition(_handLandmarks[landmarkId1].transform.position),
                _directVector) *
            InitFingerRotation;
      }
    }

    private Vector3 ReflectIKFingerPosition(Vector3 fingerPosition) {
      var wristTransform = transform;
      var indexFinger = _handLandmarks[5].transform.position;
      var middleFinger = _handLandmarks[9].transform.position;
      var targetFinger = fingerPosition - wristTransform.position;
      if (Vector3.Angle(_directVector.normalized, targetFinger.normalized) > 99.0f) {
        Vector3 refNode = Vector3.Reflect(targetFinger.normalized, _directVector.normalized);
        targetFinger = -refNode;
      }
      return targetFinger;
    }

    private Vector3 ToVector(NormalizedLandmark landmark) {
      return new Vector3(landmark.X, landmark.Y, landmark.Z);
    }
    private void ComputeWristRotation() {
      var wristTransform = transform;
      var indexFinger = _handLandmarks[5].transform.position;
      var middleFinger = _handLandmarks[9].transform.position;

      var vectorToMiddle = middleFinger - wristTransform.position;
      var vectorToIndex = indexFinger - wristTransform.position;
      Vector3.OrthoNormalize(ref vectorToMiddle, ref vectorToIndex);
      _forwardVector = Vector3.Cross(vectorToIndex, vectorToMiddle);
      _directVector = vectorToMiddle.normalized;
      _wristRotation = Quaternion.LookRotation(_forwardVector, vectorToIndex);
    }
  }
}
