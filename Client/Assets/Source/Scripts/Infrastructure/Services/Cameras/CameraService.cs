using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Source.Scripts.Infrastructure.Services.Cameras
{
    public class CameraService : ICameraService, IInitializable
    {
        private Camera _main;
        
        public Camera Main
        {
            get => _main;
            set
            {
                if(_main == value)
                    return;

                _main = value;
                MainChanged?.Invoke();
            }
        }

        public event Action MainChanged;
        public UniTask Initialize()
        {
            Main = Camera.main;
            return UniTask.CompletedTask;
        }
    }
}