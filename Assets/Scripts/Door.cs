using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool openDoorAnimationDone = false;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();    
    }

    public void OpenDoor()
    {
        animator.SetTrigger("open");
    }

    public void OpenDoorBool()
    {
        animator.SetBool("openDoor", true);
    }

    public void CloseDoorBool()
    {
        animator.SetBool("openDoor", false);
    }

    public void DoorGone()
    {
        animator.SetTrigger("gone");
    }

    public void UpAgain()
    {
        animator.SetTrigger("upAgain");
    }

    public void ResetUpAgain()
    {
        animator.ResetTrigger("upAgain");
    }

    public void OpenDoorAnimationDone()
    {
        openDoorAnimationDone = true;
    }

    public void OpenDoorAnimationNotDone()
    {
        openDoorAnimationDone = false;
    }
}
