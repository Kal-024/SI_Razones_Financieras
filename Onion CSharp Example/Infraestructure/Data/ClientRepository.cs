using Core.Interfaces;
using Core.Poco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Data
{
    public class ClientRepository : IClientRepository
    {
        private RAFContext context;
        private readonly int SIZE = 228;
        List<Cliente> clients;

        public ClientRepository()
        {
            context = new RAFContext("Client", SIZE);
        }

        #region Metodos
        public void Create(Cliente t)
        {
            context.Create<Cliente>(t);
        }

        public int Update(Cliente t)
        {
            return context.Update<Cliente>(t);
        }

        public bool Delete(Cliente t)
        {
            FindId(getId(), t.Id);
            if (context.Get<Cliente>(t.Id) == null)
            {
                throw new ArgumentException($"Product with Id {t.Id} does not exists.");
            }
            return context.Delete(t);
        }

        public IEnumerable<Cliente> GetAll()
        {
            return context.GetAll<Cliente>();
        }

        public IEnumerable<Cliente> Find(Expression<Func<Cliente, bool>> where)
        {
            throw new NotImplementedException();
        }
        #endregion

        private int[] getId()
        {
            clients = context.GetAll<Cliente>();
            int indexx = clients.Count();
            int[] clientsId = new int[indexx];

            for (int i = 0; i < indexx; i++)
            {
                clientsId[i] = clients.ElementAt(i).Id;
            }
            return clientsId;
        }

        private void FindId(Array arreglo, Object objeto)
        {
            if (arreglo == null)
            {
                return;
            }
            int index = Array.BinarySearch(arreglo, objeto);
            if (index < 0)
            {
                Console.WriteLine($"The id ({objeto}) is not found ma bro :(");
            }
            else
            {
                Console.WriteLine($"The id is ({objeto}) Found at the index ({index}).");
            }
        }
    }
}
