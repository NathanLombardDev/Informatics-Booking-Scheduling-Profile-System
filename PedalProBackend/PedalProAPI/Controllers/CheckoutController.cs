using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Google.Api;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PedalProAPI.Context;
using PedalProAPI.Models;
using PedalProAPI.Other_Models;
using PedalProAPI.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
namespace PedalProAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CheckoutController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IRepository _repository;

        
        private readonly UserManager<PedalProUser> _userManager;
        private readonly IUserClaimsPrincipalFactory<PedalProUser> _claimsPrincipalFactory;
       
        private readonly RoleManager<IdentityRole> _roleManager;
 
        private readonly PedalProDbContext _context;

        

        public CheckoutController(IHttpClientFactory httpClientFactory, IConfiguration configuration, IRepository repository, UserManager<PedalProUser> userManager, IUserClaimsPrincipalFactory<PedalProUser> claimsPrincipalFactory, RoleManager<IdentityRole> roleManager, PedalProDbContext context)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _repository = repository;
            _userManager = userManager;
            _claimsPrincipalFactory = claimsPrincipalFactory;

            _roleManager = roleManager;

            _context = context;
        }
        
        [HttpPost("initiate-payment")]
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> InitiatePaymentAsync(int cartId)
        {
            try
            {
                var payFastMerchantId = _configuration["PayFast:MerchantId"];
                var payFastMerchantKey = _configuration["PayFast:MerchantKey"];

                // Retrieve the cart items (packages) from your database based on the user or session
                var cartWithPackages = await _repository.GetCartWithPackages(cartId);

                var paymentItems = new List<PaymentItem>();

                var packagepackage = new PackagePrice();

                foreach (var cartItem in cartWithPackages.Packages)
                {
                    // Fetch the package details using the package ID
                    var package = await _repository.GetPackageAssocAsync(cartItem.PackageId);

                    packagepackage = package;

                    var price = await _repository.GetPriceAsync((int)package.PriceId);

                    // Add the package price to the payment items
                    paymentItems.Add(new PaymentItem
                    {
                        ItemName = cartItem.PackageName,
                        Amount = (decimal)price.Price1
                    }) ;
                }

                var totalAmount = paymentItems.Sum(item => item.Amount);
                var payFastMerchantIds = "10030572";
                var payFastMerchantKeys = "ojrpnoz04gz2a";
                var totalAmounts = 100.00M;
                var itemParameterss = "item_name=Product1&item_amount=50.00&item_name=Product2&item_amount=50.00";

                using (var client = _httpClientFactory.CreateClient())
                {
                    var itemParameters = new StringBuilder();

                    var returnUrl = "http://localhost:4200/SuccessfulPayment";

                    foreach (var item in paymentItems)
                    {
                        itemParameters.Append($"&item_name={Uri.EscapeDataString(item.ItemName)}&item_amount={item.Amount:F2}");
                    }
                    var paymentUrl = $"https://sandbox.payfast.co.za/eng/process" +
                                     $"/?merchant_id={payFastMerchantId}" +
                                     $"&merchant_key={payFastMerchantKey}" +
                                     $"&amount={totalAmount}" + 
                                     $"{itemParameters}"+ $"&return_url={Uri.EscapeDataString(returnUrl)}"+ $"&cancel_url=http://localhost:4200/UnSuccessfulPayment";


                    var paymentUrltwo = $"https://sandbox.payfast.co.za/eng/process" +
                    $"/?merchant_id=10030572" +
                    $"&merchant_key=ojrpnoz04gz2a" +
                    $"&amount={totalAmount}" +
                    $"&item_name=Product1&item_amount=50.00" +
                    $"&item_name=Product2&item_amount=50.00";

                    
                    return Ok(new { PaymentUrl = paymentUrl });
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Error initiating payment: {ex.Message}");
            }
        }

        [HttpPost("SavePayment")]
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> SavePayment(int cartId)
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

            var client = await _repository.GetClient(userId);

            if (client == null)
            {
                return BadRequest("Client not found.");
            }

            var userClaims = User.Claims;
            bool hasClientRole = userClaims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Client");

            if (!hasClientRole)
            {
                return Forbid("You do not have the necessary role to perform this action.");
            }


            var cartWithPackages = await _repository.GetCartWithPackages(cartId);

            var packagepackage = new PackagePrice();

            var paymentItems = new List<PaymentItem>();
            var packagerevenue = new PackageRevenue();

            foreach (var cartItem in cartWithPackages.Packages)
            {
                var package = await _repository.GetPackageAssocAsync(cartItem.PackageId);

                packagepackage = package;

                client.NumBookingsAllowance += cartItem.NumPackageBookings;
                await _repository.SaveChangesAsync();

                var revenue = await _repository.GetPackageRevenue(cartItem.PackageName);

                if (revenue==null)
                {
                    packagerevenue.PackageName = cartItem.PackageName;
                    packagerevenue.Quantity = 1;
                    _repository.Add(packagerevenue);
                }
                else
                {
                    revenue.Quantity+=1;
                }

                await _repository.SaveChangesAsync();

                var price = await _repository.GetPriceAsync((int)package.PriceId);

                paymentItems.Add(new PaymentItem
                {
                    ItemName = cartItem.PackageName,
                    Amount = (decimal)price.Price1
                });

                var clientpackage = new ClientPackage()
                {
                    ClientId = client.ClientId,
                    PackageId = package.PackageId,

                };
                _repository.Add(clientpackage);
                await _repository.SaveChangesAsync();
            }

            var totalAmount = paymentItems.Sum(item => item.Amount);


            var payment = new Payment
            {
                PaymentAmount = (double?)totalAmount,
                PaymentDate = DateTime.Now,
            };

            try
            {
                _repository.Add(payment);
                await _repository.SaveChangesAsync();

                var checkout = new Checkout
                {
                    PaymentId = payment.PaymentId,
                    CartId = cartWithPackages.CartId,

                };
                _repository.Add(checkout);
                await _repository.SaveChangesAsync();


                return Ok(checkout);
            }
            catch (Exception ex) 
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }

        }
    }
}


