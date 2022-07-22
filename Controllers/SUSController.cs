using CPMS_AUTO.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPMS_AUTO.Controllers
{
    public class SUSController : Controller
    {
        private readonly RepositorySus _repositorySus;
        private readonly RepositoryUnitStatus _repositoryUnitStatus;

        public SUSController(RepositorySus repositorySus,RepositoryUnitStatus repositoryUnitStatus)
        {
            _repositorySus = repositorySus;
            _repositoryUnitStatus = repositoryUnitStatus;
        }
       
    }
}
