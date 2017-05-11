using UnityEngine;
using HoloToolkit.Unity.InputModule;
using LocalJoost.HoloToolkitExtensions;
using UnityEngine.SceneManagement;

public class SpeechCommandExecuter : MonoBehaviour
{
    public bool IsActive = false;
    public GameObject model;
    public GameObject minimap;

    private AudioSource _sound;
    Vector3 savedPostion;
    void Start()
    {
        _sound = GetComponent<AudioSource>();
        savedPostion = minimap.transform.position;
    }

    public void Move()
    {
        TryChangeMode(ManipulationMode.Move);
    }

    public void RotateX()
    {
        TryChangeMode(ManipulationMode.RotateX);
    }
    public void RotateY()
    {
        TryChangeMode(ManipulationMode.RotateY);
    }

    public void RotateZ()
    {
        TryChangeMode(ManipulationMode.RotateZ);
    }

    public void ChangetoArtery()
    {
        SceneManager.LoadScene(0);
    }

    public void ChangetoDrive()
    {
        SceneManager.LoadScene(1);
    }

    public void Scale()
    {
        TryChangeMode(ManipulationMode.Scale);
    }

    public void Done()
    {
        TryChangeMode(ManipulationMode.None);
    }

    public void Faster()
    {
        TryChangeSpeed(true);
    }

    public void Slower()
    {
        TryChangeSpeed(false);
    }
    public void ShowModel()
    {
        if (model.activeInHierarchy == false)
        {
            model.SetActive(true);
        }
    }
    public void hideModel()
    {
        if (model.activeInHierarchy == true)
        {
            model.SetActive(false);
        }
    }

    public void RotateMiniMap()
    {

        transform.position = savedPostion;
        minimap.transform.rotation = Quaternion.identity;
        transform.localScale = Vector3.one;
    }

    private void TryChangeMode(ManipulationMode mode)
    {
        var manipulator = GetSpatialManipulator();
        if (manipulator == null)
        {
            return;
        }

        if (manipulator.Mode != mode)
        {
            manipulator.Mode = mode;
            TryPlaySound();
        }
    }

    private void TryChangeSpeed(bool faster)
    {
        var manipulator = GetSpatialManipulator();
        if (manipulator == null)
        {
            return;
        }

        if (manipulator.Mode == ManipulationMode.None)
        {
            return;
        }

        if (faster)
        {
            manipulator.Faster();
        }
        else
        {
            manipulator.Slower();

        }
        TryPlaySound();

    }

    private void TryPlaySound()
    {
        if (_sound != null && _sound.clip != null)
        {
            _sound.Play();
        }
    }


    private SpatialManipulator GetSpatialManipulator()
    {
        var lastSelectedObject = AppStateManager.Instance.SelectedGameObject;
        if (lastSelectedObject == null)
        {
            Debug.Log("No selected element found");
            return null;
        }
        var manipulator = model.GetComponent<SpatialManipulator>(); //lastSelectedObject.GetComponent<SpatialManipulator>();
        if (manipulator == null)
        {
            manipulator = model.GetComponent<SpatialManipulator>(); // lastSelectedObject.GetComponentInChildren<SpatialManipulator>();
        }

        if (manipulator == null)
        {
            Debug.Log("No manipulator component found");
        }
        return manipulator;
    }
}
