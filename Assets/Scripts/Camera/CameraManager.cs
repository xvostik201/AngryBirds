using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{

    private CinemachineVirtualCamera _vCam;
    private Vector3 _initPos;
    private Transform _currentFollow;

    public static CameraManager Instance;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        _vCam = GetComponent<CinemachineVirtualCamera>();
        _initPos = transform.position;

        Slingshot.OnBirdLaunched += OnBirdLaunched;
    }

    private void OnDestroy()
    {
        Slingshot.OnBirdLaunched -= OnBirdLaunched;
    }

    private void OnBirdLaunched(Bird bird)
    {
        _currentFollow = bird.transform;
        _vCam.Follow = _currentFollow;
    }

    public void ResetToInitial()
    {
        transform.position = _initPos;
        _vCam.Follow = null;
    }
}
