using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
using Assets._04.Characters.Scripts;

/// <summary>
/// 暫時設定為近戰小兵
/// 之後再重構成通用模型
/// </summary>
public class Minion : MonoBehaviour, IEntity, IMinion, ISpawnable
{
    private Collider2D _collider2D;
    [SerializeField] private EntityType _type;
    [SerializeField] private int _healthPoint;
    [SerializeField] private float _attackSpeed;
    [SerializeField] private int _attack;
    [SerializeField] private float _attackRange;
    private Vector2 _faceDirection;
    [SerializeField] private int _alignment;
    [SerializeField] private int _spawnResource;
    [SerializeField] private int _spawnTime;
    [SerializeField] private float _movement;

    void Awake()
    {
        _collider2D = GetComponent<Collider2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _faceDirection = (_alignment == 0 ? Vector2.right : Vector2.left);
    }

    // Update is called once per frame
    void Update()
    {
        var entities = CheckAttackRange();
        entities.ForEach(x => Debug.Log(x));
        ActionStrategy(entities);
    }

    /// <summary>
    /// 行為策略
    /// </summary>
    void ActionStrategy(List<IEntity> entities)
    {
        /*一切以重生後為準
         * 先判斷前方(攻擊範圍)是否有友軍、敵軍
         * 有友軍->等待
         * 有敵軍->停下攻擊
         * 有敵軍英雄->攻擊移動
         * 同時有敵人也有友軍->停下攻擊
         */
        //先用if-else硬幹 等重構
        if (entities.Count > 1)
        {
            //如果有任何敵人
            if (entities.Any(e => { return e.GetAlignment() != _alignment; }))
            {
                Attack();
            }
            //如果有任何友軍
            else if(entities.Any(e => { return e.GetAlignment() == _alignment; }))
            {
                //停下來
            }
            else
            {
                throw new Exception("前面有其他實體，但該實體不隸屬於任何陣營，尚未處理。");
            }
        }
        else//沒人，肯定move的
        {
            Move();
        }
    }


    /// <summary>
    /// 檢測前方的實體有哪些
    /// </summary>
    /// <returns>回傳實體</returns>
    List<IEntity> CheckAttackRange()
    {
        Vector3 startPosition = transform.position;
        startPosition.x += _collider2D.bounds.size.x / 2f + 0.1f;
        //畫一條線
        Debug.DrawLine(startPosition, transform.position + (Vector3)(_faceDirection * _attackRange));
        //射線射出
        var raycastHit2Ds = Physics2D.RaycastAll(startPosition, _faceDirection, _attackRange).ToList();
        //回傳IEntity
        return raycastHit2Ds.ConvertAll(
            (temp) =>
            {
                return temp.transform.GetComponent<IEntity>();
            }
            );
    }





    /// <summary>
    /// 走路
    /// </summary>
    void Move()
    {
        transform.Translate(_movement * _faceDirection.x, 0, 0);
    }

    //見人就扁
    void Attack()
    {
        Debug.Log("攻擊");
        //近戰攻擊，撥放動畫，動畫到了放射線，傷害範圍內所有敵軍(委派hurted)
    }
    //但是見到英雄不會停下來扁
    //邊走邊扁
    //也稱為 扁帶跑
    void AttackMove()
    {

    }

    //受傷會頓一下
    void Hurted()
    {

    }


    int IEntity.GetAlignment()
    {
        return _alignment;
    }

    void IEntity.Hurted(int damage)
    {
        throw new NotImplementedException();
    }

    Vector2 IEntity.GetPostion()
    {
        return transform.position;
    }

    int ISpawnable.GetSpawnTime()
    {
        return _spawnTime;
    }

    string IEntity.GetName()
    {
        return name;
    }

    EntityType IEntity.GetEntityType()
    {
        return _type;
    }
}