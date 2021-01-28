﻿using Microsoft.AspNetCore.Authentication;

namespace ReCode.Cocoon.Proxy.Authentication
{
    public class CocoonAuthenticationOptions : AuthenticationSchemeOptions
    {
        public string BackendApiUrl { get; set; }
        public string LoginUrl { get; set; }
    }
}