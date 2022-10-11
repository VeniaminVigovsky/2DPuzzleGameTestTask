using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveHandlerController
{ 
    private static List<IGameDataSaver> GAMEDATASAVERS;

    private static GameDataSO GAMEDATASO;

    public SaveHandlerController(GameDataSO gameDataSO)
    {
        GAMEDATASO = gameDataSO;
        GAMEDATASAVERS = new List<IGameDataSaver>();
    }

    public static void RegisterGameDataSaver(IGameDataSaver gameDataSaver, out GameDataSO gameDataSO)
    {
        if (GAMEDATASAVERS == null)
            GAMEDATASAVERS = new List<IGameDataSaver>();

        gameDataSO = GAMEDATASO;

        if (GAMEDATASAVERS.Contains(gameDataSaver)) return;

        GAMEDATASAVERS.Add(gameDataSaver);
    }

    public void Save()
    {
        if (GAMEDATASO == null) return;

        foreach (var gameDataSaver in GAMEDATASAVERS)
        {
            gameDataSaver?.SaveGameData();
        }

        var saveData = new SaveData(GAMEDATASO.PlayerBoardIndeces, GAMEDATASO.GameBoardIndeces, GAMEDATASO.SoundGainSliderValue);

        SaveFileSerializationController.SerializeSaveData(saveData);
    }

    public void Load()
    {
        if (GAMEDATASO == null) return;

        var saveData = SaveFileSerializationController.DeserializeSaveData();

        if (saveData == null) return;

        GAMEDATASO.PlayerBoardIndeces = saveData.PlayerBoardIndeces;
        GAMEDATASO.GameBoardIndeces = saveData.GameBoardIndeces;
        GAMEDATASO.SoundGainSliderValue = saveData.SoundGainSliderValue;
    }
}
