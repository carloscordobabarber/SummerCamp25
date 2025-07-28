using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CozyHouseConsola.Repository.Interface;
using Dominio;
// Archivo: Repositories/FileApartmentRepository.cs
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
namespace CozyHouseConsola.Repository { 


    //    private readonly string filePath = "apartments.json";

    //    public List<Apartment> GetAll()
    //    {
    //        if (!File.Exists(filePath))
    //            return new List<Apartment>();

    //        string json = File.ReadAllText(filePath);
    //        return JsonSerializer.Deserialize<List<Apartment>>(json) ?? new List<Apartment>();
    //    }

    //    public Apartment GetById(int id)
    //    {
    //        return GetAll().Find(a => a.IdProperty == id);
    //    }

    //    public void Save(Apartment apartment)
    //    {
    //        var apartments = GetAll();

    //        var existing = apartments.FindIndex(a => a.IdProperty == apartment.IdProperty);
    //        if (existing >= 0)
    //            apartments[existing] = apartment;
    //        else
    //            apartments.Add(apartment);

    //        string json = JsonSerializer.Serialize(apartments, new JsonSerializerOptions { WriteIndented = true });
    //        File.WriteAllText(filePath, json);
    //    }

    //    public void Delete(int id)
    //    {
    //        var apartments = GetAll();
    //        apartments.RemoveAll(a => a.IdProperty == id);

    //        string json = JsonSerializer.Serialize(apartments, new JsonSerializerOptions { WriteIndented = true });
    //        File.WriteAllText(filePath, json);
    //    }
    //}
}
