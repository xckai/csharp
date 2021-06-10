using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using DapperTest.Model;
using MySql.Data.MySqlClient;

namespace DapperTest
{
    class Program
    {
        static void Main(string[] args)
        {
            BaseUsage();
            BaseQuery();
            Console.WriteLine("Hello World!");
        }

        static void BaseUsage()
        {
            const string _connectionString = "Database=kai_test;Data Source=10.10.10.136;User Id=xckai;Password=123465;pooling=false;CharSet=utf8;port=3306;";
            using (IDbConnection dbConnection = new MySqlConnection(_connectionString))
            {
                dbConnection.Open();
                //通过匿名类型插入单条数据
                dbConnection.Execute("insert into user(UUID,name) values(@UUID,@Name)", new { UUID = "dfad-sss3-sfad-3s对a", Name = "测试账号1" });
                //批量插入数据
                List<User> schools = new List<User>()
                {
                    new User() {UUID = "1234-4567-2245-11113",Name = "杭州"},
                    new User()  { UUID = "1234-4567-2245-11113",Name = "sss"},
                    new User()  { UUID = "1234-4567-2245-11113",Name = "dfadf"},
                };
                //在执行参数化的SQL时，SQL中的参数（如@title可以和数据表中的字段不一致，但要和实体类型的属性Title相对应）
                dbConnection.Execute("insert into user(uuid,name) values(@UUID,@Name);", schools);
                //通过匿名类型批量插入数据
               
            }
        }

        static void BaseQuery()
        {
            const string _connectionString = "Database=kai_test;Data Source=10.10.10.136;User Id=xckai;Password=123465;pooling=false;CharSet=utf8;port=3306;";
            using (IDbConnection dbConnection = new MySqlConnection(_connectionString))
            {
                dbConnection.Open();
                //通过匿名类型插入单条数据
                var schools = dbConnection.Query<User>("select * from user").Select(x =>
                {
                    x.NickName = x.Name;
                    return x;
                });
                foreach (var user in schools)
                {
                    Console.WriteLine(user.NickName);
                }

            }
        }
    }
}
