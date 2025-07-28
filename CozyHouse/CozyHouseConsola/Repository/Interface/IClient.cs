using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyHouseConsola.Repository.Interface
{
    public interface IApartmentRepository
    {
        List<Apartment> GetAll();
        Apartment GetById(int id);
        void Save(Apartment apartment);
        void Delete(int id);
    }
}