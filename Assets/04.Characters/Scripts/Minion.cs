using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
using Assets._04.Characters.Scripts;

/// <summary>
/// �Ȯɳ]�w����Ԥp�L
/// ����A���c���q�μҫ�
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
    /// �欰����
    /// </summary>
    void ActionStrategy(List<IEntity> entities)
    {
        /*�@���H���ͫᬰ��
         * ���P�_�e��(�����d��)�O�_���ͭx�B�ĭx
         * ���ͭx->����
         * ���ĭx->���U����
         * ���ĭx�^��->��������
         * �P�ɦ��ĤH�]���ͭx->���U����
         */
        //����if-else�w�F �����c
        if (entities.Count > 1)
        {
            //�p�G������ĤH
            if (entities.Any(e => { return e.GetAlignment() != _alignment; }))
            {
                Attack();
            }
            //�p�G������ͭx
            else if(entities.Any(e => { return e.GetAlignment() == _alignment; }))
            {
                //���U��
            }
            else
            {
                throw new Exception("�e������L����A���ӹ��餣���ݩ����}��A�|���B�z�C");
            }
        }
        else//�S�H�A�֩wmove��
        {
            Move();
        }
    }


    /// <summary>
    /// �˴��e�誺���馳����
    /// </summary>
    /// <returns>�^�ǹ���</returns>
    List<IEntity> CheckAttackRange()
    {
        Vector3 startPosition = transform.position;
        startPosition.x += _collider2D.bounds.size.x / 2f + 0.1f;
        //�e�@���u
        Debug.DrawLine(startPosition, transform.position + (Vector3)(_faceDirection * _attackRange));
        //�g�u�g�X
        var raycastHit2Ds = Physics2D.RaycastAll(startPosition, _faceDirection, _attackRange).ToList();
        //�^��IEntity
        return raycastHit2Ds.ConvertAll(
            (temp) =>
            {
                return temp.transform.GetComponent<IEntity>();
            }
            );
    }





    /// <summary>
    /// ����
    /// </summary>
    void Move()
    {
        transform.Translate(_movement * _faceDirection.x, 0, 0);
    }

    //���H�N��
    void Attack()
    {
        Debug.Log("����");
        //��ԧ����A����ʵe�A�ʵe��F��g�u�A�ˮ`�d�򤺩Ҧ��ĭx(�e��hurted)
    }
    //���O����^�����|���U�ӫ�
    //�䨫���
    //�]�٬� ��a�]
    void AttackMove()
    {

    }

    //���˷|�y�@�U
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