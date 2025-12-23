using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizApp.Dtos;
using QuizApp.Interfaces;

namespace QuizApp.Controllers;

[Route("[controller]")]
[ApiController]

public class ExamController : CustomBaseController
{
    private readonly IExamService _examService;

    public ExamController(IExamService examService)
    {
        _examService = examService;
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> AddExam([FromBody] CreateExamDto createExamDto)
    {
        var response = await _examService.CreateExamAsync(createExamDto, IsTeacher(), GetUserId() ?? "");
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllExam()
    {
        var response = await _examService.GetAllExamAsync();
        return Ok(response);
    }
}