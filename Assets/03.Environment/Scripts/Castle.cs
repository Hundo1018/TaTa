using Assets._04.Characters.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : MonoBehaviour, IEntity
{
    [SerializeField] private GameObject _minionPrefab;
    [SerializeField] private GameObject _currentSpawn;
    [SerializeField] private bool _isSpawning;
    [SerializeField] private float _startTime;
    [SerializeField] private float _prepareTime;
    [SerializeField] private Transform _spawnPoint;
    private int _alignment;
    /// <summary>
    /// �ͦ���C
    /// </summary>
    //Queue<IMinion> _spawnQueue;
    Queue<GameObject> _spawnQueue = new Queue<GameObject>();
    int IEntity.GetAlignment()
    {
        return _alignment;
    }

    string IEntity.GetName()
    {
        return name;
    }

    Vector2 IEntity.GetPostion()
    {
        return transform.position;
    }

    void IEntity.Hurted(int damage)
    {
        throw new System.NotImplementedException();
    }

    /// <summary>
    /// �ͤp�L
    /// </summary>
    void Spawn()
    {

    }

    /// <summary>
    /// �[�J��C
    /// </summary>
    public void AddMinionToQueue()
    {
        _spawnQueue.Enqueue(_minionPrefab);
    }

    IEnumerable Timer()
    {

        yield return 0;
    }

    void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //�]�ɶ��A�ɶ���F
        if (_isSpawning && Time.time >= _startTime + _prepareTime)
        {
            Debug.Log("�l���");
            _isSpawning = false;
            Instantiate(_minionPrefab, _spawnPoint.position, new Quaternion());
        }
        //�ˬd�O�_�b�l���
        //�p�G�D�l�ꤤ
        //�ˬd���S���n�l�ꪺ
        //�p�G���A��X�n�l�ꪺ��H�A
        //
        if (!_isSpawning)
        {
            if (_spawnQueue.Count > 0)
            {
                var go = _spawnQueue.Dequeue();
                var minion = go.GetComponent<ISpawnable>();
                _prepareTime = minion.GetSpawnTime();
                //�ҥέp�ɡB
                _isSpawning = true;
                //�O����U�ɶ��A�C���۴�P�_�O�_�W�L�Ψ�F�l��w�Ʈɶ�
                _startTime  = Time.time;
            }
        }
    }

    EntityType IEntity.GetEntityType()
    {
        throw new System.NotImplementedException();
    }
}