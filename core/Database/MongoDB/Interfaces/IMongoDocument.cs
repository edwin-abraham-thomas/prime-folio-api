using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Database.MongoDB.Interfaces
{
    public interface IMongoDocument
    {
        public string _id { get; set; }
    }
}
