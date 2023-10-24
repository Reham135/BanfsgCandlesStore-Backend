using Banfsg.BL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using System.Security.Claims;

namespace Banfsg.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [EnableRateLimiting("fixed")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersManager _ordersManager;

        public OrdersController(IOrdersManager ordersManager)
        {
            _ordersManager = ordersManager;
        }

        #region Make New Order
        [HttpPost]
        [Route("AddOrder")]
        public ActionResult MakeNewOrder(int addressId)
        {
            var UID = User.Claims.FirstOrDefault(c=>c.Type==ClaimTypes.NameIdentifier)?.Value;
            if(UID == null) { return BadRequest("Not Logged in !!"); }
            _ordersManager.AddNewOrder(UID, addressId);
            var response= new {message="Order Added Successfully"};
            return Ok(response);
        }
        #endregion
    }
}
