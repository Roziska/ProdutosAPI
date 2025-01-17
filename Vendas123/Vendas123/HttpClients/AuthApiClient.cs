﻿using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Vendas123.Seguranca;

namespace Vendas123.HttpClients
{
    public class LoginResult
    {
        public string Token { get; set; }
        public bool Succeeded { get; set; }

        public LoginResult(string token, HttpStatusCode statusCode)
        {
            Token = token;
            Succeeded = (statusCode == HttpStatusCode.OK);
        }
    }

    public class AuthApiClient
    {
        private readonly HttpClient _httpClient;

        public AuthApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<LoginResult> PostLoginAsync(LoginModel model)
        {
            var resposta = await _httpClient.PostAsJsonAsync<LoginModel>("login",model);
            return new LoginResult(await resposta.Content.ReadAsStringAsync(), resposta.StatusCode);
        }

        public async Task PostRegisterAsync(RegisterViewModel model)
        {
            var resposta = await _httpClient.PostAsJsonAsync<RegisterViewModel>("usuarios", model);
            resposta.EnsureSuccessStatusCode();
        }
    }
}
