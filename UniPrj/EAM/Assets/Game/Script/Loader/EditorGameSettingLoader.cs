using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class EditorGameSettingLoader : IGameSettingLoader
{
    /// <summary>
    /// 直接从Excel读取配置
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="name"></param>
    /// <returns></returns>
    public List<T> LoadData<T>(string name) 
    {
        string filePath = $"Assets/GameSettings~/{name}.xlsx";

        if(!File.Exists(filePath))
            throw new Exception($"找不到配置表 {filePath}");

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

                    // 字段名行
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
                                Debug.LogError("配置表字段应该是字符串!!!");
                            }
                        }
                    }
                    // 实际数据行
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

                                Type type = reader.GetFieldType(fieldIndex[key]);

                                if (type == typeof(string))
                                {
                                    item.Add(key, reader.GetString(fieldIndex[key]));
                                }
                                else if (type == typeof(int))
                                {
                                    item.Add(key, reader.GetInt32(fieldIndex[key]));
                                }
                                else if (type == typeof(double))
                                {
                                    item.Add(key, reader.GetDouble(fieldIndex[key]));
                                }
                                else if (type == typeof(bool))
                                {
                                    item.Add(key, reader.GetBoolean(fieldIndex[key]));
                                }
                                else
                                {
                                    Debug.LogError($"表格{name}含有不支持的字段类型: {type.Name} 在{fieldIndex[key]},{i+1}");
                                }
                            }
                            catch(Exception e)
                            {
                                Debug.LogError($"表格{name}含有不支持的字段");
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
}
