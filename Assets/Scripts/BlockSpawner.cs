using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    // Fields
    [SerializeField]
    public GameObject BlockPrefab = null;
    [SerializeField]
    public int PoolSize = 50;

    // Members
    public Queue<Transform> BlockTransformQueue = new Queue<Transform>();
    private GameObject[] m_BlockPool;

    // Singleton instance
    public static BlockSpawner BlockSpawnerSingleton = null;

    private void Awake()
    {
        if (BlockSpawnerSingleton != null)
        {
            Destroy(GetComponent<BlockSpawner>());
            return;
        }

        BlockSpawnerSingleton = this;
        m_BlockPool = new GameObject[PoolSize];

        for (int i = 0; i < PoolSize; i++)
        {
            m_BlockPool[i] = Instantiate(BlockPrefab, Vector3.zero, Quaternion.identity) as GameObject;
            Transform blockTransform = m_BlockPool[i].transform;
            blockTransform.parent = transform;

            BlockTransformQueue.Enqueue(blockTransform);
            m_BlockPool[i].SetActive(false);
        }
    }

    // Interface

    public static Transform SpawnBlock()
    {
        Transform spawnedBlock = BlockSpawnerSingleton.BlockTransformQueue.Dequeue();
        spawnedBlock.gameObject.SetActive(true);

        BlockSpawnerSingleton.BlockTransformQueue.Enqueue(spawnedBlock);

        return spawnedBlock;
    }
}
