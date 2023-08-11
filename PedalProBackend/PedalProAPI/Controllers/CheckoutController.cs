using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PedalProAPI.Other_Models;
using PedalProAPI.Repositories;

namespace PedalProAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckoutController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IRepository _repository;

        public CheckoutController(IHttpClientFactory httpClientFactory, IConfiguration configuration, IRepository repository)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _repository = repository;
        }
        /*
        [HttpPost("initiate-payment")]
        public async Task<IActionResult> InitiatePaymentAsync([FromBody] PaymentRequest paymentRequest)
        {
            try
            {
                var payFastMerchantId = _configuration["PayFast:MerchantId"];
                var payFastMerchantKey = _configuration["PayFast:MerchantKey"];

                decimal totalAmount = paymentRequest.Items.Sum(item => item.Amount);

                using (var client = _httpClientFactory.CreateClient())
                {
                    var itemParameters = string.Join("&", paymentRequest.Items
                        .Select(item => $"item_name={item.ItemName}&item_amount={item.Amount}"));

                    // Construct the payment URL with multiple items
                    var paymentUrl = $"https://sandbox.payfast.co.za/eng/process?merchant_id={payFastMerchantId}&merchant_key={payFastMerchantKey}&amount={totalAmount}&{itemParameters}";

                    // Redirect to the PayFast payment page
                    return Redirect(paymentUrl);
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Error initiating payment: {ex.Message}");
            }
        }
        */

        [HttpPost("initiate-payment")]
        public async Task<IActionResult> InitiatePaymentAsync(int cartId)
        {
            try
            {
                var payFastMerchantId = _configuration["PayFast:MerchantId"];
                var payFastMerchantKey = _configuration["PayFast:MerchantKey"];

                // Retrieve the cart items (packages) from your database based on the user or session
                var cartWithPackages = await _repository.GetCartWithPackages(cartId);

                var paymentItems = new List<PaymentItem>();

                foreach (var cartItem in cartWithPackages.Packages)
                {
                    // Fetch the package details using the package ID
                    var package = await _repository.GetPackageAssocAsync(cartItem.PackageId);

                    var price = await _repository.GetPriceAsync((int)package.PriceId);

                    // Add the package price to the payment items
                    paymentItems.Add(new PaymentItem
                    {
                        ItemName=cartItem.PackageName,
                        Amount= (decimal)price.Price1
                    });
                }

                var totalAmount = paymentItems.Sum(item => item.Amount);
                var payFastMerchantIds = "10030572";
                var payFastMerchantKeys = "ojrpnoz04gz2a";
                var totalAmounts = 100.00M; // Example total amount
                var itemParameterss = "item_name=Product1&item_amount=50.00&item_name=Product2&item_amount=50.00";

                using (var client = _httpClientFactory.CreateClient())
                {
                    var itemParameters = string.Join("&", paymentItems
                        .Select(item => $"item_name={item.ItemName}&item_amount={item.Amount}"));

                    
                    // Construct the payment URL with multiple items
                    var paymentUrltwo = $"https://sandbox.payfast.co.za/eng/process/" +
                    $"?merchant_id={payFastMerchantId}" +
                    $"&merchant_key={payFastMerchantKey}" +
                    $"&amount={totalAmount}" +
                    $"{itemParameters}";
                    


                    /*
                    var paymentUrl = $"https://sandbox.payfast.co.za/eng/process" +
                    $"/?merchant_id=10030572" +
                    $"&merchant_key=ojrpnoz04gz2a" +
                    $"&amount=100.00" +
                    $"&item_name=Product1&item_amount=50.00" +
                    $"&item_name=Product2&item_amount=50.00";*/
                    // Redirect to the PayFast payment page
                    return Ok(new { PaymentUrl = paymentUrltwo });
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Error initiating payment: {ex.Message}");
            }
        }
    }
}


