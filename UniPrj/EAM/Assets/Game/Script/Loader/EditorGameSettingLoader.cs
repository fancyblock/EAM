using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class EditorGameSettingLoader : IGameSettingLoader
{
    /// <summary>
    /// ֱ�Ӵ�Excel��ȡ����
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="name"></param>
    /// <returns></returns>
    public List<T> LoadData<T>(string name) 
    {
        string filePath = $"Assets/GameSettings~/{name}.xlsx";

        if(!File.Exists(filePath))
            throw new Exception($"�Ҳ������ñ� {filePath}");

        string dataJson = null;

        using (FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
        {
            // Auto-detect format, supports:
            //  - Binary Excel files (2.0-2003 format; *.xls)
            //  - OpenXml Excel files (2007 format; *.xlsx, *.xlsb)
            using (var reader = ExcelReaderFactory.CreateReader(stream))
            {
                List<string> fields = new List<string>();
                Dictionary<string, int> fieldIndex = new Dictionary<string, int>();

                List<Dictionary<string, object>> itemList = new List<Dictionary<string, object>>();

                int count = reader.RowCount;

                for(int i = 0; i < count; i++)
                {
                    reader.Read();

                    // �ֶ�����
                    if (i == 0)
                    {
                        for (int j = 0; j < reader.FieldCount; j++)
                        {
                            try
                            {
                                string fieldName = reader.GetString(j);

                                fields.Add(fieldName);
                                fieldIndex.Add(fieldName, j);
                            }
                            catch(Exception e)
                            {
                                Debug.LogError("���ñ��ֶ�Ӧ�����ַ���!!!");
                            }
                        }
                    }
                    // ʵ��������
                    else
                    {
                        Dictionary<string, object> item = new Dictionary<string, object>();

                        foreach (string key in fieldIndex.Keys)
                        {
                            try
                            {
                                if(reader.GetValue(fieldIndex[key]) == null)
                                {
                                    item.Add(key, "");
                                    continue;
                                }

                                item.Add(key, reader.GetValue(fieldIndex[key]).ToString());
                            }
                            catch(Exception e)
                            {
                                Debug.LogError($"����{name}���в�֧�ֵ��ֶ�");
                            }
                        }

                        itemList.Add(item);
                    }
                }

                dataJson = Newtonsoft.Json.JsonConvert.SerializeObject(itemList);
            }
        }


        List<T> result = new List<T>();

        result = Newtonsoft.Json.JsonConvert.DeserializeObject<List<T>>(dataJson);

        return result;
    }


    /// <summary>
    /// ��ȡExcelԭʼ����
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public object[,] LoadRawSheet(string name)
    {
        string filePath = $"Assets/GameSettings~/{name}.xlsx";

        if (!File.Exists(filePath))
            throw new Exception($"�Ҳ������ñ� {filePath}");

        object[,] result = null;

        using (FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
        {
            using (var reader = ExcelReaderFactory.CreateReader(stream))
            {
                int fieldCnt = reader.FieldCount;
                int rowCnt = reader.RowCount;

                if (fieldCnt + rowCnt == 0)
                {
                    Util.Log($"����{name}û������", Color.yellow);
                    return null;
                }

                result = new object[fieldCnt, rowCnt];

                for (int i = 0; i < rowCnt; i++)
                {
                    reader.Read();

                    for (int j = 0; j < fieldCnt; j++)
                    {
                        result[j,i] = reader.GetValue(j);
                    }
                }
            }
        }

        return result;
    }
}