using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameDataSaver
{
    void RegisterGameDataSaver();

    void SaveGameData();

    void ClearGameData();
}
