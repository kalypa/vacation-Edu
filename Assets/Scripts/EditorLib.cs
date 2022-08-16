using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using System.Text;

public class EditorLib : MonoBehaviour
{
    public static string getAssetPath(UnityEngine.Object _clip)
    {
        string retStrPath = string.Empty;
        retStrPath = AssetDatabase.GetAssetPath(_clip);
        string[] tmpStrPath = retStrPath.Split('/');
        bool flagRes = false;
        for(int i = 0; i <tmpStrPath.Length - 1; i++)
        {
            if(flagRes == false)
            {
                if(tmpStrPath[i] == "Resources")
                {
                    flagRes = true;
                    retStrPath = string.Empty;
                }
            }
            else
            {
                retStrPath += tmpStrPath[i] + "/";
            }
        }
        return retStrPath;
    }

    public static void makeEnumClass(string enumName, StringBuilder enumData)
    {
        string _filePathTemplate = "Assets/Scripts/Editor/EnumClassTemplate.txt";

        string contentClassTemplate = File.ReadAllText(_filePathTemplate);

        contentClassTemplate = contentClassTemplate.Replace("$DATA$", enumData.ToString());
        contentClassTemplate = contentClassTemplate.Replace("$ENUM$", enumName);

        string tempFilePathTemplate = "Assets/Scripts/ClassTemplate/";
        if (Directory.Exists(tempFilePathTemplate) == false)
        {
            Directory.CreateDirectory(tempFilePathTemplate);
        }

        string retFilePathTemplate = tempFilePathTemplate + enumName + ".cs";
        if(File.Exists(retFilePathTemplate))
        {
            File.Delete(retFilePathTemplate);
        }
        File.WriteAllText(retFilePathTemplate, contentClassTemplate);
    }

    public static void setTopLayer(DefaultData data, ref int nowIdx, ref UnityEngine.Object objLayer, int sizeWidth)
    {
        EditorGUILayout.BeginHorizontal();
        {
            if(GUILayout.Button("Constructor", GUILayout.Width(sizeWidth)))
            {
                data.constructorData("New Data");
                nowIdx = data.getDataCnt() - 1;
                objLayer = null;
            }

            if (GUILayout.Button("Defulicate", GUILayout.Width(sizeWidth)))
            {
                data.defulicateData(nowIdx);
                objLayer = null;
                nowIdx = data.getDataCnt() - 1;
            }

            if(data.getDataCnt() > 1)
            {
                if (GUILayout.Button("Delete", GUILayout.Width(sizeWidth)))
                {
                    objLayer = null;
                    data.deleteData(nowIdx);
                }
            }

            if(nowIdx > data.getDataCnt() - 1)
            {
                nowIdx = data.getDataCnt() - 1;
            }
        }
        EditorGUILayout.EndHorizontal();
    }

    public static void setListLayer(ref Vector2 posScroll, DefaultData data, ref int nowIdx, ref UnityEngine.Object objLayer, int uiWidth)
    {
        EditorGUILayout.BeginVertical(GUILayout.Width(uiWidth));
        {
            EditorGUILayout.Separator();
            EditorGUILayout.BeginVertical("box");
            {
                posScroll = EditorGUILayout.BeginScrollView(posScroll);
                {
                    int lastIdx = nowIdx;
                    nowIdx = GUILayout.SelectionGrid(nowIdx, data.getDataIdxList(true), 1);
                    if (lastIdx != nowIdx)
                    {
                        objLayer = null;
                    }
                }
                EditorGUILayout.EndScrollView();
            }
            EditorGUILayout.EndVertical();
        }
        EditorGUILayout.EndVertical();
    }
}
