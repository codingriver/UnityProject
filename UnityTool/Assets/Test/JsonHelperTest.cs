﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Codingriver;
using System.IO;
using System;
using LitJson;
using System.Runtime.Serialization;

public class JsonHelperTest : MonoBehaviour
{

    [Serializable]
    public class InnerTest
    {
        public class InnerA
        {
           public string innerStr = "严";
            int key = 99;
        }
        public int a = 1;
        public string b = "hello";
        int[] arr = new int[] { 5, 76, 12 };
        public InnerA innerA = new InnerA();
    }

    
    public class TestData: IDeserializationCallback
    {
        public string[] Sitekeys = new string[] { "river", "coding" };
        public string name = "codingriver";
        
        public int a = 1000;
        public int d = 3;
        public string b = "哈哈哈哈";
        [NonSerialized]
        public int c;
        protected string protect = "11111";
        
        int m_Main = 1024;

        [NonSerialized]
        public InnerTest obj = new InnerTest();

        public int Main
        {
            get
            {
                return m_Main;
            }
            set
            {
                m_Main = value;
            }
        }

        public void OnDeserialization(object sender)
        {
            c = a * d;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        TestData data = new TestData();
        data.Main = 2048;
        data.name = "你们好吗！";
        data.obj.a = 9;
        data.a = 7;
        data.d = 2;
        
        Debug.Log("origin:"+Dumper.DumpAsString(data));
        string json= JsonHelper.ToJson(data);
        Debug.Log("json:"+json);
        File.WriteAllText("./test.json", json);
        TestData data1 = JsonHelper.FromJson<TestData>(json);
        Debug.Log("data1:"+ Dumper.DumpAsString(data1));

        string litJson = JsonMapper.ToJson(data);
        Debug.Log($"litJson:{litJson}");
        TestData data2 = JsonMapper.ToObject<TestData>(litJson);
        Debug.Log($"data2:{Dumper.DumpAsString(data2)}");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
