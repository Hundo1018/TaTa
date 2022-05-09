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
    /// 生成佇列
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
    /// 生小兵
    /// </summary>
    void Spawn()
    {

    }

    /// <summary>
    /// 加入佇列
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
        //跑時間，時間到了
        if (_isSpawning && Time.time >= _startTime + _prepareTime)
        {
            Debug.Log("召喚啦");
            _isSpawning = false;
            Instantiate(_minionPrefab, _spawnPoint.position, new Quaternion());
        }
        //檢查是否在召喚當中
        //如果非召喚中
        //檢查有沒有要召喚的
        //如果有，選出要召喚的對象，
        //
        if (!_isSpawning)
        {
            if (_spawnQueue.Count > 0)
            {
                var go = _spawnQueue.Dequeue();
                var minion = go.GetComponent<ISpawnable>();
                _prepareTime = minion.GetSpawnTime();
                //啟用計時、
                _isSpawning = true;
                //記錄當下時間，每次相減判斷是否超過或到達召喚預備時間
                _startTime  = Time.time;
            }
        }
    }

    EntityType IEntity.GetEntityType()
    {
        throw new System.NotImplementedException();
    }
}