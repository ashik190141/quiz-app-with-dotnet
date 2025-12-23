using System.Text.Json;
using QuizApp.Dtos;
using QuizApp.Helper;
using QuizApp.Interfaces;
using QuizApp.Models;

namespace QuizApp.Services;

public class QuestionService : IQuestionService
{
    private readonly IQuestionRepository _questionRepository;
    private readonly IOptionsRepository _optionsRepository;
    private readonly IExamRepository _examRepository;
    public QuestionService(IQuestionRepository questionRepository, IOptionsRepository optionsRepository, IExamRepository examRepository)
    {
        _questionRepository = questionRepository;
        _optionsRepository = optionsRepository;
        _examRepository = examRepository;
    }

    public async Task<StandardApiResponse<CreateQuestionResponse?>> CreateQuestionAsync(CreateQuestionDto createQuestionDto, bool isTeacher, string userId)
    {
        if(!isTeacher) return new StandardApiResponse<CreateQuestionResponse?>(false, "You are not authorized to create an exam", null);
        if(string.IsNullOrEmpty(userId) || !int.TryParse(userId, out int parsedUserId))
        {
            return new StandardApiResponse<CreateQuestionResponse?>(false, "Invalid user ID", null);
        }
        var totalNumberOfQuestions = await _questionRepository.GetQuestionCountByExamIdAsync(createQuestionDto.ExamId);
        var exam = await _examRepository.GetExamByIdAsync(createQuestionDto.ExamId);
        if (exam == null)
        {
            return new StandardApiResponse<CreateQuestionResponse?>(false, "Exam do not exist", null);
        }
        if(exam.NumberOfQuestions <= totalNumberOfQuestions)
        {
            return new StandardApiResponse<CreateQuestionResponse?>(false, "You have reached the maximum number of questions for this exam", null);
        }
        var createdOptionsDto = new Option
        {
            Option1 = createQuestionDto.Option1,
            Option2 = createQuestionDto.Option2,
            Option3 = createQuestionDto.Option3,
            Option4 = createQuestionDto.Option4,
            CorrectAnswer = createQuestionDto.CorrectAnswer,
            CreatedBy = parsedUserId,
            UpdatedBy = parsedUserId
        };
        int optionId = await _optionsRepository.CreateOptionsAsync(createdOptionsDto);
        var createQuestionPayload = new Questions
        {
            ExamId = createQuestionDto.ExamId,
            Question = createQuestionDto.Question,
            OptionId = optionId,
            CreatedBy = parsedUserId,
            UpdatedBy = parsedUserId
        };
        var createdQuestion = await _questionRepository.CreateQuestionAsync(createQuestionPayload);
        if(createdQuestion == null)
        {
            return new StandardApiResponse<CreateQuestionResponse?>(false, "Question not created", null);
        }
        var CreatedQuestionResponse = new CreateQuestionResponse
        {
            Id = createdQuestion.Id,
            Question = createdQuestion.Question,
            ExamId = createdQuestion.ExamId,
            Option1 = createdOptionsDto.Option1,
            Option2 = createdOptionsDto.Option2,
            Option3 = createdOptionsDto.Option3,
            Option4 = createdOptionsDto.Option4,
            CorrectAnswer = createdOptionsDto.CorrectAnswer,
            CreatedBy = createdQuestion.CreatedBy,
            UpdatedBy = createdQuestion.UpdatedBy,
            CreatedAt = createdQuestion.CreatedAt,
            UpdatedAt = createdQuestion.UpdatedAt
        };
        return new StandardApiResponse<CreateQuestionResponse?>(true, "Question created successfully", CreatedQuestionResponse);
    }

    public async Task<StandardApiResponse<IEnumerable<Questions>>> GetExamQuestionAsync(int examId, string userId)
    {
        if(string.IsNullOrEmpty(userId) || !int.TryParse(userId, out int parsedUserId))
        {
            return new StandardApiResponse<IEnumerable<Questions>>(false, "Invalid user ID", null);
        }
        var questions = await _questionRepository.GetQuestionsByExamIdAsync(examId);
        if(questions == null)
        {
            return new StandardApiResponse<IEnumerable<Questions>>(false, "Question not found", null);
        }
        return new StandardApiResponse<IEnumerable<Questions>>(true, "Question found", questions);
    }
}