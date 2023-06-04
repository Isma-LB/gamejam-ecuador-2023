using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [SerializeField] Vector2 gridSize = new Vector2(16,8);
    [SerializeField] List<MapBlock> blocks;
    [SerializeField] Transform playerTransform;
    [SerializeField] Transform playerTransformDown;
    [SerializeField] PolygonCollider2D cameraContainer;

    [Header("backgrounds")]
    [SerializeField] int skyStart = 2;
    [SerializeField] int earthStart = 2;
    [SerializeField] GameObject skyBGPrefab;
    [SerializeField] GameObject middleBGPrefab;
    [SerializeField] GameObject earthBGPrefab;

    Vector2 currentGridCell = Vector2.zero;

    List<Vector2> visited = new List<Vector2>();

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
        Vector2 playerDownGridCoordinates = GetGridCoordinates(playerTransformDown.position);
        if(currentGridCell != playerGridCoordinates && !visited.Contains(playerGridCoordinates)){
            ChooseMapBlock(playerGridCoordinates);
            currentGridCell = playerGridCoordinates;
            cameraContainer.transform.position = GetWorldPos(playerGridCoordinates);
        };
        if(currentGridCell != playerDownGridCoordinates && !visited.Contains(playerDownGridCoordinates)){
            ChooseMapBlock(playerDownGridCoordinates);
            currentGridCell = playerDownGridCoordinates;
            cameraContainer.transform.position = GetWorldPos(playerDownGridCoordinates);
        };
        
    }

    private void ChooseMapBlock(Vector2 playerPos)
    {
        SpawnBG(playerPos);
        visited.Add(playerPos);
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
    void SpawnBG(Vector2 playerPos){
        if(playerPos.y > skyStart){
            Instantiate(skyBGPrefab, GetWorldPos(playerPos), Quaternion.identity, this.transform);
        }
        else if(playerPos.y < earthStart){
            Instantiate(earthBGPrefab, GetWorldPos(playerPos), Quaternion.identity, this.transform);
        }
        else {
            Instantiate(middleBGPrefab, GetWorldPos(playerPos), Quaternion.identity, this.transform);
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
    public int GetScores()
    {
        return Mathf.RoundToInt(currentGridCell.magnitude);
    }
}
