﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using SeeSharp.Application.Features.BlogPosts.Commands;
using SeeSharp.Application.Features.BlogPosts.Queries;

namespace SeeSharp.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class BlogPostsController : ControllerBase
{
    private readonly IMediator _mediatr;
    private readonly ILogger<BlogPostsController> _logger;

    public BlogPostsController(ILogger<BlogPostsController> logger, IMediator mediatr)
    {
        _logger = logger;
        _mediatr = mediatr;
    }

    [HttpGet]
    public async Task<List<BlogPostDto>> Get()
    {
        return await _mediatr.Send(new GetBlogPostsQuery());
    }

    [HttpGet("{id:guid}")]
    public async Task<BlogPostDto> Get(Guid id)
    {
        return await _mediatr.Send(new GetBlogPostByIdQuery(id));
    }

    [HttpPost]
    public async Task<Guid> CreateBlogPost(CreateBlogPostCommand command)
    {
        return await _mediatr.Send(command);
    }

    [HttpPut("id:guid")]
    public async Task<IResult> UpdateBlogPost(Guid id, UpdateBlogPostCommand command)
    {
        if (id != command.Id) return Results.BadRequest();
        await _mediatr.Send(command);
        return Results.NoContent();
    }

    [HttpDelete("id:guid")]
    public async Task<IResult> DeleteBlogPost(Guid id)
    {
        await _mediatr.Send(new DeleteBlogPostCommand(id));

        return Results.NoContent();
    }
}

