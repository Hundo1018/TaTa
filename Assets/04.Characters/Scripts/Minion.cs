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
    private Rigidbody2D _rigidbody2D;
    [SerializeField] private EntityType _type;
    [SerializeField] private int _healthPoint;
    [SerializeField] private float _attackSpeed;
    [SerializeField] private int _attack;
    [SerializeField] private float _attackRange;
    private Vector2 _faceDirection;
    /// <summary>
    /// �}��
    /// </summary>
    [SerializeField] private int _alignment;
    [SerializeField] private int _spawnResource;
    [SerializeField] private int _spawnTime;
    [SerializeField] private float _movement;
    /// <summary>
    /// ��U���ʳt�v
    /// </summary>
    [SerializeField] private float _velocity;
    [SerializeField] private float _spawnedTime;

    void Awake()
    {
        _collider2D = GetComponent<Collider2D>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spawnedTime = Time.time;
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
        if (entities.Count > 0)
        {
            //�p�G������ĤH
            if (entities.Any(e => { return e.GetAlignment() != _alignment; }))
            {
                Attack();
            }
            //�p�G������D�ۤv���ͭx
            else if(entities.Any(e => { return e.GetAlignment() == _alignment; }))
            {
                //���Ӥͭx��ۤv�٦ѴN���U�����L����
                if (entities.Any(e => { return e.GetAlignment() == _alignment && e.GetSpawnedTime() < _spawnedTime; }))
                    _rigidbody2D.velocity = Vector2.zero;
                else
                    Move();
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
    /// <returns>�^�Ǥ��]�t�ۤv������</returns>
    private List<IEntity> CheckAttackRange()
    {
        Vector3 startPosition = transform.position;
        Vector3 endPosition = transform.position + (Vector3)(_faceDirection * _attackRange);
        endPosition.x += _collider2D.bounds.size.x / 2f;
        //�e�@���u
        Debug.DrawLine(startPosition, endPosition);
        //�g�u�g�X
        var raycastHit2Ds = Physics2D.RaycastAll(startPosition, _faceDirection, _attackRange + _collider2D.bounds.size.x / 2f).ToList();

        //�Ǵ���IEntity
        var entities = raycastHit2Ds.ConvertAll(
            (temp) =>
            {
                return temp.transform.GetComponent<IEntity>();
            }
            );
        //�簣�ۤv
        entities.RemoveAll(x =>
        {
            return x.GetHashCode() == GetHashCode();
        });
        return entities;
    }





    /// <summary>
    /// ����
    /// </summary>
    void Move()
    {
        _rigidbody2D.velocity = _faceDirection * _movement;
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
    float IEntity.GetSpawnedTime()
    {
        return _spawnedTime;
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