using UnityEngine;

public class GameController : MonoBehaviour
{
    private const float CAMERA_ZOOM = .2f;
    private const float CAMERA_ZOOM_MIN = 3;
    private const float CAMERA_ZOOM_MAX = 7;
    public Camera camera;

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

    void ZoomInZoomOut()
    {
        camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, camera.orthographicSize + Input.mouseScrollDelta.y * CAMERA_ZOOM * -1, 1);
        camera.orthographicSize = Mathf.Clamp(camera.orthographicSize, CAMERA_ZOOM_MIN, CAMERA_ZOOM_MAX);
    }
}
