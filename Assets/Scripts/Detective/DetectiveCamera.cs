using UnityEngine;
using Unity.Cinemachine;

public class DetectiveCamera : MonoBehaviour
{
    [SerializeField] private InputReader inputReader;
    [SerializeField] private CinemachineCamera virtualCam;
    [SerializeField] private float panSpeed = 5f;
    [SerializeField] private float smoothTime = 0.1f;
    [SerializeField] private Transform cameraRig;
    [SerializeField] private Transform playerTarget;

    private Vector2 panDirection;
    private Vector3 targetPosition;
    private Vector3 velocity;

    private bool isActive;

    private void OnEnable()
    {
        inputReader.SetDetective();
        inputReader.PanEvent += HandlePan;
        GameManager.OnGameStateChange += OnGameStateChanged;
    }

    private void OnDisable()
    {
        inputReader.PanEvent -= HandlePan;
        GameManager.OnGameStateChange -= OnGameStateChanged;
    }

    private void HandlePan(Vector2 direction)
    {
        panDirection = direction;
    }

    private void OnGameStateChanged(GameState state)
    {
        if (state == GameState.Detective)
        {
            virtualCam.Follow = cameraRig;
            targetPosition = cameraRig.position;
            velocity = Vector3.zero;
            isActive = true;
        }else
        {
            isActive = false;
            virtualCam.Follow = playerTarget;
        }
    }

    private void Update()
    {
        if (!isActive) return;

        targetPosition += new Vector3(panDirection.x, panDirection.y, 0f) * (panSpeed * Time.deltaTime);

        cameraRig.position = Vector3.SmoothDamp(cameraRig.position, targetPosition, ref velocity, smoothTime);
    }
}
