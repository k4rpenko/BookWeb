using BDLearn.Hash;
using BDLearn.Sending;
using Microsoft.AspNetCore.Mvc;

namespace BDLearn.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GoogleAuth : Controller
    {
        GoogleOAuth GoogleOAuth = new GoogleOAuth();

        [HttpPost("GoogleAuth")]
        public async Task<IActionResult> RedirectOauthServer()
        {
            var scope = "https://mail.google.com/";
            var redirectUrl = $"{Request.Scheme}://{Request.Host}/api/GoogleAuthGetCode";
            var CodeVerifier = Guid.NewGuid().ToString();
            var key = new HASH().GenerateKey();
            var codeChellange = new HASH().Encrypt(CodeVerifier, key.ToString());


            var url = GoogleOAuth.GenerateOauthUrl(scope, redirectUrl, codeChellange);
            return Redirect(url);
        }

        [HttpPost("GoogleAuthGetCode")]
        public async Task<IActionResult> codeOauthServer(string code)
        {
            string CodeVerifier = null;
            var redirectUrl = $"{Request.Scheme}://{Request.Host}/api/GoogleAuthGetCode";

            var token = await GoogleOAuth.ExechangeCodeOauthT(code, CodeVerifier, redirectUrl);
            return Ok();
        }
    }
}