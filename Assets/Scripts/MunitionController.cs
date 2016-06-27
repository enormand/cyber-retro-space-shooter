﻿using UnityEngine;
using System.Collections;

public class MunitionController : MonoBehaviour {

	public float damageFactor = 1f;
	public float fireForce = 100f;
	public float destroyBelowVelocity = 1f;

	void FixedUpdate () {
		DestroyBelowVelocity ();
	}

	/*
	 * Destroy the munition if it's velocity is below the threshold 'destroyBelowVelocity'.
	 */
	void DestroyBelowVelocity () {
		float velocityMagnitude = gameObject.GetComponent<Rigidbody> ().velocity.magnitude;
		if (velocityMagnitude < destroyBelowVelocity 
			&& velocityMagnitude != 0) { // Don't destroy it at it's creation, before the force has been applied by the weapon
			Destroy (gameObject);
		}
	}

	/*
	 * Hit every object with a LifeController and inflict damages.
	 */
	void OnCollisionEnter (Collision colision) {
		LifeController otherLife = colision.gameObject.GetComponent<LifeController> ();
		if (otherLife != null) {
			otherLife.Hit (damageFactor);
		}
		Destroy (this.gameObject);
	}
}