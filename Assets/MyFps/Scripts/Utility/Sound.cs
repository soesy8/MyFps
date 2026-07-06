using Unity.VisualScripting;
using UnityEngine;
using System;

namespace MyFps
{
    [Serializable]
    public class Sound
    {
        public string name;     //관리되는 사운드 이름
        public AudioClip clip;  //재생할 사운드 리소스 - 음원

        public float volume;    //오디오 재생 볼륨
        public float pitch;     //오디오 재생 속도

        public bool loop;       //반복 재생 여부
        public bool playOnAwake;//처음 플레이 여부

        public AudioSource audioSource;     //플레이시킬 오디오 소스
    }
}