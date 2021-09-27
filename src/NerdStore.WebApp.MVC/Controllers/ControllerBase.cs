using Microsoft.AspNetCore.Mvc;
using System;

namespace NerdStore.WebApp.MVC.Controllers
{
    public abstract class ControllerBase : Controller
    {
        protected Guid ClienteId = Guid.Parse("54f589fc-2f22-4980-a097-e29828020119"); 
    }
}
