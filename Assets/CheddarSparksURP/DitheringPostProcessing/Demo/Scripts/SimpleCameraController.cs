using UnityEngine;

namespace CheddarSparks.CustomDitheringPostProcessing.Demo
{
    [RequireComponent(typeof(Camera))]
    public class SimpleCameraController : MonoBehaviour
    {
        [Header("Movement")]
        public float moveSpeed = 5f;
        public float fastSpeedMultiplier = 3f;

        [Header("Look")]
        public float lookSensitivity = 2f;
        public float maxPitch = 89f;

        [Header("Cinematic Mode")]
        public bool cinematicMode = true;
        [Tooltip("How quickly the camera smooths towards its target rotation.")]
        public float rotationSmoothTime = 0.08f;
        [Tooltip("How quickly the camera smooths towards its target position.")]
        public float movementSmoothTime = 0.12f;

        private float yaw = 0f;
        private float pitch = 0f;

        // Cached values for smoothing
        private Vector3 currentVelocity;
        private Vector3 targetPosition;
        private Quaternion targetRotation;

        private bool skipMouseFrame = false;

        void Start()
        {
            yaw = transform.eulerAngles.y;
            pitch = transform.eulerAngles.x;
            targetPosition = transform.position;
            targetRotation = transform.rotation;
        }

        void LateUpdate()
        {
            HandleLook();
            HandleMovement();
        }

        void HandleLook()
        {
            if (Input.GetKey(KeyCode.Mouse1))
            {
                if (Cursor.lockState != CursorLockMode.Locked)
                {
                    Cursor.lockState = CursorLockMode.Locked;
                    skipMouseFrame = true;
                }

                if (skipMouseFrame)
                {
                    skipMouseFrame = false;
                    return;
                }

                float mouseX = Input.GetAxis("Mouse X") * lookSensitivity;
                float mouseY = Input.GetAxis("Mouse Y") * lookSensitivity;

                yaw += mouseX;
                pitch -= mouseY;
                pitch = Mathf.Clamp(pitch, -maxPitch, maxPitch);

                targetRotation = Quaternion.Euler(pitch, yaw, 0f);

                if (cinematicMode)
                {
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 1f - Mathf.Exp(-rotationSmoothTime * Time.deltaTime * 60f));
                }
                else
                {
                    transform.rotation = targetRotation;
                }
            }
            else
            {
                if (Cursor.lockState != CursorLockMode.None)
                    Cursor.lockState = CursorLockMode.None;
            }
        }

        void HandleMovement()
        {
            float speed = moveSpeed;
            if (Input.GetKey(KeyCode.LeftShift)) speed *= fastSpeedMultiplier;
            int inputX = (Input.GetKey(KeyCode.D) ? 1 : 0) - (Input.GetKey(KeyCode.A) ? 1 : 0);
            int inputZ = (Input.GetKey(KeyCode.W) ? 1 : 0) - (Input.GetKey(KeyCode.S) ? 1 : 0);
            Vector3 direction = new Vector3(
                inputX,
                0,
                inputZ
            );

            if (Input.GetKey(KeyCode.E)) direction.y += 1;
            if (Input.GetKey(KeyCode.Q)) direction.y -= 1;

            direction.Normalize();

            Vector3 move = transform.TransformDirection(direction) * speed * Time.deltaTime;
            targetPosition += move;

            if (cinematicMode)
            {
                transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, movementSmoothTime);
            }
            else
            {
                transform.position = targetPosition;
            }
        }
    }
}
