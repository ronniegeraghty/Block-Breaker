using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseCollider : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision) {
        //When something passes throught the LoseCollider
        SceneManager.LoadScene("Game Over");
    }
}
