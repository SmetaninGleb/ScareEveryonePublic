
using UnityEngine;
using Cinemachine;
using Kuhpik;

class VirtualCameraLoadingSystem : GameSystem, IIniting
{
    public void OnInit()
    {
        CinemachineVirtualCamera virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
        virtualCamera.LookAt = game.Player.transform;
        virtualCamera.Follow = game.Player.transform;
        virtualCamera.transform.SetParent(null);
    }
}