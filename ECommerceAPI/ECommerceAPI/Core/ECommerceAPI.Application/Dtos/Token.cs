﻿namespace ECommerceAPI.Application.Dtos
{
    public class Token
    {
        public string AccessToken { get; set; }
        public DateTime Expiration { get; set; }

        public string RefreshToken { get; set; }

    }
}
