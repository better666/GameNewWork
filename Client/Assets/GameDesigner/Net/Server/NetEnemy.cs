using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Net.Server
{
    public class NetEnemy
    {
        public string name;
        public Vector3 position = Vector3.zero;
        public Quaternion rotation = Quaternion.identity;
        public PropertyData propertyData = new PropertyData();
        public NetPlayer target;
    }
}
