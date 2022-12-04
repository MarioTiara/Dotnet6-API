using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.Api.Config
{
    public class MongoDbConfig
    {
        public string Host { get; set; } 
        public int Port {get;set;}
        public string DatabaseName {get;set;}
        public string User {get;set;}
        public string Password {get;set;}
        public string ConnectionString{
            get{
                return $"mongodb://{User}:{Password}@{Host}:{Port}";
            }
        }
    }
}