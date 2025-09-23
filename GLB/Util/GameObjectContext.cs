using UnityEngine;

namespace GLB.Util
{
    public class GameObjectContext
    {
        public GameObject? gameObject { get; private set; } = null;
        public Transform? transform { get { return gameObject?.transform ?? null; } }
        public GameObjectContext(GameObject go) => gameObject = go;


    }
}