using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour {
    // Config Params
    [SerializeField] AudioClip breakSound;
    [SerializeField] GameObject blockSparklesVFX;
    [SerializeField] Sprite[] hitSprites;

    // Cached Reference
    Level level;

    // State Variables
    [SerializeField] int timesHit = 0; // TODO: only serialized for debug purposes


    private void Start() {
        CountBreakableBlocks();
    }

    private void CountBreakableBlocks() {
        level = FindObjectOfType<Level>();
        if (tag == "Breakable") {
            level.CountBlocks();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (tag == "Breakable") {
            HandleHit();
        }
    }

    private void HandleHit() {
        timesHit++;
        int maxHits = hitSprites.Length + 1;
        if (timesHit >= maxHits) {
            DestroyBlock();
        } else {
            ShowNextHitSprite();
        }
    }

    private void ShowNextHitSprite() {
        int spriteIndex = timesHit - 1;
        if (hitSprites[spriteIndex] != null) {
            GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
        } else {
            Debug.LogError("Block Sprite is missing from array." + gameObject.name);
        }
    }

    private void DestroyBlock() {
        PlayBlockDestorySFX();
        TriggerSparklesVFX();
        level.BlockDestroyed();
        FindObjectOfType<GameSession>().AddToScore();
        Destroy(gameObject);
    }

    private void PlayBlockDestorySFX() {
        AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position, 1);
    }

    private void TriggerSparklesVFX() {
        GameObject sparkles = Instantiate(blockSparklesVFX, transform.position, transform.rotation);
        Destroy(sparkles, 1f);
    }
}
