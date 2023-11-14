namespace SneakersCollection.Api.Models
{
    public class SwaggerOptions
    {
        public string ApiTitle { get; set; }
        public string ApiVersion { get; set; }
        public string AuthorizationUrl { get; set; }
        public string TokenUrl { get; set; }
        public string Audience { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string RedirectUrl { get; set; }
        public string OpenIdConnectUrl { get; set; }
        public string Scope1 { get; set; }
        public string Scope2 { get; set; }
    }
}
