using Common.Commands.Books;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Controllers
{
    /// <summary>
    /// Books Controller
    /// </summary>
    [Route("[controller]")]
    public class BooksController : BaseController
    {
        /// <summary>
        /// Books Controller Constructor
        /// </summary>
        /// <param name="mediator">IMediator instance</param>
        public BooksController(IMediator mediator)
        {
            Mediator = mediator;
        }

        IMediator Mediator { get; }

        /// <summary>
        /// Gets all books
        /// </summary>
        /// <returns>List of books</returns>
        /// <response code="200">Success</response>
        /// <response code="500">Unexpected Error</response>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<Book>), 200)]
        public async Task<IActionResult> Get()
        {
            var command = new GetBooks(UserId);
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Gets book by id
        /// </summary>
        /// <param name="id">Book id, Guid</param>
        /// <returns>Book profile</returns>
        /// <response code="200">Success</response>
        /// <response code="400">If book id empty</response>
        /// <response code="404">If book is not found</response>
        /// <response code="500">Unexpected Error</response>
        [HttpGet("id")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Book), 200)]
        public async Task<IActionResult> Get(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest();

            var command = new GetBook(id, UserId);
            var result = await Mediator.Send(command);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        /// <summary>
        /// Creates a new book
        /// </summary>
        /// <param name="id">New book id, Guid</param>
        /// <returns>Empty result</returns>
        /// <response code="201">Success</response>
        /// <response code="400">If book id empty</response>
        /// <response code="500">Unexpected Error</response>
        [HttpPost("id")]
        [Produces("application/json")]
        [ProducesResponseType(201)]
        public async Task<IActionResult> Post(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest();

            var book = new Book(id, "No name", 0, null, DateTime.UtcNow);
            var command = new AddBook(book, UserId);
            await Mediator.Send(command);

            return Ok();
        }

        /// <summary>
        /// Updates an existing book
        /// </summary>
        /// <param name="id">Book id, Guid</param>
        /// <param name="book">Book profile</param>
        /// <returns>Empty result</returns>
        /// <response code="201">Success</response>
        /// <response code="400">If book id empty</response>
        /// <response code="500">Unexpected Error</response>
        [HttpPatch("id")]
        [Produces("application/json")]
        [ProducesResponseType(201)]
        public async Task<IActionResult> Patch(Guid id, [FromBody]Book book)
        {
            if (id == Guid.Empty || id != book.Id)
                return BadRequest();

            var command = new UpdateBook(book, UserId);
            await Mediator.Send(command);

            return Ok();
        }

        /// <summary>
        /// Removes an existing book
        /// </summary>
        /// <param name="id">Book id, Guid</param>
        /// <returns>Empty result</returns>
        /// <response code="201">Success</response>
        /// <response code="400">If book id empty</response>
        /// <response code="500">Unexpected Error</response>
        [HttpDelete("id")]
        [Produces("application/json")]
        [ProducesResponseType(201)]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest();

            var command = new DeleteBook(id, UserId);
            await Mediator.Send(command);

            return Ok();
        }
    }
}