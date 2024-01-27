using UnityEngine;

public class Billboard : MonoBehaviour
{
    public Camera _camera;

    private void LateUpdate()
    {
        transform.rotation = _camera.transform.rotation;
    }
}
