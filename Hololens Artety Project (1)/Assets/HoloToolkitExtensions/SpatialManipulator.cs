using UnityEngine;
using HoloToolkit.Unity.InputModule;

namespace LocalJoost.HoloToolkitExtensions
{
    public class SpatialManipulator : MonoBehaviour
    {
        public float MoveSpeed = 0.1f;

        public float RotateSpeed = 6f;

        public float ScaleSpeed = 0.2f;


        public BaseRayStabilizer Stabilizer = null;

        public BaseSpatialMappingCollisionDetector CollisonDetector;

        void Start()
        {
            Mode = ManipulationMode.None;
            if (CollisonDetector == null)
            {
                CollisonDetector = gameObject.AddComponent<DefaultMappingCollisionDetector>();
            }
        }

        public void Faster()
        {
            if (Mode == ManipulationMode.Move)
            {
                MoveSpeed *= 2f;
            }

            if (Mode == ManipulationMode.RotateX)
            {
                RotateSpeed *= 2f;
            }
            if (Mode == ManipulationMode.RotateY)
            {
                RotateSpeed *= 2f;
            }
            if (Mode == ManipulationMode.RotateZ)
            {
                RotateSpeed *= 2f;
            }
            if (Mode == ManipulationMode.Scale)
            {
                ScaleSpeed *= 2f;
            }
        }

        public ManipulationMode Mode { get; set; }


        public void Slower()
        {
            if (Mode == ManipulationMode.Move)
            {
                MoveSpeed /= 2f;
            }

            if (Mode == ManipulationMode.RotateX)
            {
                RotateSpeed /= 2f;
            }
            if (Mode == ManipulationMode.RotateY)
            {
                RotateSpeed /= 2f;
            }
            if (Mode == ManipulationMode.RotateZ)
            {
                RotateSpeed /= 2f;
            }

            if (Mode == ManipulationMode.Scale)
            {
                ScaleSpeed /= 2f;
            }
        }

        public void Manipulate(Vector3 manipulationData)
        {
            switch (Mode)
            {
                case ManipulationMode.Move:
                    Move(manipulationData);
                    break;
                case ManipulationMode.RotateX:
                    RotateX(manipulationData);
                    break;
                case ManipulationMode.RotateY:
                    RotateY(manipulationData);
                    break;
                case ManipulationMode.RotateZ:
                    RotateZ(manipulationData);
                    break;
                case ManipulationMode.Scale:
                    Scale(manipulationData);
                    break;
            }
        }

        void Move(Vector3 manipulationData)
        {
            var delta = manipulationData*MoveSpeed;
            if (CollisonDetector.CheckIfCanMoveBy(delta))
            {
                transform.position += delta;// changed from localPosition to test 
            }
        }

        void RotateX(Vector3 manipulationData)
        {
            transform.RotateAround(transform.position, Camera.main.transform.up, 
                -manipulationData.x * RotateSpeed);
            //transform.RotateAround(transform.position, Camera.main.transform.forward, 
               // manipulationData.y * RotateSpeed);
           // transform.RotateAround(transform.position, Camera.main.transform.right, 
              //  manipulationData.z * RotateSpeed);
        }
        void RotateY(Vector3 manipulationData)
        {
            transform.RotateAround(transform.position, Camera.main.transform.forward,
                manipulationData.y * RotateSpeed);
        }
        void RotateZ(Vector3 manipulationData)
        {
            transform.RotateAround(transform.position, Camera.main.transform.forward,
                manipulationData.x * RotateSpeed);
        }

        void Scale(Vector3 manipulationData)
        {
            transform.localScale *= 1.0f - (manipulationData.z*ScaleSpeed);
        }
    }
}
