using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    [SerializeField] private Camera camera1;
    [SerializeField] private Camera camera2;
    [SerializeField] private KeyCode switchKey = KeyCode.C; // Default to 'C' key

    private void Start()
    {
        // Ensure camera1 is active and camera2 is inactive at start
        if (camera1 != null && camera2 != null)
        {
            camera1.gameObject.SetActive(true);
            camera2.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        // Check if the switch key was pressed
        if (Input.GetKeyDown(switchKey))
        {
            SwitchCamera();
        }
    }

    public void SwitchCamera()
    {
        if (camera1 != null && camera2 != null)
        {
            // Toggle the active state of both cameras
            bool camera1Active = camera1.gameObject.activeSelf;
            camera1.gameObject.SetActive(!camera1Active);
            camera2.gameObject.SetActive(camera1Active);
        }
    }
}