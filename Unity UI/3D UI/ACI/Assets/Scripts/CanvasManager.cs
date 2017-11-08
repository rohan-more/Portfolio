using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CanvasManager : MonoBehaviour
{
 
    public List <GameObject> objectList;

    public GameObject currentShape;
    public Slider _slider;
  
    private Toggle isEnlargeToggleEnabled;
    private Toggle isRotateToggleEnabled;
    private Toggle _XToggle;
    private Toggle _YToggle;
    private Toggle _ZToggle;
    private bool isXToggleEnabled;
    private bool isYToggleEnabled;
    private bool isZToggleEnabled;
    int rotationSpeed = 200;

    public GameObject cubePrefab;
    public GameObject spherePrefab;
    private GameObject buttonPrefab;
    public Button cubeButton;
    public Button sphereButton;
    public ObjectSelector[] buttons;
    private Transform panel;
    public float radius = 20f;
    public static int objectsAllowed;
    public int noOfCubes= 1;
    public int noOfSpheres = 1;
    private Text rotationValues;
    private Text positionValues;
    private Button exit;
    private LoginScreen _loginSw;
    public void Start()
    {
        GameObject canvas = GameObject.Find("Canvas");
        objectList = new List<GameObject>();
       
        _slider = canvas.transform.Find("EnlargeToggle").Find("Slider").GetComponent<Slider>();
        _XToggle = canvas.transform.Find("EnlargeToggle").Find("Slider").Find("X").GetComponent<Toggle>();
        _YToggle = canvas.transform.Find("EnlargeToggle").Find("Slider").Find("Y").GetComponent<Toggle>();
        _ZToggle = canvas.transform.Find("EnlargeToggle").Find("Slider").Find("Z").GetComponent<Toggle>();
        isEnlargeToggleEnabled = canvas.transform.Find("EnlargeToggle").GetComponent<Toggle>();
        isRotateToggleEnabled = canvas.transform.Find("RotateToggle").GetComponent<Toggle>();
        isEnlargeToggleEnabled.onValueChanged.AddListener(EnlargeClicked);
        isRotateToggleEnabled.onValueChanged.AddListener(RotateClicked);
        rotationValues = canvas.transform.Find("RotateValues").GetComponent<Text>();
        positionValues = canvas.transform.Find("PositionValues").GetComponent<Text>();
        exit = canvas.transform.Find("Exit").GetComponent<Button>();
    
        _slider.onValueChanged.AddListener(EnlargeCube);
        _XToggle.onValueChanged.AddListener(ToggleXListener);
        _YToggle.onValueChanged.AddListener(ToggleYListener);
        _ZToggle.onValueChanged.AddListener(ToggleZListener);
        exit.onClick.AddListener(AppExit);

        cubePrefab = Resources.Load("Prefabs/Cube") as GameObject;
        spherePrefab = Resources.Load("Prefabs/Sphere") as GameObject;
        buttonPrefab = Resources.Load("Prefabs/ObjectSelectButton") as GameObject;
        cubeButton = canvas.transform.Find("CubeButton").GetComponent<Button>();
        sphereButton = canvas.transform.Find("SphereButton").GetComponent<Button>();
        panel = canvas.transform.Find("Panel");
        cubeButton.onClick.AddListener(CreateCube);
        sphereButton.onClick.AddListener(CreateSphere);
        
    
        _loginSw = GameObject.Find("LoginControl").GetComponent<LoginScreen>();
        _slider.gameObject.SetActive(false);
        

    }

    void EnlargeClicked(bool value)
    {
        if (isEnlargeToggleEnabled.isOn)
        {
             _loginSw.sw.WriteLine("Enlarge Toggle Selected");
        }
        else if (!isEnlargeToggleEnabled.isOn)
        {
             _loginSw.sw.WriteLine("Enlarge Toggle Deselected");
        }
    }

    void AppExit()
    {
        _loginSw.sw.Close();
        Application.Quit();
    }

    void RotateClicked(bool value)
    {
        if (isRotateToggleEnabled.isOn)
        {
            _loginSw.sw.WriteLine("Enlarge Toggle Selected");
        }
        else if (!isRotateToggleEnabled.isOn)
        {
            _loginSw.sw.WriteLine("Enlarge Toggle Deselected");
        }
    }


    public void Update()
    {
        if(currentShape)
        {
            string text = "X:{ " + currentShape.transform.rotation.x + "}Y:{ " + currentShape.transform.rotation.y + "}Z:{ " + currentShape.transform.rotation.z + "}";

            rotationValues.text = text.Replace("}", "}" + System.Environment.NewLine);
            string text2 = "X:{ " + currentShape.transform.position.x + "}Y:{ " + currentShape.transform.position.y + "}Z:{ " + currentShape.transform.position.z + "}";

            positionValues.text = text2.Replace("}", "}" + System.Environment.NewLine);
        }
        

        if (isEnlargeToggleEnabled.isOn)
        {
            _slider.gameObject.SetActive(true);
           // _loginSw.sw.WriteLine("Enlarge Toggle Selected");
        }

        else if(!isEnlargeToggleEnabled.isOn)
        {
            _slider.gameObject.SetActive(false);
            //_loginSw.sw.WriteLine("Enlarge Toggle Deselected");
        }

        if (Input.GetMouseButton(0) && isRotateToggleEnabled.isOn)
        {
            RotateObject();
        }

    }

    void CreateCube()
    {
        if(objectsAllowed < 12)
        {
            Vector3 position = new Vector3(0.0F, 0.5F, 0.0F);

            GameObject cube = Instantiate(cubePrefab, position, Quaternion.identity);
            GameObject button = Instantiate(buttonPrefab, panel);
            button.GetComponent<ObjectSelector>().shape = cube;
            button.GetComponentInChildren<Text>().text = "Cube " + noOfCubes;
            objectList.Add(cube);
            currentShape = cube;
            for (var i = 0; i < objectList.Count; i++)
            {
                if (objectList[i] != currentShape)
                {
                    objectList[i].SetActive(false);
                }
            }
            _loginSw.sw.WriteLine("Cube " + noOfCubes + " created.");
            noOfCubes++;
            objectsAllowed++;
            
        }
    }

    void CreateSphere()
    {
        if (objectsAllowed < 12)
        {
            Vector3 position = new Vector3(0.0F, 0.5F, 0.0F);

            GameObject sphere = Instantiate(spherePrefab, position, Quaternion.identity);
            GameObject button = Instantiate(buttonPrefab, panel);
            button.GetComponent<ObjectSelector>().shape = sphere;
            button.GetComponentInChildren<Text>().text = "Sphere " + noOfSpheres;
            objectList.Add(sphere);
            currentShape = sphere;
            for (var i = 0; i < objectList.Count; i++)
            {
                if(objectList[i]!= currentShape)
                {
                    objectList[i].SetActive(false);
                }
            }
                _loginSw.sw.WriteLine("Sphere " + noOfSpheres + " created.");
            noOfSpheres++;
            objectsAllowed++;
            
        }

    }
    

    void OnMouseDrag()
    {
       
        float XRotation = Input.GetAxis("Mouse X") * rotationSpeed * Mathf.Deg2Rad;
        float YRotation = Input.GetAxis("Mouse Y") * rotationSpeed * Mathf.Deg2Rad;
        currentShape.transform.Rotate(Vector3.up, -XRotation);
        currentShape.transform.Rotate(Vector3.right, YRotation);
    

    }

    void RotateObject()
    {
        OnMouseDrag();
       
    }

    private void ToggleXListener(bool xToggle)
    {
        isXToggleEnabled = xToggle;
        _loginSw.sw.WriteLine("Selected X axis for rotation");
    }
    private void ToggleYListener(bool yToggle)
    {
        isYToggleEnabled = yToggle;
        _loginSw.sw.WriteLine("Selected Y axis for rotation");
    }
    private void ToggleZListener(bool zToggle)
    {
        isZToggleEnabled = zToggle;
        _loginSw.sw.WriteLine("Selected Z axis for rotation");
    }


    public void EnlargeCube(float value)
    {

        currentShape.transform.localScale = new Vector3(isXToggleEnabled ? value : currentShape.transform.localScale.x, isYToggleEnabled ? value : currentShape.transform.localScale.y, isZToggleEnabled ? value : currentShape.transform.localScale.z);
      

    }

}

	


