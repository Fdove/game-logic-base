using System.Threading.Tasks.Dataflow;
using UnityEngine;

namespace GLB.Util
{
    public class GameObjectContext
    {
        public GameObject? gameObject { get; private set; } = null;
        public Transform transform { get { return gameObject.transform; } }
        public GameObjectContext(GameObject go) => gameObject = go;


    }
}