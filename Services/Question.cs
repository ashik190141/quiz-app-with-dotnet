using QuizApp.Dtos;
using QuizApp.Helper;
using QuizApp.Interfaces;
using QuizApp.Mapper;
using QuizApp.Models;

namespace QuizApp.Services;

public class QuestionService : IQuestionService
{
    private readonly IQuestionRepository _questionRepository;
    private readonly IOptionsRepository _optionsRepository;
    private readonly IExamRepository _examRepository;
    private readonly IUserRepository _userRepository;
    public QuestionService(IQuestionRepository questionRepository, IOptionsRepository optionsRepository, IExamRepository examRepository, IUserRepository userRepository)
    {
        _questionRepository = questionRepository;
        _optionsRepository = optionsRepository;
        _examRepository = examRepository;
        _userRepository = userRepository;
    }

    public async Task<StandardApiResponse<CreateQuestionResponse?>> CreateQuestionAsync(CreateQuestionDto createQuestionDto, bool isTeacher, string userId)
    {
        if (!isTeacher) return new StandardApiResponse<CreateQuestionResponse?>(false, "You are not authorized to create an exam", null);
        if (string.IsNullOrEmpty(userId) || !int.TryParse(userId, out int parsedUserId))
        {
            return new StandardApiResponse<CreateQuestionResponse?>(false, "Invalid user ID", null);
        }
        var totalNumberOfQuestions = await _questionRepository.GetQuestionCountByExamIdAsync(createQuestionDto.ExamId);
        var exam = await _examRepository.GetExamByIdAsync(createQuestionDto.ExamId);
        if (exam == null)
        {
            return new StandardApiResponse<CreateQuestionResponse?>(false, "Exam do not exist", null);
        }
        if (exam.NumberOfQuestions <= totalNumberOfQuestions)
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
        if (createdQuestion == null)
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

    public async Task<StandardApiResponse<GetQuestionWithOutExamResponse>> GetExamQuestionAsync(int examId, string userId)
    {
        if (string.IsNullOrEmpty(userId) || !int.TryParse(userId, out int parsedUserId))
        {
            return new StandardApiResponse<GetQuestionWithOutExamResponse>(false, "Invalid user ID", null);
        }

        var exam = await _examRepository.GetExamByIdAsync(examId);
        if(exam == null)
        {
            return new StandardApiResponse<GetQuestionWithOutExamResponse>(false, "Exam not found", null);
        }

        var user = await _userRepository.GetUserByIdAsync(parsedUserId);
        IEnumerable<Questions> questions = await _questionRepository.GetQuestionsByExamIdAsync(examId, user?.RoleId ?? 0);
        if (questions == null)
        {
            return new StandardApiResponse<GetQuestionWithOutExamResponse>(false, "Question not found", null);
        }

        var questionList = new List<GetQuestionResponse>();
        foreach (var question in questions)
        {
            var options = new List<string>
            {
                question.Option.Option1,
                question.Option.Option2,
                question.Option.Option3,
                question.Option.Option4
            };

            for (int i = options.Count - 1; i > 0; i--)
            {
                int j = Random.Shared.Next(i + 1);
                (options[i], options[j]) = (options[j], options[i]);
            }

            var getQuestionResponse = new GetQuestionResponse
            {
                Id = question.Id,
                    Question = question.Question,
                    RandomPatternOption = new RandomPatternOption
                    {
                        Option1 = options[0],
                        Option2 = options[1],
                        Option3 = options[2],
                        Option4 = options[3],
                        CorrectAnswer = user?.RoleId != (int)Roles.Student ? question.Option.CorrectAnswer : null
                    }
            };

            if (user?.RoleId == (int)Roles.SuperAdmin || user?.RoleId == (int)Roles.Teacher)
            {
                getQuestionResponse.OptionId = question.OptionId;
                getQuestionResponse.Option = OptionMapper.MapOption(question.Option, user?.RoleId.ToString() ?? "0");
                getQuestionResponse.CreatedByUser = UserMapper.MapUser(question.CreatedByUser);
                getQuestionResponse.UpdatedByUser = UserMapper.MapUser(question.UpdatedByUser);
            }
            questionList.Add(getQuestionResponse);
        }

        var GetQuestionResponse = new GetQuestionWithOutExamResponse
        {
            Question = questionList
        };

        if (user?.RoleId != (int)Roles.Student)
        {
            GetQuestionResponse.Exam = ExamMapper.MapExam(exam);
        }
        return new StandardApiResponse<GetQuestionWithOutExamResponse>(true, "Question fetch successfully", GetQuestionResponse);
    }
}