using FunctionAPIApp;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FunctionAPIApp
{
    [JsonObject]
    public class subsc_user_tableList
    {
        [JsonProperty("List")]
        public List<subsc_user_tableRow> List { get; set; } = new List<subsc_user_tableRow>();
    }

    [JsonObject]
    public class subsc_employee_tableList
    {
        [JsonProperty("List")]
        public List<subsc_employee_tableRow> List { get; set; } = new List<subsc_employee_tableRow>();
    }

    [JsonObject]
    public class subsc_product_tableList
    {
        [JsonProperty("List")]
        public List<subsc_product_tableRow> List { get; set; } = new List<subsc_product_tableRow>();
    }
    [JsonObject]
    public class subsc_ordercart_tableList
    {
        [JsonProperty("List")]
        public List<subsc_ordercart_tableRow> List { get; set; } = new List<subsc_ordercart_tableRow>();
    }

    [JsonObject]
    public class subsc_detail_tableList
    {
        [JsonProperty("List")]
        public List<subsc_detail_tableRow> List { get; set; } = new List<subsc_detail_tableRow>();
    }




    [JsonObject]
    public class subsc_user_tableRow
    {
        [JsonProperty("user_id")]
        public int user_id { get; set; }

        [JsonProperty("user_name")]
        public string user_name { get; set; }

        [JsonProperty("user_pass")]
        public string user_pass { get; set; }

        [JsonProperty("user_mail")]
        public string user_mail { get; set; }

        [JsonProperty("user_postcode")]
        public string user_postcode { get; set; }

        [JsonProperty("user_adress")]
        public string user_adress { get; set; }

        [JsonProperty("user_telenum")]
        public string user_telenum { get; set; }

    }

    public class subsc_employee_tableRow
    {

        [JsonProperty("employee_name")]
        public string employee_name { get; set; }

        [JsonProperty("employee_pass")]
        public string employee_pass { get; set; }

    }


    public class subsc_product_tableRow
    {
        [JsonProperty("product_id")]
        public int? product_id { get; set; }

        [JsonProperty("product_name")]
        public string product_name { get; set; }

        [JsonProperty("product_category")]
        public string product_category { get; set; }

        [JsonProperty("product_gender")]
        public string product_gender { get; set; }

        [JsonProperty("URL")]
        public string URL { get; set; }

    }

    public class subsc_ordercart_tableRow
    {

        [JsonProperty("order_id")]
        public int order_id { get; set; }

        [JsonProperty("product_id")]
        public int product_id { get; set; }

        [JsonProperty("user_id")]
        public int user_id { get; set; }

        [JsonProperty("product_size")]
        public string product_size { get; set; }

        [JsonProperty("quantity")]
        public int quantity { get; set; }

    }

    public class subsc_detail_tableRow
    {


        [JsonProperty("detail_id")]
        public int detail_id { get; set; }

        [JsonProperty("order_id")]
        public int order_id { get; set; }

        [JsonProperty("product_id")]
        public int product_id { get; set; }

        [JsonProperty("user_id")]
        public int user_id { get; set; }

        [JsonProperty("product_size")]
        public string product_size { get; set; }

        [JsonProperty("quantity")]
        public int quantity { get; set; }

    }


}
