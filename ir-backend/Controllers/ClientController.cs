using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ir_backend.Models;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.Runtime;

namespace ir_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private static AmazonDynamoDBClient client = new AmazonDynamoDBClient();
        private static DynamoDBContext context = new DynamoDBContext(client);

        public ClientController()
        {
        }

        [HttpGet("/listclientes")]
        public async Task<ActionResult<List<ClientEntity>>> GetAllClient()
        {
            Console.WriteLine("Comenzando la lectura de la lista de clientes");

            List<ScanCondition> conditions = new List<ScanCondition>();

            List<ClientEntity> allClients = await context.ScanAsync<ClientEntity>(conditions).GetRemainingAsync();

            return Ok(allClients);
        }

        [HttpPost("/creacliente")]
        public async Task<ActionResult<string>> PostClient(ClientEntity clientEntity)
        {
            await context.SaveAsync(clientEntity);

            return Ok("Se ha creado con éxito el cliente.");
        }

        [HttpGet("/kpiclientes")]
        public async Task<ActionResult<KPIModel>> GetKpi()
        {
            Console.WriteLine("Comenzando la lectura de la lista de clientes");

            List<ScanCondition> conditions = new List<ScanCondition>();

            List<ClientEntity> allClients = await context.ScanAsync<ClientEntity>(conditions).GetRemainingAsync();

            KPIModel kpiModel = new KPIModel();

            Console.WriteLine("Calculando el promedio de edades");

            kpiModel.AverageAge = allClients.Average(element => element.Age);

            double summation = allClients.Sum(element => Math.Pow(element.Age - kpiModel.AverageAge, 2));

            Console.WriteLine("Calculando la desviacion estandar");

            kpiModel.StandardDeviation = Math.Sqrt((summation) / (allClients.Count() - 1));

            return Ok(kpiModel);
        }
    }
}
