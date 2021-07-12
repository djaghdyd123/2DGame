using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class PlayerController : MonoBehaviour
{
    public Grid _grid;
    public float _speed = 3.0f;
    Animator _animator;
    bool _isMoving = false;
    Vector3Int _cellPos = Vector3Int.zero;


    MoveDir _dir = Define.MoveDir.Down;

    public MoveDir Dir
    {
        get { return _dir; }
        set
        {
            if (_dir == value)
                return;
            switch(value)
            {
                case MoveDir.Up:
                    _animator.Play("WALK_BACK");
                    transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                    break;
                case MoveDir.Down:
                    _animator.Play("WALK_FRONT");
                    transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                    break;
                case MoveDir.Right:
                    _animator.Play("WALK_RIGHT");
                    transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                    break;
                case MoveDir.Left:
                    _animator.Play("WALK_RIGHT");
                    transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
                    break;
                case MoveDir.None:
                    if(_dir == MoveDir.Up)
                    {
                        _animator.Play("Idle_BACK");
                        transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                    }
                    else if(_dir == MoveDir.Down)
                    {
                        _animator.Play("Idle_FRONT");
                        transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                    }
                    else if (_dir == MoveDir.Right)
                    {
                        _animator.Play("Idle_RIGHT");
                        transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                    }
                    else
                    {
                        _animator.Play("Idle_RIGHT");
                        transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
                    }
                    break;
            }
            _dir = value;
        }
    }
    void Start()
    {
        _animator = GetComponent<Animator>();
        Vector3 pos = _grid.CellToWorld(_cellPos) + new Vector3(0.5f, 0.5f);
        transform.position = pos;
    }

    void Update()
    {
        GetDirInput();
        UpdateMove();
        UpdatePos();

    }

    void GetDirInput()
    {
        if (Input.GetKey(KeyCode.W))
        {
            //transform.position += Vector3.up * Time.deltaTime * _speed;
            Dir = MoveDir.Up;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            //transform.position += Vector3.left * Time.deltaTime * _speed;
            Dir = MoveDir.Left;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            //transform.position += Vector3.down * Time.deltaTime * _speed;
            Dir = MoveDir.Down;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            //transform.position += Vector3.right * Time.deltaTime * _speed;
            Dir = MoveDir.Right;
        }
        else
        {
            Dir = MoveDir.None;
        }
    }

    void UpdateMove()
    {
        if (_isMoving == false)
        {
            switch (_dir)
            {
                case MoveDir.Up:
                    _cellPos += Vector3Int.up;
                    _isMoving = true;
                    break;
                case MoveDir.Down:
                    _cellPos += Vector3Int.down;
                    _isMoving = true;
                    break;
                case MoveDir.Left:
                    _cellPos += Vector3Int.left;
                    _isMoving = true;
                    break;
                case MoveDir.Right:
                    _cellPos += Vector3Int.right;
                    _isMoving = true;
                    break;
            }
        }
    }
    // UpdateMoving 에 증감된 cellPos를 목적지로해서 speed 만큼 transition 한다.
    void UpdatePos()
    {
        if (_isMoving == false)
            return;
        Vector3 _destPos = _grid.CellToWorld(_cellPos) + new Vector3(0.5f, 0.5f);
        Vector3 _moveDir = _destPos - transform.position;

        float dist = _moveDir.magnitude;

        if (dist < _speed * Time.deltaTime)
        {
            transform.position = _destPos;
            _isMoving = false;
        }
        else
        {
            transform.position += _moveDir.normalized * _speed * Time.deltaTime;
            _isMoving = true;
        }
    }
}
