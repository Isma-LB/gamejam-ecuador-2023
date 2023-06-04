using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [SerializeField] Vector2 gridSize = new Vector2(16,8);
    [SerializeField] List<MapBlock> blocks;
    [SerializeField] Transform playerTransform;
    [SerializeField] PolygonCollider2D cameraContainer;

    Vector2 currentGridCell = Vector2.zero;

    List<MapBlock> upBlocks;
    List<MapBlock> downBlocks;
    List<MapBlock> forwardBlocks;
    void Start()
    {
        upBlocks = blocks.FindAll(FindUpBlock);
        downBlocks = blocks.FindAll(FindDownBlock);
        forwardBlocks = blocks.FindAll(FindForwardBlock);
    }
    void OnValidate()
    {
        if(cameraContainer){
            Vector2[] colliderPoints = {
                new Vector2(0,0),
                new Vector2(gridSize.x,0),
                new Vector2(gridSize.x,gridSize.y),
                new Vector2(0,gridSize.y)
            };
            cameraContainer.points = colliderPoints; 
        }
    }
    void Update()
    {
        Vector2 playerGridCoordinates = GetGridCoordinates(playerTransform.position);
        if(currentGridCell != playerGridCoordinates){
            ChooseMapBlock(playerGridCoordinates);
            currentGridCell = playerGridCoordinates;
            cameraContainer.transform.position = GetWorldPos(playerGridCoordinates);
        }
    }

    private void ChooseMapBlock(Vector2 playerPos)
    {
        if(currentGridCell.x < playerPos.x){
            // go forward
            int randomIndex = Mathf.FloorToInt(UnityEngine.Random.Range(0f, forwardBlocks.Count));
            forwardBlocks[randomIndex].SpawnBlock(GetWorldPos(playerPos), this.transform);
        }
        else if(currentGridCell.y < playerPos.y){
            // go up
            int randomIndex = Mathf.FloorToInt(UnityEngine.Random.Range(0f, upBlocks.Count));
            upBlocks[randomIndex].SpawnBlock(GetWorldPos(playerPos), this.transform);
        }
        else if(currentGridCell.y > playerPos.y){
            // go down
            int randomIndex = Mathf.FloorToInt(UnityEngine.Random.Range(0f, downBlocks.Count));
            downBlocks[randomIndex].SpawnBlock(GetWorldPos(playerPos), this.transform);
        }
    }

    Vector2 GetGridCoordinates(Vector3 pos){
        Vector2 value;
        value.x = Mathf.Floor(pos.x / gridSize.x);
        value.y = Mathf.Floor(pos.y / gridSize.y);
        return value;
    }
    Vector3 GetWorldPos(Vector2 pos){
        return new Vector3(pos.x * gridSize.x, pos.y * gridSize.y);
    }
     // Predicate delegate. used as filter
    private static bool FindDownBlock(MapBlock block){
        return block.up && !block.back;
    }
    private static bool FindUpBlock(MapBlock block){
        return block.down && !block.back;
    }
    private static bool FindForwardBlock(MapBlock block){
        return block.back;
    }
}
