using System;
using System.Collections.Generic;
using System.Linq;

public class ClientService : IClientService
{
    private List<Client> _clients = new List<Client>();
    private int id = 1;

    public IEnumerable<Client> GetClients()
    {
        return _clients;
    }

    public Client GetClient(int id)
    {
        return _clients.FirstOrDefault(x => x.Id == id);
    }

    public Client AddClient(string name, string address, string city, string zipCode, string province, string country,
                            string contactName, string contactPhone, string contactEmail)
    {
        var client = new Client
        {
            Id = id++,
            Name = name,
            Address = address,
            City = city,
            ZipCode = zipCode,
            Province = province,
            Country = country,
            ContactName = contactName,
            ContactPhone = contactPhone,
            ContactEmail = contactEmail,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _clients.Add(client);
        return client;
    }

    public Client UpdateClient(int id, string name, string address, string city, string zipCode, string province, string country,
                               string contactName, string contactPhone, string contactEmail)
    {
        var client = _clients.FirstOrDefault(c => c.Id == id);
        if (client == null)
        {
            throw new KeyNotFoundException($"Client with ID {id} not found.");
        }

        client.Name = name;
        client.Address = address;
        client.City = city;
        client.ZipCode = zipCode;
        client.Province = province;
        client.Country = country;
        client.ContactName = contactName;
        client.ContactPhone = contactPhone;
        client.ContactEmail = contactEmail;
        client.UpdatedAt = DateTime.UtcNow;

        return client;
    }

    public bool DeleteClient(int id)
    {
        var client = _clients.FirstOrDefault(c => c.Id == id);
        if (client == null)
        {
            return false;
        }

        _clients.Remove(client);
        return true;
    }
}
