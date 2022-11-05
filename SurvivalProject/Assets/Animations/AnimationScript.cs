using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AnimationScript : MonoBehaviour
{
    private Animator mAnimator;
    // Start is called before the first frame update
    void Start()
    {
        mAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(mAnimator != null) {
            if(Input.GetKeyDown(KeyCode.W)) {
                mAnimator.SetFloat("Speed", 0.5f, 0.15f, Time.deltaTime);
            }
            if(Input.GetKeyUp(KeyCode.W)) {
                mAnimator.SetFloat("Speed", 0, 0.15f, Time.deltaTime);
            }
        }
    }

    private void Idle()
    {
        mAnimator.SetFloat("Speed", 0, 0.15f, Time.deltaTime);
    }

    private void Walk()
    {
        mAnimator.SetFloat("Speed", 0.5f, 0.15f, Time.deltaTime);
    }

    private void Run()
    {
        mAnimator.SetFloat("Speed", 1, 0.15f, Time.deltaTime);
    }
}
