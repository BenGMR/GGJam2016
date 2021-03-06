﻿using UnityEngine;
using System.Collections;
using Spine;
using UnityEngine.SceneManagement;

public class GameExplosionScript : MonoBehaviour
{
    SkeletonRagdoll2D skeletonToExplode;
    
    bool exploded = false;
    bool explosionSoundPlayed = false;

    void Start()
    {

    }

    void Update()
    {
        if (exploded)
        {

            if (Input.GetButton("B1") || Input.GetKeyDown(KeyCode.B))
            {
                CanvasManager.scenes.Clear();
                CanvasManager.scenes.Push(SceneManager.GetSceneByName("TeamSelector"));
                SceneManager.LoadScene("TeamSelector", LoadSceneMode.Single);
            }
            else if (Input.GetButton("A1") || Input.GetKeyDown(KeyCode.A))
            {
                CanvasManager.scenes.Clear();
                CanvasManager.scenes.Push(SceneManager.GetSceneByName("Game"));
                SceneManager.LoadScene("Game", LoadSceneMode.Single);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        //I want the character to float to line up with the moon
        //once the character is in the center of the moon they explode
        //1) Disable top hitbox
        //2) Line up moon x-coordinate with character x-coordinate. Or look into smoothstepping (Vector2.MoveTowards)
        //3) When character gets close enough to the moon they explode
        exploded = true;



        skeletonToExplode = collider.transform.parent.GetComponent<SkeletonRagdoll2D>();

        StartCoroutine(Explode());
    }

    IEnumerator Explode()
    {
        yield return new WaitForSeconds(1);
        foreach (Bone b in skeletonToExplode.boneTable.Keys)
        {
            Destroy(skeletonToExplode.boneTable[b].GetComponent<Joint2D>());
            //generate random force
            Vector2 randomForce = new Vector2(Random.Range(-1000f, 1000f), Random.Range(-1000f, 1000f));

            //apply it bro
            skeletonToExplode.boneTable[b].GetComponent<Rigidbody2D>().AddForce(randomForce);
        }
        if (!explosionSoundPlayed)
        {
            if (SoundManager.instance != null)
            {
                SoundManager.instance.PlaySound("Lightning");
            }
            explosionSoundPlayed = true;
        }
    }
}

