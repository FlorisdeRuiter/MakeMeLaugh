using UnityEngine;

public class Billboard : MonoBehaviour
{
    public Camera _camera;

    private void LateUpdate()
    {
        if (_camera != null)
            transform.rotation = _camera.transform.rotation;
    }
}
