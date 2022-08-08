using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �������� ������ Ŭ�����Դϴ�
/// </summary>
public class DefaultData : ScriptableObject
{
    //ScriptableObject : �뷮�� �����͸� �����ϴ� �� ����� �� �ִ� ������ �����̳�
    public const string pathData = "/Resources/Data/";
    //���� ���̵� �Ӽ�
    public string[] idx = null;
    public DefaultData() { }

    //�� ���� ��������
    public int getDataCnt()
    {
        int _retCnt = 0;
        if(this.idx != null)
        {
            _retCnt = this.idx.Length;
        }
        return _retCnt;
    }
    public string[] getDataIdxList(bool flagID, string strFilter = "")
    {
        string[] retList = new string[0];
        if(this.idx == null)
        {
            return retList;
        }

        retList = new string[this.idx.Length];

        for(int i = 0; i < this.idx.Length; i++)
        {
            if(strFilter != "")
            {
                if(idx[i].ToLower().Contains(strFilter.ToLower()) == false)
                {
                    continue;
                }
            }

            if(flagID)
            {
                retList[i] = i.ToString() + " : " + this.idx[i];
            }
            else
            {
                retList[i] = this.idx[i];
            }
        }

        return retList;
    }

    public virtual int constructorData(string _dataIdx)
    {
        return getDataCnt();
    }

    public virtual void deleteData(int _pid) { }

    public virtual void defulicateData(int _pid) { }
}
