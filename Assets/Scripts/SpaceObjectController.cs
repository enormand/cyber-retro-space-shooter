﻿using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public abstract class SpaceObjectController : MonoBehaviour {

	public LifeController life;

	private UnityEvent lifeEvents;

	protected virtual void Start () {
		lifeEvents = new UnityEvent();
		lifeEvents.AddListener (LifeObserver);
		life.AddObserver (lifeEvents);

		LifeObserver(); // Init the LifeController infos in the HUD
	}

	/*
	 * Manage the notifications of the LifeController.
	 */
	protected abstract void LifeObserver ();
}
