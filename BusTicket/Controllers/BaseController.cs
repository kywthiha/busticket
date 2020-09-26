using BusTicket.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BusTicket.Controllers
{
    public class BaseController:Controller
    {
        protected BusTicketDataContext _context { get; }
        protected  IAuthorizationService _authorizationService { get; }
        protected  UserManager<IdentityUser> _userManager { get; }

        public BaseController(BusTicketDataContext context, IAuthorizationService authorizationService, UserManager<IdentityUser> userManager)
        :base(){
            _context = context;
            _authorizationService = authorizationService;
            _userManager = userManager;
        }
    }
}
