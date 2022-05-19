
using Prodesp.Gsnet.Framework;
using System;
using System.Text;

namespace Prodesp.Infra.EF.Helpers
{
    public class ConnectionStringToken
    {
        public string Saltkey { get; set; }

        public string ConnectionString { get; set; }

        public static ConnectionStringToken? Parse(string base64ConnectionString)
        {
            try
            {
                ConnectionStringToken connectionStringToken = HelperJSON.Deserialize<ConnectionStringToken>(Encoding.UTF8.GetString(Convert.FromBase64String(base64ConnectionString)));
                string str = HelperSecurity.Decrypt(connectionStringToken.ConnectionString, connectionStringToken.Saltkey);
                return new ConnectionStringToken()
                {
                    Saltkey = connectionStringToken.Saltkey,
                    ConnectionString = str
                };
            }
            catch
            {
                return null;
            }
        }

        public override string ToString()
        {
            try
            {
                string str = HelperSecurity.Encrypt(this.ConnectionString, this.Saltkey);
                return Convert.ToBase64String(Encoding.UTF8.GetBytes(HelperJSON.Serialize<ConnectionStringToken>(new ConnectionStringToken()
                {
                    Saltkey = this.Saltkey,
                    ConnectionString = str
                })));
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}
