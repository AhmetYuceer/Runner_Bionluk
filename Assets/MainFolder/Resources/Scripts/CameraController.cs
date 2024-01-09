using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance;

    private Animator animator;

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
        animator.SetBool("Finish", false);
    }

    public void StarterCamera()
    {
        animator.SetBool("Start", true);        
    }

    public void StopCamera()
    {
        animator.SetBool("Start", false);
    }

    public void FinishCamera()
    {
        animator.SetBool("Finish", true);
    }
}