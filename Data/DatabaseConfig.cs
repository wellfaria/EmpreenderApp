using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace EmpreendedoresApp.Data
{
    public static class DatabaseConfig
    {
        public static string CaminhoBanco
        {
            get { 

            var pasta =  Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "EmpreendedoresApp");

                if (!Directory.Exists(pasta))
                    Directory.CreateDirectory(pasta);

                    return Path.Combine(pasta, "sistema.db");
                

            }
        }
    }
}
