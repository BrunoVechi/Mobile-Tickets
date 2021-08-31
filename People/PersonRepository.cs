using People.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace People
{
    public class PersonRepository
    {
        SQLiteAsyncConnection conn;
        public string Nome_ { get; set; }
        public string Cpf_ { get; set; }
        public string Codigo_ { get; set; }        
        public string StatusMessage { get; set; }
        public bool exe { get; set; }

        public PersonRepository(string dbPath)
        {
            conn = new SQLiteAsyncConnection(dbPath);
            conn.CreateTableAsync<Person>().Wait();
        }

        public async Task AddNewPersonAsync(string name, string cpf, string validade)
        {
            Nome_ = "";
            Cpf_ = "";
            Codigo_ = "";
            exe = false;

            // CARREGA LISTA DE TODOS CLIENTES          
            var collection = await conn.Table<Person>().ToListAsync();            
            
            //Metodo gerar codigo 
            string Random()
            {                
                string code;
           
            INICIO:
                //Cria semente para numero/letra aleatoria
                Random random = new Random(Guid.NewGuid().GetHashCode()); 
                //string para receber codigo
                StringBuilder builder = new StringBuilder();

                char ch;
                for (int i = 0; i < 6; i++)
                {
                    ch = (char)random.Next('A', 'Z' + 1);
                    builder.Append(ch);
                    ch = (char)random.Next('0', '9' + 1);
                    builder.Append(ch);
                }
                code = Convert.ToString(builder);

                //verifica se code ja existe
                if (collection.Any(x => x.Code == code)) { goto INICIO; }
                else 
                {                    
                    Codigo_ = code;
                    return code;
                }
            }

            int result = 0;
            try
            {
                //basic validation to ensure a name was entered
                if (string.IsNullOrEmpty(name))
                    throw new Exception("Valid name required");
                if (string.IsNullOrEmpty(cpf))
                    throw new Exception("Valid cpf required");
                if (string.IsNullOrEmpty(validade))
                    throw new Exception("Valid required");

                    result = await conn.InsertAsync(new Person { Name = name, Cpf = cpf, Code = Random(), Validade = validade });
                    Nome_ = name;
                    Cpf_ = cpf;

                    StatusMessage = string.Format("Added Name: {0}", name);
               

            }
            catch (Exception ex)
            {
                exe = true;
                StatusMessage = string.Format("Failed to add {0}. Error: {1}", name, ex.Message);                 

            }
            
        }

        public async Task<List<Person>> GetAllPeopleAsync()
        {
            try
            {
                return await conn.Table<Person>().ToListAsync();              

            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
            }

            return new List<Person>();
        }

        public async Task DeletePersonAsync(string name)
        {
            try
            {
                if (!string.IsNullOrEmpty(name))
                {
                    var collection = await conn.Table<Person>().Where(x => x.Name == name).ToListAsync();
                    foreach (Person x in collection)
                    {
                        await conn.DeleteAsync(x);
                    }

                    StatusMessage = string.Format(" deleted {0}", name);

                }
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to del {0}. Error: {1}", name, ex.Message);
            }


        }
        
        public async Task UpdatePersonAsync(string name, string cpf, string validade)
        {
            try
            {
                if (!string.IsNullOrEmpty(name))
                {
                    var collection = await conn.Table<Person>().Where(x => x.Name == name).ToListAsync();
                    foreach (Person x in collection)
                    {
                        x.Cpf = cpf;
                        x.Validade = validade;

                        await conn.UpdateAsync(x);
                    }

                    StatusMessage = string.Format(" updated {0}", name);

                }
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to update {0}. Error: {1}", name, ex.Message);
            }


        }

        public async Task<string> ReadQRcode(string code)
        {
            
            try
            {
                if (!string.IsNullOrEmpty(code))
                {
                    var collection = await conn.Table<Person>().Where(x => x.Code == code).ToListAsync();
                    if (collection.Count == 0) { return "NÃO ENCONTRADO"; }
                    else
                    {
                        foreach (Person x in collection)
                        {
                            if (x.Validade == "1") { x.Validade = Convert.ToString("0"); await conn.UpdateAsync(x); return "OK!"; }
                            if (x.Validade == "0") { x.Validade = Convert.ToString("0"); await conn.UpdateAsync(x); return "INVALIDO!"; }
                        }
                    }                   

                } else { return null; }
            }
            catch (Exception)
            {
               return "ERROR";
            }

            return  null;
        }


    }
}