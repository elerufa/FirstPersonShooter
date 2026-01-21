using UnityEngine;

public class MouseLook : MonoBehaviour
{

    public float mouseSensibility = 100f;

    public Transform playerBody;

    float xRotation;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; //il cursore è bloccato al centro dello schermo
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensibility * Time.deltaTime; //il valore di rotazione sull'asse x e y prese dall'input manager per la sensibilità del mouse per frame rate
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensibility * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.localRotation = (Quaternion.Euler(xRotation, 0f, 0f));
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
