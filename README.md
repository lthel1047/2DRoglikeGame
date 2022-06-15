## 2DRoglikeGame

Queue / List 와 SceneManager 를 통해 미리 만들어 놓은 맵씬을 배열에 넣은 뒤
게임 시작과 동시에 맵을 호출한다 대신 중복 가능성이 넘치고 메모리 효율성이 떨어진다.
이후 맵에 몬스터 및 아이템을 배치하는데 맵에 분산을 시켜 배치를 하기 때문에 이 또한 효율성이 떨어지는 판단.
<pre>
<code>
 void UpdateRoomQueue(){
        if(isLoadingRoom){
            return;
        }
        if(loadRoomQueue.Count == 0){
            if(!spawnedBossRoom){
                StartCoroutine(SpawnBossRoom());
            }else if(spawnedBossRoom && !updatedRooms){
                foreach(Room room in loadedRooms){
                    room.RemoveUnconnectedDoors();
                }

                UpdateRooms();
                updatedRooms = true;
            }
            return;
        }

        currentLoadRoomData=loadRoomQueue.Dequeue();
        isLoadingRoom = true;
        StartCoroutine(LoadRoomRoutine(currentLoadRoomData));
    }
    
    // 보스 방은 맵의 끝에 생성
    IEnumerator SpawnBossRoom(){
        spawnedBossRoom = true;
        yield return new WaitForSeconds(0.5f);
        if(loadRoomQueue.Count == 0){
            Room bossRoom = loadedRooms[loadedRooms.Count - 1];
            Room tempRoom = new Room(bossRoom.X, bossRoom.Y);
            Destroy(bossRoom.gameObject);
            var roomToRemove = loadedRooms.Single(r => r.X == tempRoom.X && r.Y == tempRoom.Y);
            loadedRooms.Remove(roomToRemove);
            LoadRoom("End", tempRoom.X, tempRoom.Y);
        }
    }
    
    // 룸 정보를 담아 Queue에 추가
    public void LoadRoom(string name, int x, int y){

        if(DoseRoomExist(x,y)){
            return;
        }
        RoomInfo newRoomData = new RoomInfo();
        newRoomData.name =name;
        newRoomData.X = x;
        newRoomData.Y = y;

        loadRoomQueue.Enqueue(newRoomData);

    }
    
     // 저장된 룸 정보를 호출 및 활성화
    IEnumerator LoadRoomRoutine(RoomInfo info){
        string roomName = currentWorldName + info.name;

        AsyncOperation loadRoom =SceneManager.LoadSceneAsync(roomName, LoadSceneMode.Additive);

        while(loadRoom.isDone == false){
            yield return null;
        }
    }
  
  // 룸에 대한 정보 
    public void RegisterRoom(Room room){
        if(!DoseRoomExist(currentLoadRoomData.X, currentLoadRoomData.Y))
        {
                room.transform.position=new Vector3(
                currentLoadRoomData.X * room.Width,
                currentLoadRoomData.Y * room.Height,
                 0
            ); 
            room.X=currentLoadRoomData.X;
            room.Y=currentLoadRoomData.Y;
            room.name=currentWorldName + "-" + currentLoadRoomData.name + " " + room.X + ", " + room.Y;
            room.transform.parent = transform;
    
            isLoadingRoom = false;

            if(loadedRooms.Count == 0)
            {
                CameraController.instance.currRoom=room;
            }
            loadedRooms.Add(room);
        }
        else{
            Destroy(room.gameObject);
            isLoadingRoom = false;
        }
    }
</pre>
</code>
