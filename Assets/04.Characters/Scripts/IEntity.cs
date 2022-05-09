using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets._04.Characters.Scripts
{
    public enum EntityType
    {
        Minion,
        Hero,
        Castle
    }
    interface IEntity
    {
        /// <summary>
        /// 取得GameObject名稱
        /// </summary>
        /// <returns></returns>
        public string GetName();
        /// <summary>
        /// 取得陣營
        /// </summary>
        /// <returns></returns>
        public int GetAlignment();
        /// <summary>
        /// 受傷
        /// </summary>
        /// <param name="damage"></param>
        public void Hurted(int damage);
        /// <summary>
        /// 取得位置
        /// </summary>
        /// <returns></returns>
        public Vector2 GetPostion();
        public EntityType GetEntityType();
    }
}
