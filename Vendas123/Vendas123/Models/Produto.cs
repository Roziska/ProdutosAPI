using System.Xml.Serialization;

namespace Vendas123.Models
{
    [XmlType("Produto")]
    public class Produto
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Descricao { get; set; }
        public decimal Preco { get; set; }
        public int Quantidade { get; set; }
       
    }
}
