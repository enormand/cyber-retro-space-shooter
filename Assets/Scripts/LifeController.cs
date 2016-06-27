﻿using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class LifeController : MonoBehaviour {

	public float lifePoints = 1f;

	public float shieldCapacity = 1f;
	public float shieldRechargeDelay = 1f;
	public float shieldRechargeRatePerSecond = 0.1f;

	private float lastHitTime = 0f;
	private float shieldPoints = 1f;

	private UnityEvent observer;

	void Start () {
		shieldPoints = shieldCapacity;
	}

	void Update () {
		RechargeShield ();
	}

	/*
	 * Add an observer which be notified if the life drops to zero.
	 */
	public void AddObserver (UnityEvent observer) {
		this.observer = observer;
	}

	/*
	 * The shield takes the damages, then the life. If life drops to zero, inform the observer.
	 */
	public void Hit (float damageAmount) {
		lastHitTime = Time.time;

		if (shieldPoints > 0) {
			shieldPoints -= damageAmount;
			return;
		}
		lifePoints -= damageAmount;

		if (lifePoints <= 0f) {
			if (observer != null) {
				Debug.LogError (name + " (LifeController): no observer set.";
			} else {
				observer.Invoke ();
			}
		}
	}

	/*
	 * After a 'shieldRechargeDelay', recharge the shield at a shield rate. Should be called every frame by Update().
	 */
	void RechargeShield () {
		if (shieldPoints == shieldCapacity || lastHitTime + shieldRechargeDelay > Time.time) {
			return;
		}

		shieldPoints += shieldRechargeRatePerSecond * Time.deltaTime;
		shieldPoints = Mathf.Clamp(shieldPoints, 0f, shieldCapacity);
	}
}
