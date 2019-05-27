using Sirenix.OdinInspector;
using UnityEngine;

namespace PNN
{
    public class CameraManager : MonoBehaviour
    {
        [SerializeField, BoxGroup("Camera Settings")] private float cameraMoveSpeed = 3f;
        [SerializeField, BoxGroup("Camera Settings")] private float scrollSpeed = 1f;

        Vector3 anchorMovePoint;

        void Update()
        {
            if (Input.GetMouseButton(2))
            {
                if (Input.GetMouseButtonDown(2))
                {
                    anchorMovePoint = Input.mousePosition;
                }

                Vector3 mouseMovement = (Input.mousePosition - anchorMovePoint) / Screen.height * 2f;
                mouseMovement = (mouseMovement.x * transform.right + mouseMovement.y * transform.up);
                transform.position += mouseMovement * cameraMoveSpeed * Find.Camera.orthographicSize / 5f * Time.fixedDeltaTime;
            }
            else
            {
                Vector3 movement = (Input.GetAxisRaw("Horizontal") * transform.right + Input.GetAxisRaw("Vertical") * transform.up).normalized;
                transform.position += movement * cameraMoveSpeed * Find.Camera.orthographicSize / 5f * Time.fixedDeltaTime;
            }
            
            float deltaScroll = Input.mouseScrollDelta.y;
            if (deltaScroll != 0)
            {
                Find.Camera.orthographicSize = Mathf.Clamp(Find.Camera.orthographicSize - deltaScroll * scrollSpeed, 2.5f, 15f);
            }
        }
    }
}