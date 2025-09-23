using UnityEngine;

namespace GLB.Util
{
    public class GameObjectContext
    {
        public GameObject? gameObject { get; private set; } = null;
        public GameObjectContext(GameObject go) => gameObject = go;

        
    }
}