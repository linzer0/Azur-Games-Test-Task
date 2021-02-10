using UnityEngine;

namespace Other
{
    public class CameraMovement : MonoBehaviour
    {
        public float Speed = 5.0f;

        public float scale = 1.0f;

        void Update()
        {
            var objectTransform = transform;
            if (Input.GetButton("Vertical"))
            {
                var verticalValue = Input.GetAxis("Vertical") > 0 ? 1 : -1;

                objectTransform.position += Vector3.forward * (verticalValue * (Speed * Time.deltaTime));
            }

            transform.position += new Vector3(0, (-1) * Input.mouseScrollDelta.y * scale, 0);

            if (Input.GetButton("Horizontal"))
            {
                var horizontalValue = Input.GetAxis("Horizontal");

                objectTransform.position += objectTransform.right * (horizontalValue * (Speed * Time.deltaTime));
            }
        }
    }
}