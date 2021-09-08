using System.Collections.Generic;


public interface IGameSettingLoader 
{
    List<T> LoadData<T>(string name);

    object[,] LoadRawSheet(string name);
}
