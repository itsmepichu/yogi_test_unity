using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour {
    public float thrust = 0.0f;
    public Animator playerAnimator;

    public AudioClip
        running,
        jumping;

    private bool didPlayerJumped = false;
    private bool turnGravity = false;
    private bool changeAudio = false;
    private AudioSource playerAudioSource;
    private game gameControllerScript;

	// Use this for initialization
	void Start () {
        playerAnimator = GetComponent<Animator>();
        playerAudioSource = GetComponent<AudioSource>();
        gameControllerScript = GameObject.Find("_game_controller").GetComponent<game>();

        playerAudioSource.clip = running;
        playerAudioSource.loop = true;
        playerAudioSource.Play();

        StartCoroutine(updatePlayerScore());
    }
	
	// Update is called once per frame
	void Update () {

        // handling player jump logic here
        if (Input.GetButtonDown("Fire1") && !didPlayerJumped)
        {
            // play jump animation
            didPlayerJumped = true;
            changeAudio = true;
            playerAnimator.SetBool("isJumped", true);
        }
        if(didPlayerJumped && !turnGravity)
        {
            transform.Translate(Vector3.up * thrust * Time.deltaTime);

            // play jump sound only once
            if(changeAudio)
            {
                playerAudioSource.clip = jumping;
                playerAudioSource.loop = false;
                playerAudioSource.Play();
                changeAudio = false;
            }
        }
        if(transform.position.y>1.5f || turnGravity)
        {
            turnGravity = true;
            transform.Translate(Vector3.down * thrust * 1.05f * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.gameObject.tag);
        if(collision.gameObject.tag == "terrain" && didPlayerJumped)
        {
            // resume running animation after jump is finished
            playerAnimator.SetBool("isJumped", false);
            didPlayerJumped = false;
            turnGravity = false;

            // resume running sound after jump is finished
            playerAudioSource.clip = running;
            playerAudioSource.loop = true;
            playerAudioSource.Play();

        }

        if(collision.gameObject.tag == "obstacle")
        {
            gameControllerScript.gameOver();
        }
    }

    private IEnumerator updatePlayerScore()
    {
        while(true)
        {
            // updating player score for every 1 second
            gameControllerScript.updateScore(10);
            yield return new WaitForSeconds(1.0f);
        }
    }
}
