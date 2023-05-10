using APILibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers;

/// <summary>
/// Class UsersController.
/// Implements the <see cref="ControllerBase" />
/// </summary>
/// <seealso cref="ControllerBase" />
[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    /// <summary>
    /// The context
    /// </summary>
    private readonly UsersContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="UsersController" /> class.
    /// </summary>
    /// <param name="context">The context.</param>
    public UsersController(UsersContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Gets this instance.
    /// </summary>
    /// <returns>ActionResult&lt;IEnumerable&lt;User&gt;&gt;.</returns>
    [HttpGet]
    public async Task<List<User?>> Get()
    {
        return await _context.Users.ToListAsync();
    }

    /// <summary>
    /// Gets the specified identifier.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <returns>ActionResult&lt;User&gt;.</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<User>> Get(int id)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x != null && x.Id == id);
        if (user == null)
            return NotFound();
        return new ObjectResult(user);
    }

    /// <summary>
    /// Posts the specified user.
    /// </summary>
    /// <param name="user">The user.</param>
    /// <returns>ActionResult&lt;User&gt;.</returns>
    [HttpPost("Create")]
    public async Task<ActionResult<User>> Post(User? user)
    {
        if (user == null)
        {
            return BadRequest();
        }

        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return Ok(user);
    }

    /// <summary>
    /// Puts the specified user.
    /// </summary>
    /// <param name="user">The user.</param>
    /// <returns>ActionResult&lt;User&gt;.</returns>
    [HttpPut("Update")]
    public async Task<ActionResult<User>> Put(User? user)
    {
        if (user == null)
        {
            return BadRequest();
        }
        if (!_context.Users.Any(x => x != null && x.Id == user.Id))
        {
            return NotFound();
        }

        _context.Update(user);
        await _context.SaveChangesAsync();
        return Ok(user);
    }

    /// <summary>
    /// Deletes the specified identifier.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <returns>ActionResult&lt;User&gt;.</returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult<User>> Delete(int id)
    {
        var user = _context.Users.FirstOrDefault(x => x != null && x.Id == id);
        if (user == null)
        {
            return NotFound();
        }
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return Ok(user);
    }
}