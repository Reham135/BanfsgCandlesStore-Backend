using Banfsg.BL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;

namespace Banfsg.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [EnableRateLimiting("fixed")]
    public class UserProductCartController : ControllerBase
    {
        private readonly IUserProductCartManager _userProductCartManager;

        public UserProductCartController(IUserProductCartManager userProductCartManager)
        {
            _userProductCartManager = userProductCartManager;
        }



        #region Get All Products In Cart
        [HttpGet]
        [Route("GetAllProducts")]
        public ActionResult<IEnumerable<ProductCartReadDto>>GetAllProductsInCart()
        {
            var UID=User.Claims.FirstOrDefault(c=>c.Type==ClaimTypes.NameIdentifier)?.Value;
            if (UID is null) { return BadRequest("not logged in");  }
            var products = _userProductCartManager.GetAllProductsInCartByUID(UID);
            return Ok(products);
        }
        #endregion

        #region Add Product to cart
        [HttpPost]
        [Route("AddProduct")]
        public ActionResult AddProductToCart(AddProductCartDto AddProductDto)
        {
            var UID = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (UID is null) { return BadRequest("not logged in"); }
            var response = _userProductCartManager.AddProductToCart(UID, AddProductDto);
            return Ok(new {message=response});
        }
        #endregion

        #region Update Product Quantity In Cart
        [HttpPatch]
        [Route("UpdateProduct")]
        public ActionResult updateProductQuantityInCart(ProductQuantityCartDto productQuantityCartDto)
        {
            var UID = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (UID is null) { return BadRequest("not logged in"); }
            var response = _userProductCartManager.updateProductQuantityInCart(UID, productQuantityCartDto);
            return Ok(new { message = response });
        }
        #endregion

        #region Delete product from cart
        [HttpDelete]
        [Route("DeleteProduct/{id}")]
        public ActionResult DeleteProductFromCart(int id)
        {
            var UID = User.Claims.FirstOrDefault(c=>c.Type==ClaimTypes.NameIdentifier)?.Value;
            if (UID is null) { return BadRequest("not logged in"); }
            _userProductCartManager.DeleteProductFromCart(UID, id);
            return Ok("product Deleted from Cart");
        }
        #endregion

        #region Get Cart Products Counter
        [HttpGet]
        [Route("Counter")]
        public ActionResult<int> GetUserCartProductsCounter()
        {
            var UID = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (UID is null) { return BadRequest("not logged in"); }
           var counter= _userProductCartManager.GetUserCartProductsCounter(UID);
            return Ok(counter);
        }
        #endregion
    }
}