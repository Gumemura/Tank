using UnityEngine;
using Controller.Player;

namespace Controller.Game
{
    public class GameController : MonoBehaviour
    {
        private const float CAMERA_ZOOM = .2f;
        private const float CAMERA_ZOOM_MIN = 3;
        private const float CAMERA_ZOOM_MAX = 7;

        private const float CAMERA_FOLLOW_SMOTH_TIME = 0.25f;
        private Vector3 CAMERA_FOLLOW_OFFSET = new Vector3(0f, 0f, -10f);
        private Vector3 CAMERA_FOLLOW_VELOCITY = Vector3.zero;

        public PlayerController playerController;

        private Camera camera;

        public static GameController Instance { get; private set; }

        // Start is called before the first frame update
        void Awake()
        {
            if (Instance != null && Instance != this) 
            { 
                Destroy(this); 
            } 
            else 
            { 
                Instance = this; 
            } 
        }

        void Start()
        {
            camera = this.gameObject.transform.GetChild(0).GetComponent<Camera>();
        }

        // Update is called once per frame
        void Update()
        {
            ZoomInZoomOut();
        }

        void FixedUpdate()
        {
            CameraFollowActiveEnitiy(playerController.GetCurrentActiveEntity());
        }

        void ZoomInZoomOut()
        {
            camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, camera.orthographicSize + Input.mouseScrollDelta.y * CAMERA_ZOOM * -1, 1);
            camera.orthographicSize = Mathf.Clamp(camera.orthographicSize, CAMERA_ZOOM_MIN, CAMERA_ZOOM_MAX);
        }

        private void CameraFollowActiveEnitiy(Transform entityToFollow)
        {
            Vector3 targetPosition = entityToFollow.position + CAMERA_FOLLOW_OFFSET;
            camera.transform.position = Vector3.SmoothDamp(camera.transform.position, targetPosition, ref CAMERA_FOLLOW_VELOCITY, CAMERA_FOLLOW_SMOTH_TIME);
        }
    }
}