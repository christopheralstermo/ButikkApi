using Butikk.Data.Model;
using Butikk.Logic.Controller;
using Microsoft.AspNetCore.Mvc;


namespace ButikkApi.Controller;

[ApiController]
[Route("[controller]")]

public class ItemApi : ControllerBase
{
    private readonly ItemLogicController _controller;

    public ItemApi(IConfiguration config)
    {
        _controller = new ItemLogicController(config);
    }
        
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        List<ItemTb> list = await _controller.GetAllItems();
        return Ok(list);
    }

    [HttpPost]
    public async Task<IActionResult> PostStuff([FromBody] ItemTb itemTb)
    {
        var result = await _controller.AddItem(itemTb);
        return Ok(result);
    }
    
    
}