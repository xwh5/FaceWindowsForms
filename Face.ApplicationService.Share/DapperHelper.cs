using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using static Mysqlx.Expect.Open.Types.Condition.Types;
using System.Xml.Linq;

namespace Face.ApplicationService.Share
{
    public class DapperHelper : IDisposable
    {
        public IDbConnection con;

        public DapperHelper(string configuration= "Data Source=mysql.rms.staging.yhglobal.cn;Port=3314;Database=YH.RMS;uid=root;pwd=MYN5kVZ9KytkhcKZ;charset=utf8mb4;Allow User Variables=true;min pool size=5;max pool size=1024;Connection Timeout=30;default command timeout=90; AllowLoadLocalInfile=true;")
        {
            // var connectionString = configuration.GetConnectionString("Default");
            con = new MySqlConnection(configuration);
        }
        private const string sql_add = "insert FaceInfo (id,name,feature,creationTime,faceType) values(@id,@name,@feature,@creationTime,@faceType)";
        private const string sql_exist = "select name from FaceInfo where name=@name and faceType=@key";
        public async Task Add<T>(string name, T feat,string key)
        {
            
            await con.ExecuteAsync(sql_add, new
            {
                id = Guid.NewGuid(),
                name = name,
                feature = feat,
                creationTime = DateTime.Now,
                faceType=key
            });
        }

        public async Task<bool> IsExist(string name, string key) {
            var ise = await con.QuerySingleOrDefaultAsync<string>(sql_exist, new { name, key });
            return !string.IsNullOrWhiteSpace(ise);
        }

        private const string sql_get = "select * from FaceInfo where faceType=@faceType";
        public async Task<IEnumerable<FaceInfo<T>>> GetList<T>(string faceType)
        {
            return await con.QueryAsync<FaceInfo<T>>(sql_get, new { faceType });
        }

        public void Dispose()
        {
            con?.Close();
            con?.Dispose();
        }
    }

    public class FaceInfo<T>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public T Feature { get; set; }
        public DateTime CreationTime { get; set; }
        public string faceType { get; set; }
    }
}
