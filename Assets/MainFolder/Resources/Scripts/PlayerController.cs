using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    [SerializeField] private float speedForward;
    [SerializeField] private float speedHorizontal;
    [SerializeField] private float currentForwardSpeed;

    [SerializeField] private float jumpForce; 
    [SerializeField] private float jumpDelay; 

    //Animator
    private Animator animator;
    private Rigidbody rb;
    
    private bool isRun;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        currentForwardSpeed = speedForward;
    }

    private void Update()
    {
        if (isRun)
        {
            float Horizontal = Input.GetAxis("Horizontal");
           
            transform.position += transform.right * Horizontal * speedHorizontal * Time.deltaTime;
            transform.position += transform.forward * speedForward * Time.deltaTime;
            
            if (Input.GetKeyDown(KeyCode.Space) && transform.position.y < 2.2f)
            {
                StartCoroutine(Jump());
            }
        }
    }

    IEnumerator Jump()
    {
        animator.SetTrigger("Jump");
        speedForward = currentForwardSpeed - 1;
        yield return new WaitForSeconds(jumpDelay);
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        speedForward = currentForwardSpeed;
    }

    public void StartingMoveing()
    {
        CameraController.Instance.StarterCamera();
        
        isRun = true;
        animator.SetBool("Run", true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.gameObject.CompareTag("End"))
        {
            animator.SetBool("Dance",true);
            speedForward = 0;
            speedHorizontal = 0;
            GameManager.Instance.OpenRestartPanel();
            CameraController.Instance.FinishCamera();
        }
        else if (other.transform.gameObject.CompareTag("Strawberry"))
        {
            GameManager.Instance.AddScore(other.transform.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Obstacle"))
        {
            CameraController.Instance.StopCamera();
            animator.SetBool("Angry", true);
            GameManager.Instance.crashSound();
            speedForward = 0;
            speedHorizontal = 0;
            GameManager.Instance.OpenRestartPanel();
        }   
    }
}