﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MultiCloudAutomation.AZUREInstance
{
    class Instance
    {
        ResponseClass task;
        public static int selectedColumnIndex = 0;
        public static string region = "us-east-1";


        public async Task<string> AZUREDGVListAdd()
        {
            ResponseClass instances = await AZUREAllInstance();
            if (instances.StatusCode == HttpStatusCode.OK)
            {
                AZUREinstance_To_DataGridViewVMList(instances);
            }
            
            return instances.StatusCode.ToString();
        }

        public async Task<ResponseClass> AZUREAllInstance()
        {
            task = await Request.GetRequestAsync("azure/getVMSimple");
            return task;
        }

        public void AZUREinstance_To_DataGridViewVMList(ResponseClass instances)
        {
            string jsonbody = JsonConvert.SerializeObject(instances);
            JArray json_data = (JArray)JsonConvert.DeserializeObject(task.Content);
            foreach (var item in json_data)
            {
                DataGridViewVM instance = new DataGridViewVM();
                instance.CloudType = "AZURE";
                instance.VMID = item["vmid"].ToString();
                instance.InstanceName = item["instanceName"].ToString();
                instance.InstanceState = item["instanceState"].ToString();
                instance.InstanceType = item["instanceType"].ToString();
                instance.OSType = item["osType"].ToString();
                instance.PublicIP = item["publicIP"].ToString();
                Cloud.instanceDataGridViewList.Add(instance);
            }
        }
    }
}