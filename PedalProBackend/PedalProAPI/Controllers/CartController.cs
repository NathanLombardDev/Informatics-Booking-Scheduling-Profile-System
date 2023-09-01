using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PedalProAPI.Context;
using PedalProAPI.Models;
using PedalProAPI.Repositories;
using PedalProAPI.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace PedalProAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CartController : ControllerBase
    {
        private readonly IRepository _repsository;
        private readonly UserManager<PedalProUser> _userManager;
        private readonly IRepository _repository;
        private readonly IUserClaimsPrincipalFactory<PedalProUser> _claimsPrincipalFactory;
        private readonly IConfiguration _configuration;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<AuthenticationController> _logger;
        private readonly PedalProDbContext _context;

        public CartController(IRepository repository, UserManager<PedalProUser> userManager, ILogger<AuthenticationController> logger, IUserClaimsPrincipalFactory<PedalProUser> claimsPrincipalFactory, RoleManager<IdentityRole> roleManager, IConfiguration configuration, IRepository repsository, PedalProDbContext context)
        {
            _repsository = repository;
            _userManager = userManager;
            _claimsPrincipalFactory = claimsPrincipalFactory;
            _configuration = configuration;
            _repository = repsository;
            _roleManager = roleManager;
            _logger = logger;
            _context = context;
        }

        [HttpPost]
        [Route("AddPackageToCart")]
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> AddPackageToCart(CartViewModel packageRequest)
        {
            var username = User.FindFirst(ClaimTypes.Name)?.Value;

            if (string.IsNullOrEmpty(username))
            {
                return BadRequest("Username not found.");
            }

            var user = await _userManager.FindByNameAsync(username);

            if (user == null)
            {
                return BadRequest("User not found.");
            }

            var userId = user.Id;

            var userClaims = User.Claims;

            //bool hasAdminRole = userClaims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Admin");
            //bool hasEmployeeRole = userClaims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Employee");
            bool hasClientRole = userClaims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Client");

            if (!hasClientRole)
            {
                return Forbid("You do not have the necessary role to perform this action.");
            }

            var client = await _repsository.GetClient(userId);

            if (client == null)
            {
                return BadRequest("Client not found.");
            }

            var cart = _context.Carts.Include(c => c.Packages).FirstOrDefault(c => c.CartId == packageRequest.cartId);

            if (cart == null)
            {
                cart = new Cart();
                _context.Carts.Add(cart);
            }

            var package = _context.Packages.FirstOrDefault(p => p.PackageId == packageRequest.packageId);

            if (package == null)
            {
                return NotFound("Package not found");
            }

            var packageprice = await _repository.GetPackageAssocAsync(packageRequest.packageId);
            var price = await _repository.GetPriceAsync((int)packageprice.PriceId);

            var clientPackageExists = _context.ClientPackages
            .Any(cp => cp.ClientId == client.ClientId && cp.PackageId == packageRequest.packageId); 

            if (clientPackageExists)
            {
                return BadRequest("You have already bought this package.");
            }

            if (!cart.Packages.Contains(package))
            {
                cart.Packages.Add(package);
                if (cart.CartAmount == null)
                {
                    cart.CartAmount = price.Price1;
                }
                else
                {
                    cart.CartAmount += price.Price1;
                }

                if (cart.CartQuantity == null)
                {
                    cart.CartQuantity = 1;
                }
                else
                {
                    cart.CartQuantity++;
                }
            }

            if (cart.CartQuantity < 1)
            {
                cart.CartStatusId = 1;
            }
            else
            {
                cart.CartStatusId = 2;
            }

            _context.SaveChanges();

            return Ok(cart);
        }

        [HttpGet]
        [Route("GetCart/{cartId}")]
        public async Task<IActionResult> GetCart(int cartId)
        {
            try
            {
                var result = await _repsository.GetCartWithPackages(cartId);
                if (result == null) return NotFound("Cart does not exist");
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
        }

        [HttpDelete]
        [Route("RemovePackageFromCart")]
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> RemovePackageFromCart(CartViewModel packageRequest)
        {
            var username = User.FindFirst(ClaimTypes.Name)?.Value;

            if (string.IsNullOrEmpty(username))
            {
                return BadRequest("Username not found.");
            }

            var user = await _userManager.FindByNameAsync(username);

            if (user == null)
            {
                return BadRequest("User not found.");
            }

            var userId = user.Id;

            var userClaims = User.Claims;

            //bool hasAdminRole = userClaims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Admin");
            //bool hasEmployeeRole = userClaims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Employee");
            bool hasClientRole = userClaims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Client");

            if (!hasClientRole)
            {
                return Forbid("You do not have the necessary role to perform this action.");
            }

            var cart = _context.Carts.Include(c => c.Packages).FirstOrDefault(c => c.CartId == packageRequest.cartId);

            if (cart == null)
            {
                return NotFound("Cart not found");
            }

            var package = cart.Packages.FirstOrDefault(p => p.PackageId == packageRequest.packageId);

            if (package == null)
            {
                return NotFound("Package not found in the cart");
            }

            var packageprice = await _repository.GetPackageAssocAsync(packageRequest.packageId);
            var price = await _repository.GetPriceAsync((int)packageprice.PriceId);

            // Remove the package from the cart
            cart.Packages.Remove(package);
            cart.CartAmount -= price.Price1;
            cart.CartQuantity--;

            if (cart.CartQuantity < 1)
            {
                cart.CartStatusId = 1;
            }
            else
            {
                cart.CartStatusId = 2;
            }

            _context.SaveChanges();

            return Ok(cart);
        }
    }
}
