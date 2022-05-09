using Assets._04.Characters.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour,IEntity
{
    int _alignment;
    int IEntity.GetAlignment()
    {
        return _alignment;
    }

    EntityType IEntity.GetEntityType()
    {
        throw new System.NotImplementedException();
    }

    string IEntity.GetName()
    {
        return name;
    }

    Vector2 IEntity.GetPostion()
    {
        throw new System.NotImplementedException();
    }

    void IEntity.Hurted(int damage)
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
