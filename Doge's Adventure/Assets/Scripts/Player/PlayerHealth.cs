﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
	public int startingHealth = 100;
	public int currentHealth;
	public Slider healthSlider;
	public Image damageImage;
	public AudioClip deathClip;
	public AudioClip hurtClip;
	public float flashSpeed = 5f;
	public Color flashColour = new Color (1f, 0f, 0f, 0.1f);
	Animator anim;
	AudioSource playerAudio;
	PlayerMovement playerMovement;
	PlayerShooting playerShooting;
	bool isDead;
	bool damaged;
	

	void Awake ()
	{
		anim = GetComponent <Animator> ();
		playerAudio = GetComponent <AudioSource> ();
		playerMovement = GetComponent <PlayerMovement> ();
		playerShooting = GetComponentInChildren <PlayerShooting> ();
		currentHealth = startingHealth;

	}

	void Update ()
	{
		if (damaged) {
			damageImage.color = flashColour;
		} else {
			damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
		}
		damaged = false;


	}

	public void TakeDamage (int amount)
	{
		damaged = true;

		currentHealth -= amount;
		Debug.Log ("current heal" + currentHealth);
		healthSlider.value = currentHealth;

		playerAudio.clip = hurtClip;
		playerAudio.Play ();

		if (currentHealth <= 0 && !isDead) {
			Death ();
		}
	}

	public void Win() {
		playerMovement.enabled = false;
		playerShooting.enabled = false;
	}

	void Death ()
	{
		isDead = true;

		playerShooting.DisableEffects ();

		anim.SetTrigger ("Die");

		playerAudio.clip = deathClip;
		playerAudio.Play ();

		playerMovement.enabled = false;
		playerShooting.enabled = false;
	}

	public void RestartLevel ()
	{
		//Application.LoadLevel (Application.loadedLevel);
	}

	/*void OnCollisionEnter (Collision col)
	{
		if(col.gameObject.tag == "GreenBuff")
		{
			currentHealth += 2;
		}
	}*/
}
