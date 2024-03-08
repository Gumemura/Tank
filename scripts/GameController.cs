using UnityEngine;

public class GameController : MonoBehaviour
{
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
        camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, camera.orthographicSize + Input.mouseScrollDelta.y, 1);
    }
}
