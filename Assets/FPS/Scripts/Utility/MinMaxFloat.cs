using System;
using UnityEngine;

namespace Unity.FPS.Utility
{
    //Lerp에서 사용하는 파라미터(최소값 a, 최대값 b)의 정의
    //a(0) -> b(1) 
    [Serializable]
    public struct MinMaxFloat
    {
        public float Min;
        public float Max;

        //매개변수 ratio의 따른 lerp값 반환
        public float GetValueFromRatio(float ratio)
        {
            return Mathf.Lerp(Min, Max, ratio);
        }
    }

    [Serializable]
    public struct MinMaxColor
    {
        [ColorUsage(true, true)] public Color Min;
        [ColorUsage(true, true)] public Color Max;

        //매개변수 ratio의 따른 lerp값 반환
        public Color GetValueFromRatio(float ratio)
        {
            return Color.Lerp(Min, Max, ratio);
        }
    }

    [Serializable]
    public struct MinMaxVector3
    {
        public Vector3 Min;
        public Vector3 Max;

        //매개변수 ratio의 따른 lerp값 반환
        public Vector3 GetValueFromRatio(float ratio)
        {
            return Vector3.Lerp(Min, Max, ratio);
        }
    }
}