using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GyroCamera : MonoBehaviour
{
    private static GyroCamera instance;
    public static GyroCamera Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<GyroCamera>();
                if(instance == null)
                {
                    instance = new GameObject("SpawnedGyroManager", typeof(GyroCamera)).GetComponent<GyroCamera>();
                }
            }
            return instance;
        }
        set
        {
            instance = value;
        }
    }

    [Header("Logic")]
    private Gyroscope gyro;
    private Quaternion rotation;
    private bool gyroActive;

    private GameObject cameraContainer;


    public void EnableGyro()
    {
        if (gyroActive)
            return;
        if (SystemInfo.supportsGyroscope)
        {
            gyro = Input.gyro;
            gyro.enabled = true;

            cameraContainer.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            rotation = new Quaternion(0, 0, 1, 0);
            gyroActive = gyro.enabled;
        }
        else
        {
            Debug.Log("Not supported");
        }
    }

    public Quaternion GetGyroRotation()
    {
        return rotation;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitToGyro());
    }

    // Update is called once per frame
    void Update()
    {
        if (gyroActive)
        {
            transform.localRotation = gyro.attitude * rotation;
        }


        if (gameObject.transform.position.z >= 19f)
        {
            SceneManager.LoadScene(0);
        }
    }

    IEnumerator WaitToGyro()
    {
        Debug.Log("Not 1");
        yield return new WaitForSeconds(2f);
        Debug.Log("Not 2");
        cameraContainer = new GameObject("Camera Container");
        cameraContainer.transform.position = new Vector3(0, 0, 0);
        transform.SetParent(cameraContainer.transform);

        EnableGyro();
    }
}
