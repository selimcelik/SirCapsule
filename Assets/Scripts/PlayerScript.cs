using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerScript : MonoBehaviour
{
    public static PlayerScript Instance;

    public float speed = 8f;
    private Camera followCamera;

    private Rigidbody m_Rb;
    private Vector3 m_CameraPos;
    private float m_SpeedModifier = 1;

    public bool collect = false;

    private void Awake()
    {
        Instance = this;
        followCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        m_Rb = GetComponent<Rigidbody>();
        m_CameraPos = followCamera.transform.position - transform.position;
    }

    private void FixedUpdate()
    {
        if (!GameManager.Instance.levelFinish)
        {
            float horizontalInput = CrossPlatformInputManager.GetAxis("Horizontal");
            float verticalInput = CrossPlatformInputManager.GetAxis("Vertical");

            transform.position = new Vector3(Mathf.Clamp(transform.position.x, 1, 11.2f), transform.position.y, Mathf.Clamp(transform.position.z,-8.3f,15.8f));

            Vector3 playerPos = m_Rb.position;
            Vector3 movement = new Vector3(horizontalInput, 0, verticalInput).normalized;

            if (movement == Vector3.zero)
            {
                return;
            }

            Quaternion targetRotation = Quaternion.LookRotation(movement);

            targetRotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 360 * Time.fixedDeltaTime);

            m_Rb.MovePosition(playerPos + movement * m_SpeedModifier * speed * Time.fixedDeltaTime);
            m_Rb.MoveRotation(targetRotation);
        }
        
    }

    private void LateUpdate()
    {
        followCamera.transform.position = m_Rb.position + m_CameraPos;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Collectable")
        {
            collect = true;
            Destroy(other.gameObject);
        }
    }
}