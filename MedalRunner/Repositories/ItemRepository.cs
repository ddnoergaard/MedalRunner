using MedalRunner.Models;
using MedalRunner.Repositories.Interfaces;
using Microsoft.Data.SqlClient;

namespace MedalRunner.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private string _connectionString;

        public ItemRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task AddItem(Item item)
        {
            string sqlQuery = "INSERT INTO items(name, gear_slot, image_url, item_level, rarity, difficulty, material, armor, min_damage, max_damage, intellect, strength, agility, spirit, stamina, haste, crit, mastery, dodge, parry, hit, expertise, speed, socket_amount, socket_bonus_stat, socket_bonus_amount, enchant)" +
                "VALUES (" +
                "name = @name" +
                "gear_slot = @gear_slot" +
                "image_url = @image_url" +
                "item_level = @item_level" +
                "rarity = @rarity" +
                "difficulty = @difficulty" +
                "material = @material" +
                "armor = @armor" +
                "min_damage = @min_damage" +
                "max_damage = @max_damage" +
                "intellect = @intellect" +
                "strength = @strength" +
                "agility = @agility" +
                "spirit = @spirit" +
                "stamina = @stamina" +
                "haste = @haste" +
                "crit = @crit" +
                "mastery = @mastery" +
                "dodge = @dodge" +
                "parry = @parry" +
                "hit = @hit" +
                "expertise = @expertise" +
                "speed = @speed" +
                "socket_amount = @socket_amount" +
                "socket_bonus_stat = @socket_bonus_stat" +
                "socket_bonus_amount = @socket_bonus_amount" +
                "enchant = @enchant)";

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                await con.OpenAsync();

                using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                {
                    cmd.Parameters.AddWithValue("@name", item.Name);
                    cmd.Parameters.AddWithValue("@gear_slot", item.Slot);
                    cmd.Parameters.AddWithValue("@image_url", item.ImageUrl);
                    cmd.Parameters.AddWithValue("@item_level", item.ItemLevel);
                    cmd.Parameters.AddWithValue("@rarity", item.Rarity);
                    cmd.Parameters.AddWithValue("@difficulty", item.Difficulty);
                    cmd.Parameters.AddWithValue("@material", item.Material);
                    cmd.Parameters.AddWithValue("@armor", item.Armor);
                    cmd.Parameters.AddWithValue("@min_damage", item.MinDamage);
                    cmd.Parameters.AddWithValue("@max_damage", item.MaxDamage);
                    cmd.Parameters.AddWithValue("@intellect", item.Intellect ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@strength", item.Strenght ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@agility", item.Agility ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@spirit", item.Spirit ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@stamina", item.Stamina ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@haste", item.Haste ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@crit", item.Crit ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@mastery", item.Mastery ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@dodge", item.Dodge ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@parry", item.Parry ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@hit", item.Hit ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@expertise", item.Expertise ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@speed", item.Speed ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@socket_amount", item.SocketAmount ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@socket_bonus_stat", item.SocketBonusStat ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@socket_bonus_amount", item.SocketBonusAmount ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@enchant", item.Enchants ?? (object)DBNull.Value);
                    try
                    {
                        await cmd.ExecuteNonQueryAsync();
                    }
                    catch (SqlException ex)
                    {
                        // Handle exception (e.g., log the error)
                        Console.WriteLine($"SQL Error: {ex.Message}");
                    }

                }


            }
        }

        public async Task UpdateItem(Item item)
        {
            string sqlQuery = "UPDATE items" +
                "SET" +

                "name = @name" +
                "gear_slot = @gear_slot" +
                "image_url = @image_url" +
                "item_level = @item_level" +
                "rarity = @rarity" +
                "difficulty = @difficulty" +
                "material = @material" +
                "armor = @armor" +
                "min_damage = @min_damage" +
                "max_damage = @max_damage" +
                "intellect = @intellect" +
                "strength = @strength" +
                "agility = @agility" +
                "spirit = @spirit" +
                "stamina = @stamina" +
                "haste = @haste" +
                "crit = @crit" +
                "mastery = @mastery" +
                "dodge = @dodge" +
                "parry = @parry" +
                "hit = @hit" +
                "expertise = @expertise" +
                "speed = @speed" +
                "socket_amount = @socket_amount" +
                "socket_bonus_stat = @socket_bonus_stat" +
                "socket_bonus_amount = @socket_bonus_amount" +
                "enchant = @enchant" +

                "WHERE id = @id";

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                await con.OpenAsync();

                using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                {
                    cmd.Parameters.AddWithValue("@id", item.Id);
                    cmd.Parameters.AddWithValue("@name", item.Name);
                    cmd.Parameters.AddWithValue("@gear_slot", item.Slot);
                    cmd.Parameters.AddWithValue("@image_url", item.ImageUrl);
                    cmd.Parameters.AddWithValue("@item_level", item.ItemLevel);
                    cmd.Parameters.AddWithValue("@rarity", item.Rarity);
                    cmd.Parameters.AddWithValue("@difficulty", item.Difficulty);
                    cmd.Parameters.AddWithValue("@material", item.Material);
                    cmd.Parameters.AddWithValue("@armor", item.Armor);
                    cmd.Parameters.AddWithValue("@min_damage", item.MinDamage);
                    cmd.Parameters.AddWithValue("@max_damage", item.MaxDamage);
                    cmd.Parameters.AddWithValue("@intellect", item.Intellect ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@strength", item.Strenght ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@agility", item.Agility ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@spirit", item.Spirit ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@stamina", item.Stamina ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@haste", item.Haste ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@crit", item.Crit ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@mastery", item.Mastery ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@dodge", item.Dodge ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@parry", item.Parry ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@hit", item.Hit ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@expertise", item.Expertise ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@speed", item.Speed ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@socket_amount", item.SocketAmount ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@socket_bonus_stat", item.SocketBonusStat ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@socket_bonus_amount", item.SocketBonusAmount ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@enchant", item.Enchants ?? (object)DBNull.Value);
                    try
                    {
                        await cmd.ExecuteNonQueryAsync();
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine($"SQL Error: {ex.Message}");
                    }
                }
            }
        }

        public async Task DeleteItem(int id)
        {
            string sqlQuery = "DELETE FROM items WHERE id = @id";
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                await con.OpenAsync();
                using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    try
                    {
                        await cmd.ExecuteNonQueryAsync();
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine($"SQL Error: {ex.Message}");
                    }
                }
            }
        }
}
