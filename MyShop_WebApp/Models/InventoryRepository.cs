using Microsoft.EntityFrameworkCore;
using MyShop_WebApp.DBContext;

namespace MyShop_WebApp.Models
{
    public class InventoryRepository
     
    {
        private readonly ShopBridgeDbContext _context;

        public InventoryRepository(ShopBridgeDbContext context)
        {
            _context = context;
        }

        public async Task<Inventory> AddInventory(Inventory Inventory)
        {
            try
            {
                Inventory.CreatedAt = DateTime.Now;
                _context.Set<Inventory>().Add(Inventory);
                await _context.SaveChangesAsync();
                return Inventory;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to add Inventory to database", ex);
            }
        }

        public async Task<Inventory> GetInventory(int id)
        {
            //return await _context.Inventorys.FindAsync(id);
            var existingInventory = await _context.Set<Inventory>().FindAsync(id);
            return existingInventory;


        }

        public async Task<Inventory> UpdateInventory(int id, Inventory Inventory)
        {
            try
            {
                var existingInventory = await _context.Set<Inventory>().FindAsync(id);

                if (existingInventory == null)
                {
                    throw new Exception($"Inventory with ID {id} not found");
                }

                existingInventory.Name = Inventory.Name;
                existingInventory.Description = Inventory.Description;
                existingInventory.Price = Inventory.Price;
                existingInventory.UpdatedAt = DateTime.Now;

                await _context.SaveChangesAsync();

                return existingInventory;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to update Inventory with ID {id}", ex);
            }
        }

        public async Task DeleteInventory(int id)
        {
            try
            {
                var existingInventory = await _context.Set<Inventory>().FindAsync(id);

                if (existingInventory == null)
                {
                    throw new Exception($"Inventory with ID {id} not found");
                }

                _context.Set<Inventory>().Remove(existingInventory);

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to delete Inventory with ID {id}", ex);
            }
        }

        public async Task<List<Inventory>> GetInventorys()
        {
            try

            {
                return await _context.Set<Inventory>().ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve Inventorys from database", ex);
            }
        }

    }
}
