using Microsoft.IdentityModel.Tokens;
using PetShop.Manager.Token.Interface;
using PetShop.Models.UtilsProject;
using System.Security.Claims;


namespace PetShop.Manager.Token
{
    public class JWTContainerModel : IAuthContainerModel
    {
        public string SecretKey { get; set; } = Utils.KeyToken;
        public string SecurityAlgorithm { get; set; } = SecurityAlgorithms.HmacSha256Signature;
        public int ExpireMinutes { get; set; } = 525600; // 1 năm
        public Claim[] Claims { get; set; }
    }
}
