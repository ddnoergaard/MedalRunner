using MedalRunner.Models;
using MedalRunner.Repositories.Interfaces;
using Microsoft.Data.SqlClient;
using System.Reflection.PortableExecutable;

namespace MedalRunner.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private string _connectionString;

        public ItemRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<List<Item>> GetAllItemAsync()
        {
            //Create a list to store all the items that we retrieve from the database
            List<Item> data = new List<Item>();
            //Create Instance of item for later use
            Item item = new Item();

            string sqlQuery = "Select * FROM items";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                SqlCommand cmd = new SqlCommand(sqlQuery, connection);
                SqlDataReader reader = await cmd.ExecuteReaderAsync();
                //Check if query is usable if not throw exception

                // while still more to read, add to data list and then return said list
                while (await reader.ReadAsync())
                {
                    data.Add(ItemMapper(reader));
                }
                if (data.Count == 0) throw new IndexOutOfRangeException();
                return data;

            }


        }


        public async Task AddItem(Item item)
        {
            string sqlQuery = "INSERT INTO items(name, gear_slot, image_url, item_level, rarity, difficulty, material, armor, min_damage, max_damage, intellect, strength, agility, " +
                "spirit, stamina, haste, crit, mastery, dodge, parry, hit, expertise, speed, socket_amount, socket_bonus_stat, socket_bonus_amount, enchant)" +
                
                "VALUES (name = @name, gear_slot = @gear_slot, image_url = @image_url, item_level = @item_level, rarity = @rarity, difficulty = @difficulty, material = @material, " +
                "armor = @armor, min_damage = @min_damage, max_damage = @max_damage, intellect = @intellect, strength = @strength, agility = @agility, spirit = @spirit, " +
                "stamina = @stamina, haste = @haste, crit = @crit, mastery = @mastery, dodge = @dodge, parry = @parry, hit = @hit, expertise = @expertise, speed = @speed, " +
                "socket_amount = @socket_amount, socket_bonus_stat = @socket_bonus_stat, socket_bonus_amount = @socket_bonus_amount, enchant = @enchant)";

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
                    cmd.Parameters.AddWithValue("@strength", item.Strength ?? (object)DBNull.Value);
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

        public async Task UpdateItem(Item item)
        {
            string sqlQuery = "UPDATE items" +
                "SET name = @name, gear_slot = @gear_slot, image_url = @image_url, item_level = @item_level, rarity = @rarity, difficulty = @difficulty, material = @material, armor = @armor," +
                 "min_damage = @min_damage, max_damage = @max_damage, intellect = @intellect, strength = @strength, agility = @agility, spirit = @spirit, " +
                "stamina = @stamina, haste = @haste, crit = @crit, mastery = @mastery, dodge = @dodge, parry = @parry, hit = @hit, expertise = @expertise, speed = @speed, " +
                "socket_amount = @socket_amount, socket_bonus_stat = @socket_bonus_stat, socket_bonus_amount = @socket_bonus_amount, enchant = @enchant, " +

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
                    cmd.Parameters.AddWithValue("@strength", item.Strength ?? (object)DBNull.Value);
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

        private int GetNullableInt(SqlDataReader reader, string column)
        {
            int ordinal = reader.GetOrdinal(column);
            return reader.IsDBNull(ordinal) ? 0 : reader.GetInt32(ordinal);
        }

        private string GetNullableString(SqlDataReader reader, string column)
        {
            int ordinal = reader.GetOrdinal(column);
            return reader.IsDBNull(ordinal) ? "" : reader.GetString(ordinal);
        }

        public async Task<IEnumerable<Item>> GetItemsByDungeonId(int id)
        {
            string sqlQuery = @"SELECT i.*
FROM items i
INNER JOIN boss_drops bd ON bd.item_id = i.id
INNER JOIN bosses b ON b.id = bd.boss_id
INNER JOIN dungeon_bosses db ON db.boss_id = b.id
WHERE db.dungeon_id = @dungeonId";


            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                await con.OpenAsync();

                using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                {
                    cmd.Parameters.AddWithValue("@dungeonId", id);
                    List<Item> tempList = new List<Item>();
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            tempList.Add(ItemMapper(reader));
                            //tempList.Add(new Item
                            //{
                            //    Id = GetNullableInt(reader, "id"), //reader.GetInt32(reader.GetOrdinal("id")),
                            //    Name = GetNullableString(reader, "name"), //reader.GetString(reader.GetOrdinal("name")),
                            //    Slot = GetNullableInt(reader, "gear_slot"), //reader.GetInt32(reader.GetOrdinal("gear_slot")),
                            //    ImageUrl = GetNullableString(reader, "image_url"), //reader.GetString(reader.GetOrdinal("image_url")),
                            //    ItemLevel = GetNullableInt(reader, "item_level"), //reader.GetInt32(reader.GetOrdinal("item_level")),
                            //    Rarity = GetNullableString(reader, "rarity"), //reader.GetString(reader.GetOrdinal("rarity")),
                            //    Difficulty = GetNullableString(reader, "difficulty"), //reader.GetString(reader.GetOrdinal("difficulty")),
                            //    Material = GetNullableString(reader, "material"), //reader.GetString(reader.GetOrdinal("material")),
                            //    Armor = GetNullableInt(reader, "armor"), //reader.GetInt32(reader.GetOrdinal("armor")),
                            //    MinDamage = GetNullableInt(reader, "min_damage"), //reader.GetInt32(reader.GetOrdinal("min_damage")),
                            //    MaxDamage = GetNullableInt(reader, "max_damage"), //reader.GetInt32(reader.GetOrdinal("max_damage")),
                            //    Intellect = GetNullableInt(reader, "intellect"), //reader.GetInt32(reader.GetOrdinal("intellect")),
                            //    Strength = GetNullableInt(reader, "strength"), //reader.GetInt32(reader.GetOrdinal("strength")),
                            //    Agility = GetNullableInt(reader, "agility"), //reader.GetInt32(reader.GetOrdinal("agility")),
                            //    Spirit = GetNullableInt(reader, "spirit"), //reader.GetInt32(reader.GetOrdinal("spirit")),
                            //    Stamina = GetNullableInt(reader, "stamina"), //reader.GetInt32(reader.GetOrdinal("stamina")),
                            //    Haste = GetNullableInt(reader, "haste"), //reader.GetInt32(reader.GetOrdinal("haste")),
                            //    Crit = GetNullableInt(reader, "crit"), //reader.GetInt32(reader.GetOrdinal("crit")),
                            //    Mastery = GetNullableInt(reader, "mastery"), //reader.GetInt32(reader.GetOrdinal("mastery")),
                            //    Dodge = GetNullableInt(reader, "dodge"), //reader.GetInt32(reader.GetOrdinal("dodge")),
                            //    Parry = GetNullableInt(reader, "parry"), //reader.GetInt32(reader.GetOrdinal("parry")),
                            //    Hit = GetNullableInt(reader, "hit"), //reader.GetInt32(reader.GetOrdinal("hit")),
                            //    Expertise = GetNullableInt(reader, "expertise"), //reader.GetInt32(reader.GetOrdinal("expertise")),
                            //    Speed = Convert.ToDouble(reader["speed"]), //reader.GetDouble(reader.GetOrdinal("speed")),
                            //    SocketAmount = GetNullableInt(reader, "socket_amount"), //reader.GetInt32(reader.GetOrdinal("socket_amount")),
                            //    SocketBonusStat = GetNullableString(reader, "socket_bonus_stat"), //reader.GetString(reader.GetOrdinal("socket_bonus_stat")),
                            //    SocketBonusAmount = GetNullableInt(reader, "socket_bonus_amount"), //reader.GetInt32(reader.GetOrdinal("socket_bonus_amount")),
                            //    Enchants = GetNullableInt(reader, "enchant") //reader.GetInt32(reader.GetOrdinal("enchant"))
                            //});
                        }
                        if (tempList.Count == 0) throw new InvalidOperationException("No item for that dungeon");
                        return tempList;
                    }
                }
            }
        }

        private Item ItemMapper(SqlDataReader reader)
        {
            return new Item
            {
                Id = GetNullableInt(reader, "id"), //reader.GetInt32(reader.GetOrdinal("id")),
                Name = GetNullableString(reader, "name"), //reader.GetString(reader.GetOrdinal("name")),
                Slot = GetNullableInt(reader, "gear_slot"), //reader.GetInt32(reader.GetOrdinal("gear_slot")),
                ImageUrl = GetNullableString(reader, "image_url"), //reader.GetString(reader.GetOrdinal("image_url")),
                ItemLevel = GetNullableInt(reader, "item_level"), //reader.GetInt32(reader.GetOrdinal("item_level")),
                Rarity = GetNullableString(reader, "rarity"), //reader.GetString(reader.GetOrdinal("rarity")),
                Difficulty = GetNullableString(reader, "difficulty"), //reader.GetString(reader.GetOrdinal("difficulty")),
                Material = GetNullableString(reader, "material"), //reader.GetString(reader.GetOrdinal("material")),
                Armor = GetNullableInt(reader, "armor"), //reader.GetInt32(reader.GetOrdinal("armor")),
                MinDamage = GetNullableInt(reader, "min_damage"), //reader.GetInt32(reader.GetOrdinal("min_damage")),
                MaxDamage = GetNullableInt(reader, "max_damage"), //reader.GetInt32(reader.GetOrdinal("max_damage")),
                Intellect = GetNullableInt(reader, "intellect"), //reader.GetInt32(reader.GetOrdinal("intellect")),
                Strength = GetNullableInt(reader, "strength"), //reader.GetInt32(reader.GetOrdinal("strength")),
                Agility = GetNullableInt(reader, "agility"), //reader.GetInt32(reader.GetOrdinal("agility")),
                Spirit = GetNullableInt(reader, "spirit"), //reader.GetInt32(reader.GetOrdinal("spirit")),
                Stamina = GetNullableInt(reader, "stamina"), //reader.GetInt32(reader.GetOrdinal("stamina")),
                Haste = GetNullableInt(reader, "haste"), //reader.GetInt32(reader.GetOrdinal("haste")),
                Crit = GetNullableInt(reader, "crit"), //reader.GetInt32(reader.GetOrdinal("crit")),
                Mastery = GetNullableInt(reader, "mastery"), //reader.GetInt32(reader.GetOrdinal("mastery")),
                Dodge = GetNullableInt(reader, "dodge"), //reader.GetInt32(reader.GetOrdinal("dodge")),
                Parry = GetNullableInt(reader, "parry"), //reader.GetInt32(reader.GetOrdinal("parry")),
                Hit = GetNullableInt(reader, "hit"), //reader.GetInt32(reader.GetOrdinal("hit")),
                Expertise = GetNullableInt(reader, "expertise"), //reader.GetInt32(reader.GetOrdinal("expertise")),
                Speed = Convert.ToDouble(reader["speed"]), //reader.GetDouble(reader.GetOrdinal("speed")),
                SocketAmount = GetNullableInt(reader, "socket_amount"), //reader.GetInt32(reader.GetOrdinal("socket_amount")),
                SocketBonusStat = GetNullableString(reader, "socket_bonus_stat"), //reader.GetString(reader.GetOrdinal("socket_bonus_stat")),
                SocketBonusAmount = GetNullableInt(reader, "socket_bonus_amount"), //reader.GetInt32(reader.GetOrdinal("socket_bonus_amount")),
                Enchants = GetNullableInt(reader, "enchant") //reader.GetInt32(reader.GetOrdinal("enchant"))
            };


        }

        public async Task<Item> GetByItemId(int id)
        {
            Item item = null;
            string sqlQuery = "SELECT * FROM items WHERE id = @id";

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                await con.OpenAsync();

                using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    SqlDataReader reader = await cmd.ExecuteReaderAsync();

                    if (await reader.ReadAsync())
                    {
                        item = new Item
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("id")),
                            Name = reader.GetString(reader.GetOrdinal("name")),
                            Slot = reader.GetInt32(reader.GetOrdinal("gear_slot")),
                            ImageUrl = reader.GetString(reader.GetOrdinal("image_url")),
                            ItemLevel = reader.GetInt32(reader.GetOrdinal("item_level")),
                            Rarity = reader.GetString(reader.GetOrdinal("rarity")),
                            Difficulty = reader.GetString(reader.GetOrdinal("difficulty")),
                            Material = reader.GetString(reader.GetOrdinal("material")),
                            Armor = reader.GetInt32(reader.GetOrdinal("armor")),
                            MinDamage = reader.GetInt32(reader.GetOrdinal("min_damage")),
                            MaxDamage = reader.GetInt32(reader.GetOrdinal("max_damage")),
                            Intellect = reader.GetInt32(reader.GetOrdinal("intellect")),
                            Strength = reader.GetInt32(reader.GetOrdinal("strength")),
                            Agility = reader.GetInt32(reader.GetOrdinal("agility")),
                            Spirit = reader.GetInt32(reader.GetOrdinal("spirit")),
                            Stamina = reader.GetInt32(reader.GetOrdinal("stamina")),
                            Haste = reader.GetInt32(reader.GetOrdinal("haste")),
                            Crit = reader.GetInt32(reader.GetOrdinal("crit")),
                            Mastery = reader.GetInt32(reader.GetOrdinal("mastery")),
                            Dodge = reader.GetInt32(reader.GetOrdinal("dodge")),
                            Parry = reader.GetInt32(reader.GetOrdinal("parry")),
                            Hit = reader.GetInt32(reader.GetOrdinal("hit")),
                            Expertise = reader.GetInt32(reader.GetOrdinal("expertise")),
                            Speed = reader.GetDouble(reader.GetOrdinal("speed")),
                            SocketAmount = reader.GetInt32(reader.GetOrdinal("socket_amount")),
                            SocketBonusStat = reader.GetString(reader.GetOrdinal("socket_bonus_stat")),
                            SocketBonusAmount = reader.GetInt32(reader.GetOrdinal("socket_bonus_amount")),
                            Enchants = reader.GetInt32(reader.GetOrdinal("enchant"))
                        };
                    }
                    else
                    {
                        throw new KeyNotFoundException($"No item found with id {id}");
                    }
                }
            }

            return item;
        }

        public async Task<string> GetItemSlotNameAsync(int id)
        {
            string sqlQuery = "SELECT name FROM gear_slots WHERE id = @id";

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                await con.OpenAsync();

                using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                {
                    try
                    {
                        return Convert.ToString(await cmd.ExecuteScalarAsync());
                    } catch (SqlException)
                    {
                        throw new ArgumentException("No slot found with that Id");
                    }
                }
            }
        }

    }
}
