using SQLite;

namespace People.Models
{
    [Table("people")]
    public class Person
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Cpf { get; set; }
        public string Code { get; set; }
        public string Validade { get; set; }

    }
}
