using UnityEngine;

[CreateAssetMenu(menuName = "gamejam-ecuador-2023/MapBlock")]
public class MapBlock : ScriptableObject
{
    public bool up;
    public bool down;
    public bool forward;
    public bool back;
    [SerializeField] GameObject blockPrefab = null;
    public void SpawnBlock(Vector3 pos, Transform parent){
        Instantiate(blockPrefab, pos, Quaternion.identity, parent);
    }
}
