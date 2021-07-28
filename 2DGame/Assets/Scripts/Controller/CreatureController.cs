﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class CreatureController : MonoBehaviour
{
	public float _speed = 5.0f;

	public Vector3Int CellPos { get; set; } = Vector3Int.zero;
	protected Animator _animator;
	protected SpriteRenderer _sprite;

	protected CreatureState _state = CreatureState.Idle;

	public virtual CreatureState State
	{
		get { return _state; }
		set
		{
			if (_state == value)
				return;

			_state = value;
			UpdateAnimation();
		}
	}

	protected MoveDir _lastDir = MoveDir.Down;
	protected MoveDir _dir = MoveDir.Down;

	public MoveDir Dir
	{
		get { return _dir; }
		set
		{
			if (_dir == value)
				return;

			_dir = value;
			if (value != MoveDir.None)
				_lastDir = value;

			UpdateAnimation();
		}
	}
	public MoveDir getDirFromVec(Vector3Int dir)
    {
		if (dir.x > 0)
			return MoveDir.Right;
		else if (dir.x < 0)
			return MoveDir.Left;
		else if (dir.y > 0)
			return MoveDir.Up;
		else if (dir.y < 0)
			return MoveDir.Down;
		else
			return MoveDir.None;
	}
	public Vector3Int GetFrontCellPos()
    {
		Vector3Int _cellPos = CellPos;
		switch(_lastDir)
        {
			case MoveDir.Up:
				_cellPos += Vector3Int.up;
				break;
			case MoveDir.Down:
				_cellPos += Vector3Int.down;
				break;
			case MoveDir.Left:
				_cellPos += Vector3Int.left;
				break;
			case MoveDir.Right:
				_cellPos += Vector3Int.right;
				break;
		}
		return _cellPos;
	
    }
	protected virtual void UpdateAnimation()
	{
		if (_state == CreatureState.Idle)
		{
			switch (_lastDir)
			{
				case MoveDir.Up:
					_animator.Play("IDLE_BACK");
					_sprite.flipX = false;
					break;
				case MoveDir.Down:
					_animator.Play("IDLE_FRONT");
					_sprite.flipX = false;
					break;
				case MoveDir.Left:
					_animator.Play("IDLE_RIGHT");
					_sprite.flipX = true;
					break;
				case MoveDir.Right:
					_animator.Play("IDLE_RIGHT");
					_sprite.flipX = false;
					break;
			}
		}
		else if (_state == CreatureState.Moving)
		{
			switch (_dir)
			{
				case MoveDir.Up:
					_animator.Play("WALK_BACK");
					_sprite.flipX = false;
					break;
				case MoveDir.Down:
					_animator.Play("WALK_FRONT");
					_sprite.flipX = false;
					break;
				case MoveDir.Left:
					_animator.Play("WALK_RIGHT");
					_sprite.flipX = true;
					break;
				case MoveDir.Right:
					_animator.Play("WALK_RIGHT");
					_sprite.flipX = false;
					break;
			}
		}
		else if (_state == CreatureState.Skill)
		{
			switch (_lastDir)
			{
				case MoveDir.Up:
					_animator.Play("ATTACK_BACK");
					_sprite.flipX = false;
					break;
				case MoveDir.Down:
					_animator.Play("ATTACK_FRONT");
					_sprite.flipX = false;
					break;
				case MoveDir.Left:
					_animator.Play("ATTACK_RIGHT");
					_sprite.flipX = true;
					break;
				case MoveDir.Right:
					_animator.Play("ATTACK_RIGHT");
					_sprite.flipX = false;
					break;
			}
		}
		else
		{

		}
	}

	void Start()
	{
		Init();
	}

	void Update()
	{
		UpdateController();
	}

	
	protected virtual void Init()
	{
		_animator = GetComponent<Animator>();
		_sprite = GetComponent<SpriteRenderer>();
		Vector3 pos = Managers.Map.CurrentGrid.CellToWorld(CellPos) + new Vector3(0.5f, 0.5f);
		transform.position = pos;
	}

	protected virtual void UpdateController()
	{
		switch (State)
		{
			case CreatureState.Idle:
				UpdateIdle();
				break;
			case CreatureState.Moving:
				UpdateMoving();
				break;
			case CreatureState.Skill:
				UpdateSkill();
				break;
			case CreatureState.Dead:
				UpdateDead();
				break;
		}


	}

	// CellPos 로 스르륵 이동하는 것을 처리
	protected virtual void UpdateMoving()
	{
		if (State != CreatureState.Moving)
			return;

		Vector3 destPos = Managers.Map.CurrentGrid.CellToWorld(CellPos) + new Vector3(0.5f, 0.5f);
		Vector3 moveDir = destPos - transform.position;

		// 도착 여부 체크
		float dist = moveDir.magnitude;
		if (dist < _speed * Time.deltaTime)
		{
			transform.position = destPos;
			MoveToNextPos();
			// 예외적으로 애니메이션을 직접 컨트롤
		}
		else
		{
			transform.position += moveDir.normalized * _speed * Time.deltaTime;
			State = CreatureState.Moving;
		}
	}

	protected virtual void MoveToNextPos()
    {
		if(_dir == MoveDir.None)
        {
			State = CreatureState.Idle;
			return;
        }

		// 계속 무빙 상태에 dir를 받아서 이동

		Vector3Int destPos = CellPos;

		switch (_dir)
		{
			case MoveDir.Up:
				destPos += Vector3Int.up;
				break;
			case MoveDir.Down:
				destPos += Vector3Int.down;
				break;
			case MoveDir.Left:
				destPos += Vector3Int.left;
				break;
			case MoveDir.Right:
				destPos += Vector3Int.right;
				break;
		}


		if (Managers.Map.CanGo(destPos))
		{
			if (Managers.Object.Find(destPos) == null)
			{
				CellPos = destPos;
			}
		}
	}

	protected virtual void UpdateIdle()
	{
	
	}

	protected virtual void UpdateSkill()
	{

	}
	protected virtual void UpdateDead()
	{

	}

	public virtual void OnDamaged()
    {

	}
}