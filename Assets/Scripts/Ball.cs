using UnityEngine;

public class Ball : MonoBehaviour {
    // Config params
    [SerializeField] Paddle paddle1;
    [SerializeField] Vector2 launchVelocity;
    [SerializeField] AudioClip[] ballSounds;
    [SerializeField] float randomFactor = 0.2f;

    // state
    Vector2 paddleToBallVector;
    bool hasStarted = false;

    // Cached component references 
    AudioSource myAudioSource;
    Rigidbody2D myRigidBody2D;

    // Start is called before the first frame update
    void Start() {
        paddleToBallVector = transform.position - paddle1.transform.position;
        myAudioSource = GetComponent<AudioSource>();
        myRigidBody2D = GetComponent<Rigidbody2D>();
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
            myRigidBody2D.velocity = launchVelocity;
            hasStarted = true;
        }
    }

    private void LockBallToPaddle() {
        Vector2 paddlePos = new Vector2(paddle1.transform.position.x, paddle1.transform.position.y);
        transform.position = paddlePos + paddleToBallVector;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        Vector2 velocityTweak = new Vector2
            (Random.Range(0, randomFactor),
            Random.Range(0, randomFactor));

        if (hasStarted && ballSounds.Length != 0) {
            int index = Random.Range(0, ballSounds.Length);
            AudioClip clip = ballSounds[index];
            if (clip == null) Debug.Log("clip is null");
            else if (myAudioSource == null) Debug.Log("myAudioSource is null");
            else myAudioSource.PlayOneShot(clip);
            myRigidBody2D.velocity += velocityTweak;
        }

    }
}
