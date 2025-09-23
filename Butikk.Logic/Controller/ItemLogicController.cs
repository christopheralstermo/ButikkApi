using Microsoft.Extensions.Configuration;
using Butikk.Data.Controller;
using Butikk.Data.Model;

namespace Butikk.Logic.Controller;

public class ItemLogicController
{
    private ItemDbController _Db;

    public ItemLogicController(IConfiguration config)
    {
        _Db = new ItemDbController(config);
    }

    public async Task<bool> AddItem(ItemTb item) => await _Db.AddItem(item);

    public async Task<List<ItemTb>> GetAllItems() => await _Db.GetAllItems();
    
    //public async Task<Item> GetSingle(int id) => await _Db.GetSingleItem(id);
}