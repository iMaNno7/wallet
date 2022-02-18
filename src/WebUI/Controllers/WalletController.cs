using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.TodoItems.Commands.CreateTodoItem;
using CleanArchitecture.Application.TodoItems.Commands.DeleteTodoItem;
using CleanArchitecture.Application.TodoItems.Commands.UpdateTodoItem;
using CleanArchitecture.Application.TodoItems.Commands.UpdateTodoItemDetail;
using CleanArchitecture.Application.TodoItems.Queries.GetTodoItemsWithPagination;
using CleanArchitecture.Application.Wallet.Commands.CreateWallet;
using CleanArchitecture.Application.Wallet.Commands.DeleteTodoItem;
using CleanArchitecture.Application.Wallet.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.WebUI.Controllers;

[Authorize]
public class WalletController : ApiControllerBase
{
    private readonly ICurrentUserService _currentUserService;

    public WalletController(ICurrentUserService currentUserService)
    {
        _currentUserService = currentUserService;
    }

    [HttpGet]
    public async Task<ActionResult<List<GetAllWalletVm>>> GetList()
    {
        try
        {
        return await Mediator.Send(new GetAllWalletQuery() { UserId= _currentUserService.UserId });

        }
        catch (Exception ex)
        {

            throw;
        }
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Create(CreateWalletCommand command)
    {
        command.UserId = _currentUserService.UserId;
        return await Mediator.Send(command);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(Guid id, UpdateWalletCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }

        await Mediator.Send(command);

        return NoContent();
    }

    [HttpPut("[action]")]
    public async Task<ActionResult> UpdateItemDetails(int id, UpdateTodoItemDetailCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }

        await Mediator.Send(command);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        await Mediator.Send(new DeleteWalletCommand { Id = id });

        return NoContent();
    }
}
