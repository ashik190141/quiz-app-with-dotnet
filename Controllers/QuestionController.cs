using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizApp.Dtos;
using QuizApp.Interfaces;

namespace QuizApp.Controllers;

[Route("[controller]")]
[ApiController]

public class QuestionController : CustomBaseController
{
    private readonly IQuestionService _questionService;

    public QuestionController(IQuestionService questionService)
    {
        _questionService = questionService;
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> AddQuestion([FromBody] CreateQuestionDto createQuestionDto)
    {
        var response = await _questionService.CreateQuestionAsync(createQuestionDto, IsTeacher(), GetUserId() ?? "");
        return Ok(response);
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetQuestionByExam([FromRoute] int id)
    {
        var response = await _questionService.GetExamQuestionAsync(id, GetUserId() ?? "");
        return Ok(response);
    }
}