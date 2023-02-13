using Microsoft.AspNetCore.Mvc;
using MyShop_WebApp.Models;

namespace MyShop_WebApp.Controllers
{
    public class InventoryController : Controller

    {
        private readonly InventoryRepository _InventoryRepository;

        public InventoryController(InventoryRepository InventoryRepository)
        {
            _InventoryRepository = InventoryRepository;
        }


        public IActionResult Add()
        {
            var model = new AddInventoryViewModel { Step = 1, Inventory = new Inventory() };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddInventoryViewModel model)
        {
            try
            {
                if (model.Step == 1)
                {
                    // Validate and process data from Step 1
                    ModelState.ClearValidationState("Inventory.Id");
                    ModelState.ClearValidationState("Inventory.Description");

                    if (ModelState.IsValid)
                    {
                        model.Step = 2;
                        return View(model);
                    }
                }
                else 
                {
                   
                    if (ModelState.IsValid)
                    {
                        // Add the Inventory to the repository and redirect to the list of Inventorys
                        await _InventoryRepository.AddInventory(model.Inventory);
                        return RedirectToAction("GetInventorys");
                    }
                }

                return View(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

     
        [Route("Inventory/UpdateInventory")]
        [HttpPut("{id}")]
        public async Task<ActionResult<Inventory>> UpdateInventory(int id, Inventory inventory)
        {
            

            try
            {
                if (!ModelState.IsValid)
                {
                    return View("UpdateInventory", inventory);

                }
                else
                {
                    var result = await _InventoryRepository.UpdateInventory(id, inventory);
                    // return View("UpdateInventory", result);
                    return RedirectToAction("GetInventorys", new { id = result.Id });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Deletes a Inventory with a given ID
        [Route("Inventory/DeleteInventory")]

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteInventory(int id)
        {
            try
            {
                await _InventoryRepository.DeleteInventory(id);
                return RedirectToAction("GetInventorys");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }




        // Retrieves a list of all Inventorys
        [HttpGet]
        public async Task<ActionResult<List<Inventory>>> GetInventorys()

        {
            try
            {
                var result = await _InventoryRepository.GetInventorys();
                return View("GetInventorys", result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        
    }
}
