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

using Mediapipe;
using Mediapipe.Unity;
using UnityEngine;

namespace SeedUnityVRKit {
  public class PoseLandmarksRecognizer {
    /// <summary>Scale factor to z axis. To be tuned.</summary>
    private const float _zScale = 0.3f;
    /// <summary>Screen width used as to scale the recognized normalized landmarks.</summary>
    private readonly float _screenWidth;
    /// <summary>Screen height used as to scale the recognized normalized landmarks.</summary>
    private readonly float _screenHeight;
    private KalmanFilter leftHip_kalmanFilter = new KalmanFilter(0.125f, 1f);
    private KalmanFilter rightHip_kalmanFilter = new KalmanFilter(0.125f, 1f);
    private KalmanFilter leftShoulder_kalmanFilter = new KalmanFilter(0.125f, 1f);
    private KalmanFilter leftElbow_kalmanFilter = new KalmanFilter(0.125f, 1f);
    private KalmanFilter leftWrist_kalmanFilter = new KalmanFilter(0.125f, 1f);
    private KalmanFilter rightShoulder_kalmanFilter = new KalmanFilter(0.125f, 1f);
    private KalmanFilter rightElbow_kalmanFilter = new KalmanFilter(0.125f, 1f);
    private KalmanFilter rightWrist_kalmanFilter = new KalmanFilter(0.125f, 1f);

    public PoseLandmarksRecognizer(float screenWidth, float screenHeight) {
      _screenWidth = screenWidth;
      _screenHeight = screenHeight;
    }

    public PoseLandmarks recognize(NormalizedLandmarkList poseLandmarks) {

      PoseLandmarks landmarks = new PoseLandmarks();

      if (getVisible(poseLandmarks.Landmark[Landmarks.LeftShoulder]) && getVisible(poseLandmarks.Landmark[Landmarks.RightShoulder])) {
        Vector3 leftHip = leftHip_kalmanFilter.Update(toVector(poseLandmarks.Landmark[Landmarks.LeftHip]));
        Vector3 rightHip = rightHip_kalmanFilter.Update(toVector(poseLandmarks.Landmark[Landmarks.RightHip]));
        Vector3 leftShoulder = leftShoulder_kalmanFilter.Update(toVector(poseLandmarks.Landmark[Landmarks.LeftShoulder]));
        Vector3 leftElbow = leftElbow_kalmanFilter.Update(toVector(poseLandmarks.Landmark[Landmarks.LeftElbow]));
        Vector3 leftWrist = leftWrist_kalmanFilter.Update(toVector(poseLandmarks.Landmark[Landmarks.LeftWrist]));
        Vector3 rightShoulder = rightShoulder_kalmanFilter.Update(toVector(poseLandmarks.Landmark[Landmarks.RightShoulder]));
        Vector3 rightElbow = rightElbow_kalmanFilter.Update(toVector(poseLandmarks.Landmark[Landmarks.RightElbow]));
        Vector3 rightWrist = rightWrist_kalmanFilter.Update(toVector(poseLandmarks.Landmark[Landmarks.RightWrist]));

        Vector3 forward = GetNormal(leftShoulder, leftHip, rightHip);

        // Note: left and right are mirrored here.

        landmarks.Add(
            new PoseLandmark { Id = Landmarks.Hip, Rotation = Quaternion.LookRotation(forward) });
        landmarks.Add(new PoseLandmark {
          Id = Landmarks.LeftShoulder,
          Rotation = Quaternion.LookRotation(
                                             rightShoulder - rightElbow, -forward)
        });
        landmarks.Add(new PoseLandmark {
          Id = Landmarks.LeftElbow,
          Rotation = Quaternion.LookRotation(
                                             rightElbow - rightWrist, rightShoulder - rightElbow)
        });
        landmarks.Add(new PoseLandmark {
          Id = Landmarks.RightShoulder,
          Rotation = Quaternion.LookRotation(leftShoulder - leftElbow,
                                                                            -forward)
        });
        landmarks.Add(new PoseLandmark {
          Id = Landmarks.RightElbow,
          Rotation = Quaternion.LookRotation(
                                             leftElbow - leftWrist, leftShoulder - leftElbow)
        });
      }

      return landmarks;
    }

    private Vector3 toVector(NormalizedLandmark landmark) {
      return new Vector3(landmark.X * _screenWidth, landmark.Y * _screenHeight,
                         landmark.Z * _screenWidth * _zScale);
    }

    private bool getVisible(NormalizedLandmark landmark) {
      return landmark.Visibility > 0.9F;
    }

    // <summary>
    // Get the normal to a triangle from the three corner points, a, b and c.
    // See https://docs.unity3d.com/ScriptReference/Vector3.Cross.html.
    // </summary>
    // <exception cref="Assertions.AssertionException">
    // An assertion is thrown if the sides from input vectors are parallel.
    // </exception>
    private Vector3 GetNormal(Vector3 a, Vector3 b, Vector3 c) {
      // Find vectors corresponding to two of the sides of the triangle.
      Vector3 side1 = b - a;
      Vector3 side2 = c - a;

      // Cross the vectors to get a perpendicular vector, then normalize it.
      return Vector3.Cross(side1, side2).normalized;
    }
  }
}
