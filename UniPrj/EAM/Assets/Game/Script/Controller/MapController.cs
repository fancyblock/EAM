using System.Collections.Generic;
using Zenject;


public class MapController : IInitializable
{
    [Inject]
    private IGameSettingLoader m_loader;

    private TableMap m_tableMap;
    private List<TableMapTile> m_tableMapTile;


    public void Initialize()
    {
        Util.Log("Map initialize.");

        //TODO 

        LoadMap();
    }

    public void LoadMap()
    {
        Util.Log("LoadMap");

        m_tableMapTile = m_loader.LoadData<TableMapTile>("TableMapTile");
        m_tableMap = new TableMap();

        object[,] rawTableMap = m_loader.LoadRawSheet("TableMap");

        int x = rawTableMap.GetLength(0);
        int y = rawTableMap.GetLength(1);
        m_tableMap.m_tiles = new string[x,y];

        for(int i = 0; i < x; i++)
        {
            for(int j = 0; j < y; j++)
                m_tableMap.m_tiles[i,j] = rawTableMap[i, j].ToString();
        }

        //TODO 
    }
}
