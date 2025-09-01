using CCC_Rugby_Web.Models.Entityes;
using CCC_Rugby_Web.Models.Repositories;
using CCC_Rugby_Web.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CCC_Rugby_Web.Controllers
{
    public abstract class GenericController : ControllerBase
    {
        protected readonly EntityManager entityManager;

        public GenericController(EntityManager entityManager)
        {
            this.entityManager = entityManager;
        }
    }
}
