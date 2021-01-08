using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.Runtime;

namespace ir_backend.Models
{ 
    [DynamoDBTable("client")]
    public class ClientEntity
    {
        [DynamoDBHashKey]
        public string Id { get; set; }

        [DynamoDBProperty("Nombre")]
        public string FirstName { get; set; }

        [DynamoDBProperty("Apellido")]
        public string LastName { get; set; }

        [DynamoDBProperty("Edad")]
        public int Age { get; set; }

        [DynamoDBProperty("FechaNacimiento")]
        public string FechaDeNacimiento { get; set; }
    }
}
