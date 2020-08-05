using Npgsql;
using System;
using System.ComponentModel;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Text.RegularExpressions;

namespace DenisTestTask
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:/Users/USER/source/repos/DenisTestTask/EventService.txt";

            string[] str = new string[224];
            string RequestFor = "Request for";
            string pattern = @"\B*Request for\B*";
            int i = 0;
            try
            {
                using (StreamReader srLine = new StreamReader(path, System.Text.Encoding.Default))
                {
                    string line;
                    while ((line = srLine.ReadLine()) != null)
                    {
                        foreach (Match match in Regex.Matches(line, pattern, RegexOptions.IgnoreCase))
                        {
                            str[i] = line;
                            i++;
                        }
                    }
                }
            }
            catch (Exception e) { Console.WriteLine(e.Message); }

            string[] data = new string[i];
            string[] time = new string[i];
            string[] x = new string[i];
            string[] y = new string[i];
            string[] z = new string[i];
            string[] k = new string[i];
            string[] temp = new string[i];

            string templine;


            string connString = @"Host=drona.db.elephantsql.com;Username=wzhipdfv;Password=md6DdY3CNDgI0kEni6HU4mMMJi6H8fks;Database=wzhipdfv;";
            NpgsqlConnection conn = new NpgsqlConnection(connString);
            conn.Open();

            for (i = 0; i < str.Length; i++)
            {
                data[i] = str[i].Substring(0, 10);
                time[i] = str[i].Substring(11, 8);
                temp[i] = str[i].Substring(str[i].LastIndexOf(RequestFor));
                temp[i] = temp[i].Substring(12);
                templine = temp[i];
                StringBuilder stringBuilder = new StringBuilder(templine);
                stringBuilder.Replace('_', ' ');
                templine = stringBuilder.ToString();
                x[i] = templine.Substring(0, templine.IndexOf(' '));
                y[i] = templine.Substring(templine.IndexOf(' '));
                y[i] = y[i].Trim(new char[] { ' ' });
                y[i] = y[i].Substring(0, y[i].IndexOf(' '));
                z[i] = templine.Substring(templine.IndexOf(' '));
                z[i] = z[i].Trim(new char[] { ' ' });
                z[i] = z[i].Substring(z[i].IndexOf(' '));
                z[i] = z[i].Substring(1, z[i].LastIndexOf(' ')).Trim(new char[] { ' ', ' ' });
                k[i] = templine.Substring(templine.LastIndexOf(' '));
                k[i] = k[i].Trim(new char[] { ' ' });
                Console.WriteLine(data[i] + " " + time[i] + " " + x[i] + " " + y[i] + " " + z[i] + " " + k[i]);
                /*
                NpgsqlCommand cmd = new NpgsqlCommand("insert into from_log (dtt_data, dtt_time, dtt_x, dtt_y, dtt_z, dtt_k) values (@data,@time,@x,@y,@z,@k)", conn);
                cmd.Parameters.AddWithValue("@data", data[i]);
                cmd.Parameters.AddWithValue("@time", time[i]);
                cmd.Parameters.AddWithValue("@x", x[i]);
                cmd.Parameters.AddWithValue("@y", y[i]);
                cmd.Parameters.AddWithValue("@z", z[i]);
                cmd.Parameters.AddWithValue("@k", k[i]);
                cmd.ExecuteNonQuery();
                */
            }
            conn.Close();
        }
    }
}
