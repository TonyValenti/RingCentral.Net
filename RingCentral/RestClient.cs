﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace RingCentral
{
    public partial class RestClient : IDisposable
    {
        public const string SandboxServer = "https://platform.devtest.ringcentral.com";
        public const string ProductionServer = "https://platform.ringcentral.com";

        public string clientId;
        public string clientSecret;
        public Uri server;
        public TokenInfo token;

        private RestClient(string clientId, string clientSecret, Uri server)
        {
            this.clientId = clientId;
            this.clientSecret = clientSecret;
            this.server = server;
        }

        public RestClient(string clientId, string clientSecret, string server)
            : this(clientId, clientSecret, new Uri(server))
        {
        }

        public RestClient(string clientId, string clientSecret, bool production = false)
            : this(clientId, clientSecret, production ? ProductionServer : SandboxServer)
        {
        }

        public async Task<HttpResponseMessage> Request(HttpRequestMessage httpRequestMessage,
            bool basicAuth = false)
        {
            var httpClient = new HttpClient();
            httpRequestMessage.Headers.UserAgent.ParseAdd("RingCentral.Net");
            httpRequestMessage.Headers.Authorization = basicAuth
                ? new AuthenticationHeaderValue("Basic",
                    Convert.ToBase64String(
                        Encoding.UTF8.GetBytes($"{clientId}:{clientSecret}")))
                : new AuthenticationHeaderValue("Bearer", token.access_token);
            var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                AfterHttpCall?.Invoke(this, new HttpCallEventArgs(httpResponseMessage, httpRequestMessage));
                return httpResponseMessage;
            }

            throw new RestException(httpResponseMessage, httpRequestMessage);
        }

        public async Task<TokenInfo> Authorize(string username, string extension, string password)
        {
            var httpContent = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                {"grant_type", "password"},
                {"username", username},
                {"extension", extension},
                {"password", password}
            });
            var httpRequestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(server, "/restapi/oauth/token"),
                Content = httpContent
            };
            var httpResponseMessage = await Request(httpRequestMessage, true);
            var json = await httpResponseMessage.Content.ReadAsStringAsync();
            token = JsonConvert.DeserializeObject<TokenInfo>(json);
            return token;
        }

        public async Task<TokenInfo> Refresh()
        {
            if (token == null) // nothing  to refresh
            {
                return null;
            }

            var httpContent = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                {"grant_type", "refresh_token"},
                {"refresh_token", token.refresh_token}
            });
            var httpRequestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(server, "/restapi/oauth/token"),
                Content = httpContent
            };
            var httpResponseMessage = await Request(httpRequestMessage, true);
            var json = await httpResponseMessage.Content.ReadAsStringAsync();
            token = JsonConvert.DeserializeObject<TokenInfo>(json);
            return token;
        }

        public async Task Revoke()
        {
            if (token == null) // nothing  to revoke
            {
                return;
            }

            var httpContent = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                {"token", token.access_token}
            });
            var httpRequestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(server, "/restapi/oauth/revoke"),
                Content = httpContent
            };
            await Request(httpRequestMessage, true);
            token = null;
        }

        public async void Dispose()
        {
            await Revoke();
        }

        public string AuthorizeUri(string redirectUri, string state = "")
        {
            return AuthorizeUri(redirectUri, new OAuthOptions {state = state});
        }

        public string AuthorizeUri(string redirectUri, OAuthOptions options)
        {
            var uriBuilder = new UriBuilder(server) {Path = "/restapi/oauth/authorize"};
            var queryParams = new[]
            {
                ("redirect_uri", redirectUri),
                ("client_id", clientId),
                ("response_type", options.responseType),
                ("state", options.state),
                ("brand_id", options.brandId),
                ("display", options.display),
                ("prompt", options.prompt)
            };
            uriBuilder.Query =
                string.Join("&", queryParams.Select(qp => $"{qp.Item1}={Uri.EscapeUriString(qp.Item2)}"));
            return uriBuilder.Uri.ToString();
        }

        public async Task<TokenInfo> Authorize(string authCode, string redirectUri)
        {
            var httpContent = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                {"grant_type", "authorization_code"},
                {"code", authCode},
                {"redirect_uri", redirectUri}
            });
            var httpRequestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(server, "/restapi/oauth/token"),
                Content = httpContent
            };
            var httpResponseMessage = await Request(httpRequestMessage, true);
            var json = await httpResponseMessage.Content.ReadAsStringAsync();
            token = JsonConvert.DeserializeObject<TokenInfo>(json);
            return token;
        }
    }

    public class OAuthOptions
    {
        public string responseType = "code";
        public string state = "";
        public string brandId = "";
        public string display = "";
        public string prompt = "";
    }
}