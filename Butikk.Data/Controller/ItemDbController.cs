using Butikk.Data.Model;
using Microsoft.Extensions.Configuration;
using MySqlConnector;

namespace Butikk.Data.Controller;

public class ItemDbController
{
    public readonly string? _connectionstring;
    

    public ItemDbController(IConfiguration config)
    {
        _connectionstring = config.GetConnectionString("FinalConnection")
                            ?? throw new Exception("Feil på stringen");

    }
    
    public async Task<bool> AddItem(ItemTb itemTb)
    {
        await using var con = new MySqlConnection(_connectionstring);
        await con.OpenAsync();

        string query = @$"INSERT INTO item (name, price, category) VALUES (@name, @price, @category)";
        await using var cmd = new MySqlCommand(query, con);
        cmd.Parameters.AddWithValue("@name", itemTb.Name);
        cmd.Parameters.AddWithValue("@price", itemTb.Price);
        cmd.Parameters.AddWithValue("@category", itemTb.Category);

        return await cmd.ExecuteNonQueryAsync() > 0;
    }

    //private void Parameters(Item item, MySqlCommand cmd)
    //{
    //    cmd.Parameters.AddWithValue("@text", item.Name);
    //}
    public async Task<List<ItemTb>> GetAllItems()
    {
        List<ItemTb> items = new List<ItemTb>();
        await using var con = new MySqlConnection(_connectionstring);
        await con.OpenAsync();

        string query = @$"SELECT * FROM item";
        await using var cmd = new MySqlCommand(query, con);

        await using var reader = cmd.ExecuteReader();
        while (await reader.ReadAsync())
        {
            items.Add(new ItemTb
            {
                Id = reader.GetInt32("id"),
                Name = reader.GetString("name"),
                Price = reader.GetDouble("price"),
                Category = reader.GetString("category")
            });
        }

        return items;
    }
}