using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PedalProAPI.Context;
using PedalProAPI.Models;
using PedalProAPI.Repositories;
using PedalProAPI.ViewModels;

namespace PedalProAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        /*
        [HttpPost]
        [Route("AddPackageToCart")]
        public async Task<IActionResult> AddPackageToCart(CartViewModel packageRequest)
        {
            var cart=_context.Carts.Include(c => c.Packages).FirstOrDefault(c => c.CartId == packageRequest.cartId);
            
            if (cart == null)
            {
                // Create a new cart if it doesn't exist
                cart = new Cart();
                _context.Carts.Add(cart);
            }
            
            var package = _context.Packages.FirstOrDefault(p => p.PackageId == packageRequest.packageId);

            if (package == null)
            {
                return NotFound("Package not found");
            }

            // Add the package to the cart
            cart.Packages.Add(package);

            var packageprice= await _repository.GetPackageAssocAsync(packageRequest.packageId);

            var price =await _repository.GetPriceAsync((int)packageprice.PriceId);


            // Update the cart amount based on the added package price
            if (cart.CartAmount == null)
            {
                cart.CartAmount = price.Price1;
            }
            else
            {
                cart.CartAmount += price.Price1;
            }

            if (cart.CartQuantity==null)
            {
                cart.CartQuantity = 1;
            }
            else
            {
                cart.CartQuantity += 1;
            }

            _context.SaveChanges();


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
        */

        [HttpPost]
        [Route("AddPackageToCart")]
        public async Task<IActionResult> AddPackageToCart(CartViewModel packageRequest)
        {
            var cart = _context.Carts.Include(c => c.Packages).FirstOrDefault(c => c.CartId == packageRequest.cartId);

            if (cart == null)
            {
                // Create a new cart if it doesn't exist
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

            if (!cart.Packages.Contains(package)) // Check if package doesn't exist in cart
            {
                // Add the package to the cart for the first time
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

        /*
        [HttpPost("api/carts/{cartId}/packages")]
        public IActionResult AddPackageToCart(int cartId, [FromBody] PackageRequest packageRequest)
        {
            // Fetch the cart by cartId
            var cart = _context.Carts.Include(c => c.Packages).FirstOrDefault(c => c.CartId == cartId);

            if (cart == null)
            {
                return NotFound();
            }

            // Fetch the package by packageId
            var package = _context.Packages.FirstOrDefault(p => p.PackageId == packageRequest.PackageId);

            if (package == null)
            {
                return NotFound();
            }

            // Add the package to the cart
            cart.Packages.Add(package);

            // Update the cart amount based on the added package price
            if (cart.CartAmount == null)
            {
                cart.CartAmount = package.Price;
            }
            else
            {
                cart.CartAmount += package.Price;
            }

            _context.SaveChanges();

            return Ok();
        }*/
    }
}
