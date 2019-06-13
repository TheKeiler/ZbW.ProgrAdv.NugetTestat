using System;
using System.Collections.Generic;
using System.Data;
using ZbW.ProgrAdv.NugetTestat.Model;

namespace ZbW.ProgrAdv.NugetTestat.Persistence
{
    //TODO: Refactor Class with LINQ
    public class LocationRepository : RepositoryBase<Location>
    {
        public override string TableName => "location";
        public override string FieldsInSelectStatement => "location_id, parent_location, address_fk, designation, building, room";
        public override string TablePrimaryKey => "location_id";
        public override string FieldsInAddStatement => "parent_location , address_fk , designation , building , room";

        public LocationRepository(string connectionString) : base(connectionString)
        {
        }

        public override List<Location> GenerateEntityListFromReader(IDataReader reader)
        {
            var locations = new List<Location>();
            object[] dataRow = new object[reader.FieldCount];

            while (reader.Read())
            {// solange noch Daten vorhanden sind
                int cols = reader.GetValues(dataRow); // tatsächliches Lesen 

                var location = new Location();
                for (int i = 0; i < cols; i++)
                {
                    switch (i)
                    {
                        case 0:
                            location.Id = Convert.ToInt32(dataRow[i]);
                            break;
                        case 1:
                            if (reader.IsDBNull(1))
                            {
                                location.ParentId = null;
                            }
                            else
                            {
                                location.ParentId = Convert.ToInt32(dataRow[i]);
                            }
                            break;
                        case 2:
                            location.AddressId = Convert.ToInt32(dataRow[i]);
                            break;
                        case 3:
                            location.Designation = dataRow[i].ToString();
                            break;
                        case 4:
                            location.BuildingNr = Convert.ToInt32(dataRow[i]);
                            break;
                        case 5:
                            location.RoomNr = Convert.ToInt32(dataRow[i]);
                            break;
                        default:
                            Console.WriteLine("Da kahmen zu viele Felder");
                            break;
                    }            
                }
                locations.Add(location);
            }
            return locations;
        }

        public override string GenerateAddStatementValues(Location entity)
        {
            return $"{entity.ParentId}, {entity.AddressId}, '{entity.Designation}', {entity.BuildingNr}, {entity.RoomNr}";
        }

        public override string GenerateUpdateStatementValues(Location entity)
        {
            return $"parent_location = {entity.ParentId}, address_fk = {entity.AddressId} , designation = '{entity.Designation}', building = {entity.BuildingNr} , room = {entity.RoomNr} WHERE location_id = {entity.Id}";
        }
    }
}
