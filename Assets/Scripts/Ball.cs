﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {
    // Config params
    [SerializeField] Paddle paddle1;
    [SerializeField] Vector2 launchVelocity;
    [SerializeField] AudioClip[] ballSounds;

    // state
    Vector2 paddleToBallVector;
    bool hasStarted = false;

    // Cached component references 
    AudioSource myAudioSource;

    // Start is called before the first frame update
    void Start() {
        paddleToBallVector = transform.position - paddle1.transform.position;
        myAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {
        if (!hasStarted) {
            LockBallToPaddle();
            LaunchOnClick();
        }
    }

    private void LaunchOnClick() {
        if (Input.GetMouseButtonDown(0)) {
            GetComponent<Rigidbody2D>().velocity = launchVelocity;
            hasStarted = true;
        }
    }

    private void LockBallToPaddle() {
        Vector2 paddlePos = new Vector2(paddle1.transform.position.x, paddle1.transform.position.y);
        transform.position = paddlePos + paddleToBallVector;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (hasStarted && ballSounds.Length!=0) {
            int index = UnityEngine.Random.Range(0, ballSounds.Length);
            AudioClip clip = ballSounds[index];
            if (clip == null) Debug.Log("clip is null");
            else if (myAudioSource == null) Debug.Log("myAudioSource is null");
            else myAudioSource.PlayOneShot(clip);
        }
        
    }
}
