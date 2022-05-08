using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace Bulutbilisim.Models.OrmConfigration
{
    public class DapperService
    {
        public static string connstring = "";
        public static string connstringremote = "";


        public static IDbConnection _dbConnection
        {
            get
            {

                connstring = "Data Source=185.59.74.10;Initial Catalog=bulutbilisim;Integrated Security=False;Persist Security Info=False;User ID=sa;Password=nht25*kzm30;MultipleActiveResultSets=true";


                return new SqlConnection(connstring);
            }
        }

        public static IDbConnection _dbremoteConnection
        {
            get
            {
                if (connstringremote.Length < 5)
                {
                    connstringremote = "Data Source=185.59.74.10;Initial Catalog=bulutbilisim;Integrated Security=False;Persist Security Info=False;User ID=sa;Password=nht25*kzm30;MultipleActiveResultSets=true";
                }

                return new SqlConnection(connstringremote);
            }
        }

    }

    public class settingFile
    {
        public string DBConString { get; set; }
    }

}
