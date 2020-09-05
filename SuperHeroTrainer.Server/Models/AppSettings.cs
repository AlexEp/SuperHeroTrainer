using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperHeroTrainer.Models
{
    public class AppSettings
    {
        public CORSSettings CORS { get; set; }
        public JWTSettings JWT { get; set; }
        public DBSettings DB { get; set; }
    }

    public class DBSettings
    {
        public string DefaultConnection { get; set; }
        public string IdentityConnection { get; set; }
    }

    public class CORSSettings
    {
        public string FrontEndURL { get; set; }
    }

    public class JWTSettings
    {
        public string SecretKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }

}
