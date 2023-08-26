using CS.Staff.Models;
using CS.Staff.Repositories.Interfaces;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS.Staff.Repositories
{
    public class DepartmentRepository : BaseRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(CosmosClient cosmosClient, DatabaseSettings settings) 
            : base(cosmosClient, settings.DatabaseId, settings.ContainerId)
        {

        }
    }
}
