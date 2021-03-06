﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlantTopState : IEnemyState
{
	private Enemy enemy;

	private float idleTimer;
	private float idleDuration;
	private const int IDLETOP = 1, IDLEBOT = -1, IDLERIGHT = 2, IDLESLANTTOP = 3, IDLESLANTBOT = -3;
	private float speedX = 1, speedY = 1;
	public void Execute()
	{
		//Idle(1);

		//if (enemy.Target != null)
		//{
		//enemy.ChangeState(new PatrolState());
		//}
		StartState();
	}

	public void Enter(Enemy enemy)
	{
		//idleDuration = UnityEngine.Random.Range(1, 10);
		this.enemy = enemy;
	}

	public void Exit()
	{

	}

	public void OnTriggerEnter(Collider2D other)
	{

	}

	public void StartState()
	{
		//	enemy.mAnimator.SetInt("Speed", speed);
		enemy.mAnimator.SetInteger("Speed", 3);
		enemy.mAnimator.SetFloat("SpeedX", 1);

	}
}

