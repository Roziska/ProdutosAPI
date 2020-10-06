using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using System.Linq;
using Vendas123.Models;

namespace Vendas123.HttpClients
{
    public class ProdutoApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _acessor;

        public ProdutoApiClient(HttpClient client, IHttpContextAccessor accessor)
        {
            _httpClient = client;
            _acessor = accessor;
        }

        private void AddBearerToken()
        {
            var token = _acessor.HttpContext.User.Claims.First(c => c.Type == "Token").Value;
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        public async Task<Produto> GetProdutoAsync(int id)
        {
            AddBearerToken();
            var resposta = await _httpClient.GetAsync($"produtos/{id}");
            resposta.EnsureSuccessStatusCode();
            return await resposta.Content.ReadAsAsync<Produto>();
        }

        public async Task DeleteProdutoAsync(int id)
        {
            AddBearerToken();
            var resposta = await _httpClient.DeleteAsync($"produtos/{id}");
            resposta.EnsureSuccessStatusCode();
            if (resposta.StatusCode != System.Net.HttpStatusCode.NoContent)
            {
                throw new InvalidOperationException("Código de Status Http 204 esperado!");
            }
        }

        public async Task PostProdutoAsync(Produto produto)
        {
            AddBearerToken();
            HttpContent content = CreateMultipartContent(produto);
            var resposta = await _httpClient.PostAsync("produtos", content);
            resposta.EnsureSuccessStatusCode();
            if (resposta.StatusCode != System.Net.HttpStatusCode.Created)
            {
                throw new InvalidOperationException("Código de Status Http 201 esperado!");
            }
        }

        public async Task PutProdutoAsync(Produto produto)
        {
            AddBearerToken();
            HttpContent content = CreateMultipartContent(produto);
            var resposta = await _httpClient.PutAsync("produtos", content);
            resposta.EnsureSuccessStatusCode();
            if (resposta.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new InvalidOperationException("Código de Status Http 200 esperado!");
            }

        }

        private string EnvolveComAspasDuplas(string valor)
        {
            return $"\u0022{valor}\u0022";
        }

        private HttpContent CreateMultipartContent(Produto produto)
        {
            var content = new MultipartFormDataContent();

            content.Add(new StringContent(produto.Codigo), EnvolveComAspasDuplas("codigo"));
            content.Add(new StringContent(produto.Descricao), EnvolveComAspasDuplas("descricao"));


            if (produto.Id > 0)
            {
                content.Add(new StringContent(Convert.ToString(produto.Id)), EnvolveComAspasDuplas("id"));
            }
            return content;
        }
    }
}
