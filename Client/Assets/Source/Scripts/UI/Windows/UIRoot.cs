﻿using Reflex.Attributes;
using Source.Scripts.Infrastructure.Services.Cameras;
using UnityEngine;

namespace Source.Scripts.UI.Windows
{
    [RequireComponent(typeof(Canvas))]
    public class UIRoot : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;
        
        private ICameraService _camera;

        [Inject]
        private void Construct(ICameraService cameraService) => 
            _camera = cameraService;

        public float ScaleFactor => _canvas.scaleFactor;

        private void Start()
        {
            SetMainCamera();
            _camera.MainChanged += SetMainCamera;
        }

        private void SetMainCamera() => 
            _canvas.worldCamera = _camera.Main;
    }
}