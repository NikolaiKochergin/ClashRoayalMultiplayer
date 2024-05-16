using System;
using UnityEngine;

namespace Source.Scripts.Infrastructure.Services.Cameras
{
    public interface ICameraService
    {
        Camera Main { get; set; }
        event Action MainChanged;
    }
}