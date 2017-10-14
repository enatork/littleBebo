using UnityEngine;

public class CameraController : MonoBehaviour
{

    private Transform _XForm_Camera;
    private Transform _XForm_Parent;

    private Vector3 _LocalRotation;
    private float _CameraDistance = 10f;

    public float MouseSensitivity = 4f;
    public float ScrollSensitivity = 2f;
    public float OrbitDampening = 10f;
    public float ScrollDampening = 6f;

    public Transform target;

    public bool CameraDisabled = false;

    // Use this for initialization
    void Start()
    {
        _XForm_Camera = transform;
        _XForm_Parent = transform.parent;
    }

    // Update is called once per frame
    void LateUpdate()
    {

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            CameraDisabled = !CameraDisabled;
        }

        Vector3 followPosition = new Vector3(target.position.x, target.position.y, target.position.z);

        _XForm_Parent.position = followPosition;

        if (!CameraDisabled)
        {
            if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
            {
                _LocalRotation.x += Input.GetAxis("Mouse X") * MouseSensitivity;
                _LocalRotation.y -= Input.GetAxis("Mouse Y") * MouseSensitivity;

                //Clamp Y Axis
                _LocalRotation.y = Mathf.Clamp(_LocalRotation.y, 0f, 90f);
            }

            if (Input.GetAxis("Mouse ScrollWheel") != 0f)
            {
                float ScrollAmount = Input.GetAxis("Mouse ScrollWheel") * ScrollSensitivity;

                //camera zoom faster the further you are from the object
                ScrollAmount *= (_CameraDistance * 0.3f);

                _CameraDistance += ScrollAmount * -1f;

                _CameraDistance = Mathf.Clamp(_CameraDistance, 1.5f, 100f);
            }
        }

        Quaternion QT = Quaternion.Euler(_LocalRotation.y, _LocalRotation.x, 0);
        _XForm_Parent.rotation = Quaternion.Lerp(_XForm_Parent.rotation, QT, OrbitDampening * Time.deltaTime);

        if (_XForm_Camera.localPosition.z != _CameraDistance * -1f)
        {
            _XForm_Camera.localPosition = new Vector3(0f, 0f, Mathf.Lerp(_XForm_Camera.localPosition.z, _CameraDistance * -1f, ScrollDampening * Time.deltaTime));
        }

    }
}
