using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Negocio.Models;

namespace console.UI
{
    internal class ClientesUI
    {
        public static void Cadastrar()
        {
            var cliente = new Cliente();
            Console.WriteLine("========= [Cadastro de cliente] ========");

            Console.WriteLine("Nome:");
            cliente.Nome = Console.ReadLine();

            Console.WriteLine("Email:");
            cliente.Email = Console.ReadLine();

            cliente.Salvar();

            Console.Clear();
            Console.WriteLine("Cliente cadastrado com sucesso !!!");
            Thread.Sleep(5000);
            Console.Clear();
        }

        internal static void Atualizar()
        {            
            Console.WriteLine("========= [Atualização de clientes] ========");
            Console.WriteLine("Digite o Id ou o Email do cliente");
            var idOuEmail = Console.ReadLine();
            if(string.IsNullOrEmpty(idOuEmail))
            {
                MensagensUI.Mensagem("Digite o Id ou o Email");
                ClientesUI.Atualizar();
                return;
            }

            var clientes = Cliente.BuscaPorIdOuEmail(idOuEmail);
            if(clientes.Count == 0)
            {
                MensagensUI.Mensagem("Cliente não localizado");
                ClientesUI.Atualizar();
                return;
            }
            Console.WriteLine("Digite o Id do cliente abaixo para atualizar");
            foreach (var clienteDb in clientes)
            {
                Console.WriteLine($"Id: {clienteDb.Id}");
                Console.WriteLine($"Nome: {clienteDb.Nome}");
                Console.WriteLine($"Email: {clienteDb.Email}");
                Console.WriteLine("------------------------");
            }

            var cliente = new Cliente();
            cliente.Id = Convert.ToInt32(Console.ReadLine());
            
            Console.WriteLine("Novo Nome:");
            cliente.Nome = Console.ReadLine();

            Console.WriteLine("Novo Email:");
            cliente.Email = Console.ReadLine();

            cliente.Salvar();
            
            MensagensUI.Mensagem("Cliente cadastrado com sucesso !!!");            
        }

        internal static void Listar()
        {
            Console.WriteLine("==== Lista de Clientes ====");
            var clientes = Cliente.Todos();
            foreach (var clienteDb in clientes)
            {
                Console.WriteLine($"Id: {clienteDb.Id}");
                Console.WriteLine($"Nome: {clienteDb.Nome}");
                Console.WriteLine($"Email: {clienteDb.Email}");
                Console.WriteLine("------------------------");
            }
        }
    }
}