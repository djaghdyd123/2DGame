using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class MonsterController : CreatureController
{
    Coroutine _coPatrol;
    Coroutine _coSkill;
    Coroutine _coSearch;
    [SerializeField]
    Vector3Int _destCellPos;

    [SerializeField]
    GameObject _target;

    [SerializeField]
    float _searchRange = 5.0f;

    [SerializeField]
    float _skillRange = 1.0f;

    [SerializeField]
    bool _rangedSkill;

    public override CreatureState State
    {
        get { return _state; }
        set
        {
            if (_state == value)
                return;

            base.State = value;

            if (_coPatrol != null)
            {
                StopCoroutine(_coPatrol);
                _coPatrol = null;
            }
            if (_coSearch != null)
            {
                StopCoroutine(_coSearch);
                _coSearch = null;
            }
        }
    }

    protected override void Init()
    {
        base.Init();
        State = Define.CreatureState.Idle;
        Dir = Define.MoveDir.None;
        _speed = 3.0f;

        _rangedSkill = Random.Range(0, 2) == 0 ? true : false;

        if (_rangedSkill)
            _skillRange = 10.0f;
    }

    protected override void UpdateIdle()
    {
        base.UpdateIdle();

        if (_coPatrol == null)
        {
            _coPatrol = StartCoroutine("CoPatrol");
        }
        if (_coSearch == null)
        {
            _coSearch = StartCoroutine("CoSearch");
        }
    }

    protected override void MoveToNextPos()
    {
        //패트롤에서 받아온 목표지nextPos
        Vector3Int destPos = _destCellPos;
        // target 이 있는 경우
        if (_target != null)
        {
            destPos = _target.GetComponent<CreatureController>().CellPos;

            Vector3Int dir = destPos - CellPos;
            if (dir.magnitude <= _skillRange && (dir.x ==0 || dir.y ==0))
            {
                Dir = getDirFromVec(dir);
                State = CreatureState.Skill;
                if(_rangedSkill)
                {
                    _coSkill = StartCoroutine("CoStartShootArrow");
                    return;
                }
                else
                {
                    _coSkill = StartCoroutine("CoStartPunch");
                    return;
                }
                
            }
        }

        List<Vector3Int> path = Managers.Map.FindPath(CellPos, destPos, ignoreDestCollision: true);

        //길이 아예 막혔거나 , 플레이어가 멀리 도망간 경우
        if(path.Count < 2 || (_target && path.Count > 15))
        {
            _target = null;
            State = CreatureState.Idle;
            return;
        }

        Vector3Int nextPos = path[1];
       

        Vector3Int moveCellDir = nextPos - CellPos;
         Dir = getDirFromVec(moveCellDir);

        if (Managers.Map.CanGo(nextPos) && Managers.Object.Find(nextPos) == null)
        {
                CellPos = nextPos;  
        }
        else
        {
            State = CreatureState.Idle;
        }
    }
        
    protected override void UpdateController()
    {

        base.UpdateController();

    }

    public override void OnDamaged()
    {
        GameObject effect = Managers.Resources.Instantiate("Effect/DieEffect");
        effect.transform.position = transform.position;
        effect.GetComponent<Animator>().Play("Start");
        GameObject.Destroy(effect, 0.5f);

        Managers.Object.Remove(Id);
        Managers.Resources.Destroy(Id);
    }

    IEnumerator CoPatrol()
    {
       
            int waitSeconds = Random.Range(1, 4);
            yield return new WaitForSeconds(waitSeconds);

            for (int i = 0; i < 10; i++)
            {
                int xRange = Random.Range(-8, 8);
                int yRange = Random.Range(-4, 4);
                Vector3Int randPos = CellPos + new Vector3Int(xRange, yRange, 0);

                if (Managers.Map.CanGo(randPos) && Managers.Object.Find(randPos) == null)
                {
                    _destCellPos = randPos;
                    State = Define.CreatureState.Moving;
                    yield break; // Corutine 에서 나가기
                }
            }
            //State = Define.CreatureState.Idle;


    }

    IEnumerator CoSearch()
    {
 
        while (true)
        {
            yield return new WaitForSeconds(1);

            if (_target != null)
                continue;

            // 람다 인자 go 는 호출 함수 Find 내부에 람다가 호출될때 인자 (obj)
            // 1초 마다 범위 안에 플레이어를 찾아 _target에 넣어줌.
           _target = Managers.Object.Find(go =>
            {
                PlayerController pc = go.GetComponent<PlayerController>();
                if (pc == null)
                    return false;

                Vector3Int dir = (pc.CellPos - CellPos);
                if (dir.magnitude > _searchRange)
                    return false;

                return true;
            });

        }
    }

    IEnumerator CoStartPunch()
    {

        GameObject go = Managers.Object.Find(GetFrontCellPos());
        if (go != null)
        {
            CreatureController cc = go.GetComponent<CreatureController>();
            if (cc != null) cc.OnDamaged();
        }
        yield return new WaitForSeconds(0.5f);
        // Idle 설정 시 coPatrol 로 인한 Delay
        State = CreatureState.Moving;
        _coSkill = null;
    }

    IEnumerator CoStartShootArrow()
    {
        GameObject go = Managers.Resources.Instantiate("Creature/Arrow");
        ArrowController ac = go.GetComponent<ArrowController>();
        ac.Dir = _lastDir;
        ac.CellPos = CellPos;

        //대기 시간
        yield return new WaitForSeconds(0.3f);

        // Idle 설정 시 coPatrol 로 인한 Delay
        State = CreatureState.Moving;
        _coSkill = null;
    }

}
