using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using TestRP.Model;
using Microsoft.Data.Sqlite;

namespace TestRP.Data
{
        public class GetProducts {

        public int ExecuteWrite(string query, Dictionary<string, object> args)
            {
                int numberOfRowsAffected;

                //setup the connection to the database
                using (var con = new SqliteConnection("Data Source=Product.db"))
                {
                    con.Open();
                    
                    //open a new command
                    using (var cmd = new SqliteCommand(query, con))
                    {
                        //set the arguments given in the query
                        foreach (var pair in args)
                        {
                            cmd.Parameters.AddWithValue(pair.Key, pair.Value);
                        }

                        //execute the query and get the number of row affected
                        numberOfRowsAffected = cmd.ExecuteNonQuery();
                    }

                    return numberOfRowsAffected;
                }
            }

            public List<Product> ExecuteRead(string query,Dictionary<string, object> args)
                {
                    string startupPath = System.IO.Directory.GetCurrentDirectory();
                    Console.WriteLine("startupPath",startupPath);
                     List<Product> entries = new List<Product>();
                    if (string.IsNullOrEmpty(query.Trim()))
                        return null;

                    using (var con = new SqliteConnection("Data Source=Product.db"))
                    {
                        con.Open();
                        SqliteCommand cmd = new SqliteCommand(query, con);
                        SqliteDataReader res = cmd.ExecuteReader();
                         while (res.Read())
                            {//ver que llega
                                // entries.Add(res);
                                
                            }

                            con.Close();
                            return entries;
                        }
                    
                }

                public  List<Product> Execute(string query)
                {
                    try{
                    string startupPath = System.IO.Directory.GetCurrentDirectory();
                    Console.WriteLine("startupPath",startupPath);
                    List<Product> entries = new List<Product>();
                    if (string.IsNullOrEmpty(query.Trim()))
                        return null;

                    using (var con = new SqliteConnection("Data Source=Product.db"))
                    {
                        con.Open();
                        SqliteCommand cmd = new SqliteCommand(query, con);
                        SqliteDataReader res = cmd.ExecuteReader();
                         while (res.Read())
                            {
                                // entries.Add(res);
                                 // entries.Add(query.GetString(0));
                            }

                            con.Close();
                

                            return entries;
                        
                    }
                    }
                    catch(Exception ex)
                    {
                        return null;
                    }
                }


        }

}