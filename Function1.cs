using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Data.SqlClient;//DB接続用ライブラリ
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;

namespace FunctionAPIApp
{
    public static class Function1
    {
        //ユーザーテーブル
        [FunctionName("SELECT")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {

            //レスポンス用文字列
            string responseMessage = "SQL RESULT:";

            try
            {
                //接続文字列の設定
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = "m3hminagawafunction.database.windows.net";
                builder.UserID = "sqladmin";
                builder.Password = "Ynm004063";
                builder.InitialCatalog = "m3h-minagawa-fanctionDB";

                //接続用オブジェクトの初期化
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    Console.WriteLine("\nQuery data example:");
                    Console.WriteLine("=========================================\n");

                    //実行するクエリ
                    String sql = "SELECT user_id, user_name, user_pass, user_mail, user_postcode, user_adress, user_telenum FROM subsc_user_table";

                    //SQL実行オブジェクトの初期化
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        //DBと接続
                        connection.Open();

                        //SQLを実行し、結果をオブジェクトに格納
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            //結果を格納するためのオブジェクトを初期化
                            subsc_user_tableList resultList = new subsc_user_tableList();

                            //結果を1行ずつ処理
                            while (reader.Read())
                            {
                                //オブジェクトに結果を格納
                                resultList.List.Add(new subsc_user_tableRow { user_id = reader.GetInt32("user_id"), user_name = reader.GetString("user_name"), user_pass = reader.GetString("user_pass"), user_mail = reader.GetString("user_mail"), user_postcode = reader.GetString("user_postcode"), user_adress = reader.GetString("user_adress"), user_telenum = reader.GetString("user_telenum") });
                            }
                            //JSONオブジェクトを文字列に変換
                            responseMessage = JsonConvert.SerializeObject(resultList);
                        }
                    }
                }
            }
            //DB操作でエラーが発生した場合はここでキャッチ
            catch (SqlException e)
            {
                //エラーをコンソールに出力
                Console.WriteLine(e.ToString());
            }
            //結果文字列を返却
            return new OkObjectResult(responseMessage);
        }


        //従業員テーブル
        [FunctionName("SELECT2")]
        public static async Task<IActionResult> Run2(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {

            //レスポンス用文字列
            string responseMessage = "SQL RESULT:";

            try
            {
                //接続文字列の設定
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = "m3hminagawafunction.database.windows.net";
                builder.UserID = "sqladmin";
                builder.Password = "Ynm004063";
                builder.InitialCatalog = "m3h-minagawa-fanctionDB";

                //接続用オブジェクトの初期化
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    Console.WriteLine("\nQuery data example:");
                    Console.WriteLine("=========================================\n");

                    //実行するクエリ
                    String sql = "SELECT employee_name, employee_pass FROM subsc_employee_table";

                    //SQL実行オブジェクトの初期化
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        //DBと接続
                        connection.Open();

                        //SQLを実行し、結果をオブジェクトに格納
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            //結果を格納するためのオブジェクトを初期化
                            subsc_employee_tableList resultList = new subsc_employee_tableList();

                            //結果を1行ずつ処理
                            while (reader.Read())
                            {
                                //オブジェクトに結果を格納
                                resultList.List.Add(new subsc_employee_tableRow { employee_name = reader.GetString("employee_name"), employee_pass = reader.GetString("employee_pass") });
                            }
                            //JSONオブジェクトを文字列に変換
                            responseMessage = JsonConvert.SerializeObject(resultList);
                        }
                    }
                }
            }
            //DB操作でエラーが発生した場合はここでキャッチ
            catch (SqlException e)
            {
                //エラーをコンソールに出力
                Console.WriteLine(e.ToString());
            }
            //結果文字列を返却
            return new OkObjectResult(responseMessage);
        }


        /// 商品テーブル（一覧）
        [FunctionName("SELECT3")]
        public static async Task<IActionResult> Run3(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            // レスポンス用文字列
            string responseMessage = "SQL RESULT:";

            try
            {
                // 接続文字列の設定
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = "m3hminagawafunction.database.windows.net";
                builder.UserID = "sqladmin";
                builder.Password = "Ynm004063";
                builder.InitialCatalog = "m3h-minagawa-fanctionDB";

                // 接続用オブジェクトの初期化
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    Console.WriteLine("\nQuery data example:");
                    Console.WriteLine("=========================================\n");

                    // 実行するクエリにproduct_idを追加
                    String sql = "SELECT product_id, product_name, product_category, product_gender, URL FROM subsc_product_table";

                    // SQL実行オブジェクトの初期化
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        // DBと接続
                        connection.Open();

                        // SQLを実行し、結果をオブジェクトに格納
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            // 結果を格納するためのオブジェクトを初期化
                            subsc_product_tableList resultList = new subsc_product_tableList();

                            // 列インデックスの取得
                            int idIndex = reader.GetOrdinal("product_id");
                            int nameIndex = reader.GetOrdinal("product_name");
                            int categoryIndex = reader.GetOrdinal("product_category");
                            int genderIndex = reader.GetOrdinal("product_gender");
                            int urlIndex = reader.GetOrdinal("URL");

                            // 結果を1行ずつ処理
                            while (reader.Read())
                            {
                                // オブジェクトに結果を格納
                                resultList.List.Add(new subsc_product_tableRow
                                {
                                    product_id = reader.IsDBNull(idIndex) ? (int?)null : (int?)reader.GetInt32(idIndex),
                                    product_name = reader.IsDBNull(nameIndex) ? null : reader.GetString(nameIndex),
                                    product_category = reader.IsDBNull(categoryIndex) ? null : reader.GetString(categoryIndex),
                                    product_gender = reader.IsDBNull(genderIndex) ? null : reader.GetString(genderIndex),
                                    URL = reader.IsDBNull(urlIndex) ? null : reader.GetString(urlIndex)
                                });
                            }
                            // JSONオブジェクトを文字列に変換
                            responseMessage = JsonConvert.SerializeObject(resultList);
                        }
                    }
                }
            }
            // DB操作でエラーが発生した場合はここでキャッチ
            catch (SqlException e)
            {
                // エラーをコンソールに出力
                Console.WriteLine(e.ToString());
            }
            // 結果文字列を返却
            return new OkObjectResult(responseMessage);
        }




        //注文カートテーブル（一覧）
        [FunctionName("SELECT4")]
        public static async Task<IActionResult> Run4(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {

            //レスポンス用文字列
            string responseMessage = "SQL RESULT:";

            try
            {
                //接続文字列の設定
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = "m3hminagawafunction.database.windows.net";
                builder.UserID = "sqladmin";
                builder.Password = "Ynm004063";
                builder.InitialCatalog = "m3h-minagawa-fanctionDB";

                //接続用オブジェクトの初期化
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    Console.WriteLine("\nQuery data example:");
                    Console.WriteLine("=========================================\n");

                    //実行するクエリ
                    String sql = "SELECT order_id, product_id, user_id, product_size, quantity FROM subsc_ordercart_table";

                    //SQL実行オブジェクトの初期化
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        //DBと接続
                        connection.Open();

                        //SQLを実行し、結果をオブジェクトに格納
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            //結果を格納するためのオブジェクトを初期化
                            subsc_ordercart_tableList resultList = new subsc_ordercart_tableList();

                            //結果を1行ずつ処理
                            while (reader.Read())
                            {
                                //オブジェクトに結果を格納
                                resultList.List.Add(new subsc_ordercart_tableRow { order_id = reader.GetInt32("order_id"), product_id = reader.GetInt32("product_id"), user_id = reader.GetInt32("user_id"), product_size = reader.GetString("product_size"), quantity = reader.GetInt32("quantity") });
                            }
                            //JSONオブジェクトを文字列に変換
                            responseMessage = JsonConvert.SerializeObject(resultList);
                        }
                    }
                }
            }
            //DB操作でエラーが発生した場合はここでキャッチ
            catch (SqlException e)
            {
                //エラーをコンソールに出力
                Console.WriteLine(e.ToString());
            }
            //結果文字列を返却
            return new OkObjectResult(responseMessage);
        }


        // 商品テーブル（検索）
        [FunctionName("SELECT5")]
        public static async Task<IActionResult> Run5(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {

            // HTTPレスポンスで返す文字列を定義
            string responseMessage = "SELECT5 RESULT:";

            // SELECT5用のパラメーター取得（GETメソッド用）
            string product_category = req.Query["product_category"];
            string product_gender = req.Query["product_gender"];

            // SELECT5用のパラメーター取得（POSTメソッド用）
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            product_category = product_category ?? data?.product_category;
            product_gender = product_gender ?? data?.product_gender;

            // 両パラメーターを取得できた場合のみ処理
            if (!string.IsNullOrWhiteSpace(product_category) && !string.IsNullOrWhiteSpace(product_gender))
            {
                try
                {
                    // DB接続文字列の設定
                    SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                    builder.DataSource = "m3hminagawafunction.database.windows.net";
                    builder.UserID = "sqladmin";
                    builder.Password = "Ynm004063";
                    builder.InitialCatalog = "m3h-minagawa-fanctionDB";

                    // 接続用オブジェクトの初期化
                    using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                    {
                        Console.WriteLine("\nQuery data example:");
                        Console.WriteLine("=========================================\n");

                        // 実行するクエリ
                        String sql = "SELECT * FROM subsc_product_table WHERE product_category LIKE @product_category AND product_gender LIKE @product_gender";

                        // SQL実行オブジェクトの初期化
                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
                            // パラメーターを設定
                            command.Parameters.AddWithValue("@product_category", "%" + product_category + "%");
                            command.Parameters.AddWithValue("@product_gender", "%" + product_gender + "%");

                            // DBと接続
                            connection.Open();

                            // SQLを実行し、結果をオブジェクトに格納
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                // 結果を格納するためのオブジェクトを初期化
                                subsc_product_tableList resultList = new subsc_product_tableList();

                                // 列インデックスの取得
                                int categoryIndex = reader.GetOrdinal("product_category");
                                int genderIndex = reader.GetOrdinal("product_gender");
                                int nameIndex = reader.GetOrdinal("product_name");
                                int urlIndex = reader.GetOrdinal("URL");

                                // 結果を1行ずつ処理
                                while (reader.Read())
                                {
                                    // オブジェクトに結果を格納
                                    resultList.List.Add(new subsc_product_tableRow
                                    {
                                        product_name = reader.IsDBNull(nameIndex) ? null : reader.GetString(nameIndex),
                                        product_category = reader.IsDBNull(categoryIndex) ? null : reader.GetString(categoryIndex),
                                        product_gender = reader.IsDBNull(genderIndex) ? null : reader.GetString(genderIndex),
                                        URL = reader.IsDBNull(urlIndex) ? null : reader.GetString(urlIndex)
                                    });
                                }
                                // JSONオブジェクトを文字列に変換
                                responseMessage = JsonConvert.SerializeObject(resultList);
                            }
                        }
                    }
                }
                // DB操作でエラーが発生した場合はここでキャッチ
                catch (SqlException e)
                {
                    // エラーをコンソールに出力
                    Console.WriteLine(e.ToString());
                }
            }
            else
            {
                responseMessage = "パラメーターが設定されていません";
            }

            // HTTPレスポンスを返却
            return new OkObjectResult(responseMessage);
        }


        //注文詳細テーブル
        [FunctionName("SELECT6")]
        public static async Task<IActionResult> Run6(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            //レスポンス用文字列
            string responseMessage = "SQL RESULT:";

            try
            {
                // クエリパラメータから user_id を取得
                string userId = req.Query["user_id"];

                if (string.IsNullOrEmpty(userId))
                {
                    return new BadRequestObjectResult("user_id is required");
                }

                //接続文字列の設定
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = "m3hminagawafunction.database.windows.net";
                builder.UserID = "sqladmin";
                builder.Password = "Ynm004063";
                builder.InitialCatalog = "m3h-minagawa-fanctionDB";

                //接続用オブジェクトの初期化
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    Console.WriteLine("\nQuery data example:");
                    Console.WriteLine("=========================================\n");

                    //実行するクエリ。user_id でフィルタリング
                    String sql = "SELECT order_id, product_id, user_id, product_size, quantity FROM subsc_detail_table WHERE user_id = @user_id";

                    //SQL実行オブジェクトの初期化
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        // パラメータの追加
                        command.Parameters.AddWithValue("@user_id", userId);

                        //DBと接続
                        connection.Open();

                        //SQLを実行し、結果をオブジェクトに格納
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            //結果を格納するためのオブジェクトを初期化
                            subsc_detail_tableList resultList = new subsc_detail_tableList();

                            //結果を1行ずつ処理
                            while (reader.Read())
                            {
                                //オブジェクトに結果を格納
                                resultList.List.Add(new subsc_detail_tableRow
                                {
                                    order_id = reader.GetInt32(reader.GetOrdinal("order_id")),
                                    product_id = reader.GetInt32(reader.GetOrdinal("product_id")),
                                    user_id = reader.GetInt32(reader.GetOrdinal("user_id")),
                                    product_size = reader.GetString(reader.GetOrdinal("product_size")),
                                    quantity = reader.GetInt32(reader.GetOrdinal("quantity"))
                                });
                            }
                            //JSONオブジェクトを文字列に変換
                            responseMessage = JsonConvert.SerializeObject(resultList);
                        }
                    }
                }
            }
            //DB操作でエラーが発生した場合はここでキャッチ
            catch (SqlException e)
            {
                //エラーをコンソールに出力
                Console.WriteLine(e.ToString());
            }
            //結果文字列を返却
            return new OkObjectResult(responseMessage);
        }

        // 商品テーブル（検索）
        [FunctionName("SELECT7")]
        public static async Task<IActionResult> Run7(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {

            // HTTPレスポンスで返す文字列を定義
            string responseMessage = "SELECT7 RESULT:";

            // SELECT5用のパラメーター取得（GETメソッド用）
            string product_id = req.Query["product_id"];

            // SELECT5用のパラメーター取得（POSTメソッド用）
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            product_id = product_id ?? data?.product_id;

            // 両パラメーターを取得できた場合のみ処理
            if (!string.IsNullOrWhiteSpace(product_id))
            {
                try
                {
                    // DB接続文字列の設定
                    SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                    builder.DataSource = "m3hminagawafunction.database.windows.net";
                    builder.UserID = "sqladmin";
                    builder.Password = "Ynm004063";
                    builder.InitialCatalog = "m3h-minagawa-fanctionDB";

                    // 接続用オブジェクトの初期化
                    using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                    {
                        Console.WriteLine("\nQuery data example:");
                        Console.WriteLine("=========================================\n");

                        // 実行するクエリ
                        String sql = "SELECT * FROM subsc_product_table WHERE product_id LIKE @product_id";

                        // SQL実行オブジェクトの初期化
                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
                            // パラメーターを設定
                            command.Parameters.AddWithValue("@product_id", "%" + product_id + "%");

                            // DBと接続
                            connection.Open();

                            // SQLを実行し、結果をオブジェクトに格納
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                // 結果を格納するためのオブジェクトを初期化
                                subsc_product_tableList resultList = new subsc_product_tableList();

                                // 列インデックスの取得
                                int idIndex = reader.GetOrdinal("product_id");
                                int categoryIndex = reader.GetOrdinal("product_category");
                                int genderIndex = reader.GetOrdinal("product_gender");
                                int nameIndex = reader.GetOrdinal("product_name");
                                int urlIndex = reader.GetOrdinal("URL");

                                // 結果を1行ずつ処理
                                while (reader.Read())
                                {
                                    // オブジェクトに結果を格納
                                    resultList.List.Add(new subsc_product_tableRow
                                    {
                                        product_id = reader.IsDBNull(idIndex) ? (int?)null : (int?)reader.GetInt32(idIndex),
                                        product_name = reader.IsDBNull(nameIndex) ? null : reader.GetString(nameIndex),
                                        product_category = reader.IsDBNull(categoryIndex) ? null : reader.GetString(categoryIndex),
                                        product_gender = reader.IsDBNull(genderIndex) ? null : reader.GetString(genderIndex),
                                        URL = reader.IsDBNull(urlIndex) ? null : reader.GetString(urlIndex)
                                    });
                                }
                                // JSONオブジェクトを文字列に変換
                                responseMessage = JsonConvert.SerializeObject(resultList);
                            }
                        }
                    }
                }
                // DB操作でエラーが発生した場合はここでキャッチ
                catch (SqlException e)
                {
                    // エラーをコンソールに出力
                    Console.WriteLine(e.ToString());
                }
            }
            else
            {
                responseMessage = "パラメーターが設定されていません";
            }

            // HTTPレスポンスを返却
            return new OkObjectResult(responseMessage);
        }

        [FunctionName("SELECT8")]
        public static async Task<IActionResult> Run8(
           [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
           ILogger log)
        {

            //レスポンス用文字列
            string responseMessage = "SQL RESULT:";

            try
            {
                //接続文字列の設定
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = "m3hminagawafunction.database.windows.net";
                builder.UserID = "sqladmin";
                builder.Password = "Ynm004063";
                builder.InitialCatalog = "m3h-minagawa-fanctionDB";

                //接続用オブジェクトの初期化
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    Console.WriteLine("\nQuery data example:");
                    Console.WriteLine("=========================================\n");

                    //実行するクエリ
                    String sql = "SELECT order_id, user_id, product_size, quantity, product_id FROM subsc_detail_table quantity ";

                    //SQL実行オブジェクトの初期化
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        //DBと接続
                        connection.Open();

                        //SQLを実行し、結果をオブジェクトに格納
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            //結果を格納するためのオブジェクトを初期化
                            subsc_detail_tableList resultList = new subsc_detail_tableList();

                            //結果を1行ずつ処理
                            while (reader.Read())
                            {
                                //オブジェクトに結果を格納
                                resultList.List.Add(new subsc_detail_tableRow
                                {
                                    order_id = reader.GetInt32("order_id"),
                                    user_id = reader.GetInt32("user_id"),
                                    product_size = reader.GetString("product_size"),
                                    quantity = reader.GetInt32("quantity"),
                                    product_id = reader.GetInt32("product_id")
                                });
                            }
                            //JSONオブジェクトを文字列に変換
                            responseMessage = JsonConvert.SerializeObject(resultList);
                        }
                    }
                }
            }
            //DB操作でエラーが発生した場合はここでキャッチ
            catch (SqlException e)
            {
                //エラーをコンソールに出力
                Console.WriteLine(e.ToString());
            }
            //結果文字列を返却
            return new OkObjectResult(responseMessage);
        }

        //注文詳細テーブル
        [FunctionName("SELECT9")]
        public static async Task<IActionResult> Run9(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            //レスポンス用文字列
            string responseMessage = "SQL RESULT:";

            try
            {
                // クエリパラメータから user_id を取得
                string userId = req.Query["user_id"];

                if (string.IsNullOrEmpty(userId))
                {
                    return new BadRequestObjectResult("user_id is required");
                }

                //接続文字列の設定
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = "m3hminagawafunction.database.windows.net";
                builder.UserID = "sqladmin";
                builder.Password = "Ynm004063";
                builder.InitialCatalog = "m3h-minagawa-fanctionDB";

                //接続用オブジェクトの初期化
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    Console.WriteLine("\nQuery data example:");
                    Console.WriteLine("=========================================\n");

                    //実行するクエリ。user_id でフィルタリング
                    String sql = "SELECT user_id, user_name, user_pass, user_mail, user_postcode, user_adress, user_telenum FROM subsc_user_table WHERE user_id = @user_id";

                    //SQL実行オブジェクトの初期化
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        // パラメータの追加
                        command.Parameters.AddWithValue("@user_id", userId);

                        //DBと接続
                        connection.Open();

                        //SQLを実行し、結果をオブジェクトに格納
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            //結果を格納するためのオブジェクトを初期化
                            subsc_user_tableList resultList = new subsc_user_tableList();

                            //結果を1行ずつ処理
                            while (reader.Read())
                            {
                                //オブジェクトに結果を格納
                                resultList.List.Add(new subsc_user_tableRow
                                {
                                    user_id = reader.GetInt32(reader.GetOrdinal("user_id")),
                                    user_name = reader.GetString(reader.GetOrdinal("user_name")),
                                    user_pass = reader.GetString(reader.GetOrdinal("user_pass")),
                                    user_mail = reader.GetString(reader.GetOrdinal("user_mail")),
                                    user_postcode = reader.GetString(reader.GetOrdinal("user_postcode")),
                                    user_adress = reader.GetString(reader.GetOrdinal("user_adress")),
                                    user_telenum = reader.GetString(reader.GetOrdinal("user_telenum"))

                                });
                            }
                            //JSONオブジェクトを文字列に変換
                            responseMessage = JsonConvert.SerializeObject(resultList);
                        }
                    }
                }
            }
            //DB操作でエラーが発生した場合はここでキャッチ
            catch (SqlException e)
            {
                //エラーをコンソールに出力
                Console.WriteLine(e.ToString());
            }
            //結果文字列を返却
            return new OkObjectResult(responseMessage);
        }

        //注文詳細テーブル
        [FunctionName("SELECT10")]
        public static async Task<IActionResult> Run10(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            //レスポンス用文字列
            string responseMessage = "SQL RESULT:";

            try
            {
                // クエリパラメータから product_id を取得
                string productId = req.Query["product_id"];

                if (string.IsNullOrEmpty(productId))
                {
                    return new BadRequestObjectResult("product_id is required");
                }

                //接続文字列の設定
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = "m3hminagawafunction.database.windows.net";
                builder.UserID = "sqladmin";
                builder.Password = "Ynm004063";
                builder.InitialCatalog = "m3h-minagawa-fanctionDB";

                //接続用オブジェクトの初期化
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    Console.WriteLine("\nQuery data example:");
                    Console.WriteLine("=========================================\n");

                    //実行するクエリ。user_id でフィルタリング
                    String sql = "SELECT product_id, product_name, product_category, product_gender, URL FROM subsc_product_table WHERE product_id = @product_id";

                    //SQL実行オブジェクトの初期化
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        // パラメータの追加
                        command.Parameters.AddWithValue("@product_id", productId);

                        //DBと接続
                        connection.Open();

                        //SQLを実行し、結果をオブジェクトに格納
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            //結果を格納するためのオブジェクトを初期化
                            subsc_product_tableList resultList = new subsc_product_tableList();

                            //結果を1行ずつ処理
                            while (reader.Read())
                            {
                                //オブジェクトに結果を格納
                                resultList.List.Add(new subsc_product_tableRow
                                {
                                    product_id = reader.GetInt32(reader.GetOrdinal("product_id")),
                                    product_name = reader.GetString(reader.GetOrdinal("product_name")),
                                    product_category = reader.GetString(reader.GetOrdinal("product_category")),
                                    product_gender = reader.GetString(reader.GetOrdinal("product_gender")),
                                    URL = reader.GetString(reader.GetOrdinal("URL"))


                                });
                            }
                            //JSONオブジェクトを文字列に変換
                            responseMessage = JsonConvert.SerializeObject(resultList);
                        }
                    }
                }
            }
            //DB操作でエラーが発生した場合はここでキャッチ
            catch (SqlException e)
            {
                //エラーをコンソールに出力
                Console.WriteLine(e.ToString());
            }
            //結果文字列を返却
            return new OkObjectResult(responseMessage);
        }


        [FunctionName("INSERT")]
        public static async Task<IActionResult> RunInsert(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed an INSERT request.");

            // HTTPレスポンスで返す文字列を定義
            string responseMessage = "INSERT RESULT:";

            // インサート用のパラメーター取得（GETメソッド用）
            string user_name = req.Query["user_name"];
            string user_pass = req.Query["user_pass"];
            string user_mail = req.Query["user_mail"];
            string user_postcode = req.Query["user_postcode"];
            string user_adress = req.Query["user_adress"];
            string user_telenum = req.Query["user_telenum"];

            // インサート用のパラメーター取得（POSTメソッド用）
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            user_name = user_name ?? data?.user_name;
            user_pass = user_pass ?? data?.user_pass;
            user_mail = user_mail ?? data?.user_mail;
            user_postcode = user_postcode ?? data?.user_postcode;
            user_adress = user_adress ?? data?.user_adress;
            user_telenum = user_telenum ?? data?.user_telenum;

            // 全てのパラメーターが揃っている場合のみ処理
            if (!string.IsNullOrWhiteSpace(user_name) &&
                !string.IsNullOrWhiteSpace(user_pass) &&
                !string.IsNullOrWhiteSpace(user_mail) &&
                !string.IsNullOrWhiteSpace(user_postcode) &&
                !string.IsNullOrWhiteSpace(user_adress) &&
                !string.IsNullOrWhiteSpace(user_telenum))
            {
                try
                {
                    // DB接続設定（接続文字列の構築）
                    SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                    builder.DataSource = "m3hminagawafunction.database.windows.net";
                    builder.UserID = "sqladmin";
                    builder.Password = "Ynm004063";
                    builder.InitialCatalog = "m3h-minagawa-fanctionDB";

                    // SQLコネクションを初期化
                    using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                    {
                        // 実行するSQL（パラメーター付き）
                        String sql = "INSERT INTO subsc_user_table(user_name, user_pass, user_mail, user_postcode, user_adress, user_telenum) " +
                                     "VALUES(@user_name, @user_pass, @user_mail, @user_postcode, @user_adress, @user_telenum)";

                        // SQLコマンドを初期化
                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
                            // パラメーターを設定
                            command.Parameters.AddWithValue("@user_name", user_name);
                            command.Parameters.AddWithValue("@user_pass", user_pass);
                            command.Parameters.AddWithValue("@user_mail", user_mail);
                            command.Parameters.AddWithValue("@user_postcode", user_postcode);
                            command.Parameters.AddWithValue("@user_adress", user_adress);
                            command.Parameters.AddWithValue("@user_telenum", user_telenum);

                            // コネクションオープン（＝SQLDatabaseに接続）
                            connection.Open();

                            // SQLコマンドを実行し、挿入結果の行数を取得
                            int result = command.ExecuteNonQuery();

                            // レスポンス用にJSONオブジェクトに格納
                            JObject jsonObj = new JObject { ["result"] = $"{result} 行挿入されました" };

                            // JSONオブジェクトを文字列に変換
                            responseMessage = JsonConvert.SerializeObject(jsonObj, Formatting.None);
                        }
                    }
                }
                // DB処理でエラーが発生した場合
                catch (SqlException e)
                {
                    // コンソールにエラーを出力
                    Console.WriteLine(e.ToString());
                }
            }
            else
            {
                responseMessage = "パラメーターが設定されていません";
            }

            // HTTPレスポンスを返却
            return new OkObjectResult(responseMessage);
        }


        //商品INSERT
        [FunctionName("INSERT1")]
        public static async Task<IActionResult> RunInsert1(
  [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
  ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            //HTTPレスポンスで返す文字列を定義
            string responseMessage = "INSERT1 RESULT:";

            //インサート用のパラメーター取得（GETメソッド用）
            string product_name = req.Query["product_name"];
            string product_category = req.Query["product_category"];
            string product_gender = req.Query["product_gender"];
            string URL = req.Query["URL"];

            //インサート用のパラメーター取得（POSTメソッド用）
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            product_name = product_name ?? data?.product_name;
            product_category = product_category ?? data?.product_category;
            product_gender = product_gender ?? data?.product_gender;
            URL = URL ?? data?.URL;

            //両パラメーターを取得できた場合のみ処理
            if (!string.IsNullOrWhiteSpace(product_name) && !string.IsNullOrWhiteSpace(product_category) && !string.IsNullOrWhiteSpace(product_gender) && !string.IsNullOrWhiteSpace(URL))
            {
                try
                {
                    //DB接続設定（接続文字列の構築）
                    SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                    builder.DataSource = "m3hminagawafunction.database.windows.net";
                    builder.UserID = "sqladmin";
                    builder.Password = "Ynm004063";
                    builder.InitialCatalog = "m3h-minagawa-fanctionDB";


                    //SQLコネクションを初期化
                    using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                    {

                        //実行するSQL（パラメーター付き）
                        String sql = "INSERT INTO subsc_product_table(product_name, product_category, product_gender, URL) VALUES(@product_name, @product_category, @product_gender, @URL)";

                        //SQLコマンドを初期化
                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
                            //パラメーターを設定
                            command.Parameters.AddWithValue("@product_name", product_name);
                            command.Parameters.AddWithValue("@product_category", product_category);
                            command.Parameters.AddWithValue("@product_gender", product_gender);
                            command.Parameters.AddWithValue("@URL", URL);

                            //コネクションオープン（＝　SQLDatabaseに接続）
                            connection.Open();

                            //SQLコマンドを実行し結果行数を取得
                            int result = command.ExecuteNonQuery();

                            //レスポンス用にJSONオブジェクトに格納
                            JObject jsonObj = new JObject { ["result"] = $"{result}行挿入されました" };

                            //JSONオブジェクトを文字列に変換
                            responseMessage = JsonConvert.SerializeObject(jsonObj, Formatting.None);
                        }
                    }
                }

                //DB処理でエラーが発生した場合
                catch (SqlException e)
                {
                    //コンソールにエラーを出力
                    Console.WriteLine(e.ToString());
                }

            }
            else
            {
                responseMessage = "パラメーターが設定されていません";
            }

            //HTTPレスポンスを返却
            return new OkObjectResult(responseMessage);
        }

        //カートINSERT
        [FunctionName("INSERT2")]
        public static async Task<IActionResult> RunInsert2(
  [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
  ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            //HTTPレスポンスで返す文字列を定義
            string responseMessage = "INSERT2 RESULT:";

            //インサート用のパラメーター取得（GETメソッド用）
            string product_id = req.Query["product_id"];
            string user_id = req.Query["user_id"];
            string product_size = req.Query["product_size"];
            string quantity = req.Query["quantity"];

            //インサート用のパラメーター取得（POSTメソッド用）
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            product_id = product_id ?? data?.product_id;
            user_id = user_id ?? data?.user_id;
            product_size = product_size ?? data?.product_size;
            quantity = quantity ?? data?.quantity;

            //両パラメーターを取得できた場合のみ処理
            if (!string.IsNullOrWhiteSpace(product_id) && !string.IsNullOrWhiteSpace(user_id) && !string.IsNullOrWhiteSpace(product_size) && !string.IsNullOrWhiteSpace(quantity))
            {
                try
                {
                    //DB接続設定（接続文字列の構築）
                    SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                    builder.DataSource = "m3hminagawafunction.database.windows.net";
                    builder.UserID = "sqladmin";
                    builder.Password = "Ynm004063";
                    builder.InitialCatalog = "m3h-minagawa-fanctionDB";


                    //SQLコネクションを初期化
                    using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                    {

                        //実行するSQL（パラメーター付き）
                        String sql = "INSERT INTO subsc_ordercart_table(product_id, user_id, product_size, quantity) VALUES(@product_id, @user_id, @product_size, @quantity)";

                        //SQLコマンドを初期化
                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
                            //パラメーターを設定
                            command.Parameters.AddWithValue("@product_id", int.Parse(product_id));
                            command.Parameters.AddWithValue("@user_id", int.Parse(user_id));
                            command.Parameters.AddWithValue("@product_size", product_size);
                            command.Parameters.AddWithValue("@quantity", quantity);

                            //コネクションオープン（＝　SQLDatabaseに接続）
                            connection.Open();

                            //SQLコマンドを実行し結果行数を取得
                            int result = command.ExecuteNonQuery();

                            //レスポンス用にJSONオブジェクトに格納
                            JObject jsonObj = new JObject { ["result"] = $"{result}行挿入されました" };

                            //JSONオブジェクトを文字列に変換
                            responseMessage = JsonConvert.SerializeObject(jsonObj, Formatting.None);
                        }
                    }
                }

                //DB処理でエラーが発生した場合
                catch (SqlException e)
                {
                    //コンソールにエラーを出力
                    Console.WriteLine(e.ToString());
                }

            }
            else
            {
                responseMessage = "パラメーターが設定されていません";
            }

            //HTTPレスポンスを返却
            return new OkObjectResult(responseMessage);
        }
        //注文詳細テーブルINSERT
        [FunctionName("INSERT3")]
        public static async Task<IActionResult> RunInsert3(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            //HTTPレスポンスで返す文字列を定義
            string responseMessage = "INSERT3 RESULT:";

            //インサート用のパラメーター取得（GETメソッド用）
            string order_id = req.Query["order_id"];
            string product_id = req.Query["product_id"];
            string user_id = req.Query["user_id"];
            string product_size = req.Query["product_size"];
            string quantity = req.Query["quantity"];

            //インサート用のパラメーター取得（POSTメソッド用）
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            order_id = order_id ?? data?.order_id;
            product_id = product_id ?? data?.product_id;
            user_id = user_id ?? data?.user_id;
            product_size = product_size ?? data?.product_size;
            quantity = quantity ?? data?.quantity;

            //両パラメーターを取得できた場合のみ処理
            if (!string.IsNullOrWhiteSpace(order_id) &&
                !string.IsNullOrWhiteSpace(product_id) &&
                !string.IsNullOrWhiteSpace(user_id) &&
                !string.IsNullOrWhiteSpace(product_size) &&
                !string.IsNullOrWhiteSpace(quantity))
            {
                try
                {
                    //DB接続設定（接続文字列の構築）
                    SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                    builder.DataSource = "m3hminagawafunction.database.windows.net";
                    builder.UserID = "sqladmin";
                    builder.Password = "Ynm004063";
                    builder.InitialCatalog = "m3h-minagawa-fanctionDB";

                    //SQLコネクションを初期化
                    using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                    {
                        //実行するSQL（パラメーター付き）
                        String sql = "INSERT INTO subsc_detail_table(order_id, product_id, user_id, product_size, quantity) " +
                                     "VALUES(@order_id, @product_id, @user_id, @product_size, @quantity)";

                        //SQLコマンドを初期化
                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
                            //パラメーターを設定
                            command.Parameters.AddWithValue("@order_id", int.Parse(order_id));
                            command.Parameters.AddWithValue("@product_id", int.Parse(product_id));
                            command.Parameters.AddWithValue("@user_id", int.Parse(user_id));
                            command.Parameters.AddWithValue("@product_size", product_size);
                            command.Parameters.AddWithValue("@quantity", int.Parse(quantity));

                            //コネクションオープン（＝　SQLDatabaseに接続）
                            connection.Open();

                            //SQLコマンドを実行し結果行数を取得
                            int result = command.ExecuteNonQuery();

                            //レスポンス用にJSONオブジェクトに格納
                            JObject jsonObj = new JObject { ["result"] = $"{result}行挿入されました" };

                            //JSONオブジェクトを文字列に変換
                            responseMessage = JsonConvert.SerializeObject(jsonObj, Formatting.None);
                        }
                    }
                }
                //DB処理でエラーが発生した場合
                catch (SqlException e)
                {
                    //コンソールにエラーを出力
                    Console.WriteLine(e.ToString());
                }
            }
            else
            {
                responseMessage = "パラメーターが設定されていません";
            }

            //HTTPレスポンスを返却
            return new OkObjectResult(responseMessage);
        }


        //商品DELETE1
        [FunctionName("DELETE1")]
        public static async Task<IActionResult> RunDelete1(
  [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
  ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            //HTTPレスポンスで返す文字列を定義
            string responseMessage = "DELETE1 RESULT:";

            //インサート用のパラメーター取得（GETメソッド用）
            string product_name = req.Query["product_name"];
            string product_category = req.Query["product_category"];
            string product_gender = req.Query["product_gender"];
            string URL = req.Query["URL"];

            //DELETE用のパラメーター取得（POSTメソッド用）
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            product_name = product_name ?? data?.product_name;
            product_category = product_category ?? data?.product_category;
            product_gender = product_gender ?? data?.product_gender;
            URL = URL ?? data?.URL;


            //両パラメーターを取得できた場合のみ処理
            if (!string.IsNullOrWhiteSpace(product_name) && !string.IsNullOrWhiteSpace(product_category) && !string.IsNullOrWhiteSpace(product_gender) && !string.IsNullOrWhiteSpace(URL))
            {
                try
                {
                    //DB接続設定（接続文字列の構築）
                    SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                    builder.DataSource = "m3hminagawafunction.database.windows.net";
                    builder.UserID = "sqladmin";
                    builder.Password = "Ynm004063";
                    builder.InitialCatalog = "m3h-minagawa-fanctionDB";


                    //SQLコネクションを初期化
                    using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                    {

                        //実行するSQL（パラメーター付き）
                        String sql = "DELETE FROM  subsc_product_table WHERE product_name LIKE @product_name AND  product_category LIKE @product_category AND  product_gender LIKE @product_gender AND URL LIKE @URL";

                        //SQLコマンドを初期化
                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
                            //パラメーターを設定
                            command.Parameters.AddWithValue("@product_name", product_name);
                            command.Parameters.AddWithValue("@product_category", product_category);
                            command.Parameters.AddWithValue("@product_gender", product_gender);
                            command.Parameters.AddWithValue("@URL", URL);

                            //コネクションオープン（＝　SQLDatabaseに接続）
                            connection.Open();

                            //SQLコマンドを実行し結果行数を取得
                            int result = command.ExecuteNonQuery();

                            //レスポンス用にJSONオブジェクトに格納
                            JObject jsonObj = new JObject { ["result"] = $"{result}行削除されました" };

                            //JSONオブジェクトを文字列に変換
                            responseMessage = JsonConvert.SerializeObject(jsonObj, Formatting.None);
                        }
                    }
                }

                //DB処理でエラーが発生した場合
                catch (SqlException e)
                {
                    //コンソールにエラーを出力
                    Console.WriteLine(e.ToString());
                }

            }
            else
            {
                responseMessage = "パラメーターが設定されていません";
            }

            //HTTPレスポンスを返却
            return new OkObjectResult(responseMessage);
        }

        //注文カートDELETE2
        [FunctionName("DELETE2")]
        public static async Task<IActionResult> RunDelete2(
  [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
  ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            //HTTPレスポンスで返す文字列を定義
            string responseMessage = "DELETE2 RESULT:";

            //インサート用のパラメーター取得（GETメソッド用）
            string order_id = req.Query["order_id"];
            string product_id = req.Query["product_id"];
            string user_id = req.Query["user_id"];
            string product_size = req.Query["product_size"];
            string quantity = req.Query["quantity"];

            //DELETE用のパラメーター取得（POSTメソッド用）
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            order_id = order_id ?? data?.order_id;
            product_id = product_id ?? data?.product_id;
            user_id = user_id ?? data?.user_id;
            product_size = product_size ?? data?.product_size;
            quantity = quantity ?? data?.quantity;

            //両パラメーターを取得できた場合のみ処理
            if (!string.IsNullOrWhiteSpace(order_id) && !string.IsNullOrWhiteSpace(product_id) && !string.IsNullOrWhiteSpace(user_id) && !string.IsNullOrWhiteSpace(product_size) && !string.IsNullOrWhiteSpace(quantity))
            {
                try
                {
                    //DB接続設定（接続文字列の構築）
                    SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                    builder.DataSource = "m3hminagawafunction.database.windows.net";
                    builder.UserID = "sqladmin";
                    builder.Password = "Ynm004063";
                    builder.InitialCatalog = "m3h-minagawa-fanctionDB";


                    //SQLコネクションを初期化
                    using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                    {

                        //実行するSQL（パラメーター付き）
                        String sql = "DELETE FROM  subsc_ordercart_table WHERE order_id LIKE @order_id AND product_id LIKE @product_id AND user_id LIKE @user_id AND  product_size LIKE @product_size AND quantity LIKE @quantity";

                        //SQLコマンドを初期化
                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
                            //パラメーターを設定
                            command.Parameters.AddWithValue("@order_id", int.Parse(order_id));
                            command.Parameters.AddWithValue("@product_id", int.Parse(product_id));
                            command.Parameters.AddWithValue("@user_id", int.Parse(user_id));
                            command.Parameters.AddWithValue("@product_size", product_size);
                            command.Parameters.AddWithValue("@quantity", quantity);


                            //コネクションオープン（＝　SQLDatabaseに接続）
                            connection.Open();

                            //SQLコマンドを実行し結果行数を取得
                            int result = command.ExecuteNonQuery();

                            //レスポンス用にJSONオブジェクトに格納
                            JObject jsonObj = new JObject { ["result"] = $"{result}行削除されました" };

                            //JSONオブジェクトを文字列に変換
                            responseMessage = JsonConvert.SerializeObject(jsonObj, Formatting.None);
                        }
                    }
                }

                //DB処理でエラーが発生した場合
                catch (SqlException e)
                {
                    //コンソールにエラーを出力
                    Console.WriteLine(e.ToString());
                }

            }
            else
            {
                responseMessage = "パラメーターが設定されていません";
            }

            //HTTPレスポンスを返却
            return new OkObjectResult(responseMessage);
        }

    }
}
