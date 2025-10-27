using Microsoft.AspNetCore.Mvc;
using PigelloMockAPI.Data;
using PigelloMockAPI.Models;

namespace PigelloMockAPI.Controllers;

/// <summary>
/// Endpoints för att hantera rum
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class RoomsController : ControllerBase
{
    private readonly InMemoryDataStore _dataStore;

    public RoomsController(InMemoryDataStore dataStore)
    {
        _dataStore = dataStore;
    }

    /// <summary>
    /// Hämta alla rum med valfri filtrering
    /// </summary>
    /// <param name="buildingId">Filtrera på byggnad</param>
    /// <returns>Lista med rum</returns>
    [HttpGet]
    public ActionResult<IEnumerable<Room>> GetRooms([FromQuery] Guid? buildingId = null)
    {
        var rooms = _dataStore.Rooms.AsEnumerable();

        if (buildingId.HasValue)
            rooms = rooms.Where(r => r.BuildingId == buildingId.Value);

        return Ok(rooms.ToList());
    }

    [HttpGet("{id}")]
    public ActionResult<Room> GetRoom(Guid id)
    {
        var room = _dataStore.Rooms.FirstOrDefault(r => r.Id == id);
        if (room == null)
            return NotFound();

        return Ok(room);
    }

    [HttpPost]
    public ActionResult<Room> CreateRoom(Room room)
    {
        room.Id = Guid.NewGuid();
        _dataStore.Rooms.Add(room);
        return CreatedAtAction(nameof(GetRoom), new { id = room.Id }, room);
    }

    [HttpPut("{id}")]
    public ActionResult<Room> UpdateRoom(Guid id, Room updatedRoom)
    {
        var existingRoom = _dataStore.Rooms.FirstOrDefault(r => r.Id == id);
        if (existingRoom == null)
            return NotFound();

        existingRoom.Name = updatedRoom.Name;
        existingRoom.BuildingId = updatedRoom.BuildingId;
        existingRoom.RoomNumber = updatedRoom.RoomNumber;
        existingRoom.Floor = updatedRoom.Floor;
        existingRoom.RoomType = updatedRoom.RoomType;
        existingRoom.Area = updatedRoom.Area;
        existingRoom.Description = updatedRoom.Description;

        return Ok(existingRoom);
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteRoom(Guid id)
    {
        var room = _dataStore.Rooms.FirstOrDefault(r => r.Id == id);
        if (room == null)
            return NotFound();

        _dataStore.Rooms.Remove(room);
        return NoContent();
    }
}
