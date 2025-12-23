using System.Text.Json;
using QuizApp.Dtos;
using QuizApp.Helper;
using QuizApp.Interfaces;
using QuizApp.Models;

namespace QuizApp.Services;

public class ExamService : IExamService
{
    private readonly IExamRepository _examRepository;
    public ExamService(IExamRepository examRepository)
    {
        _examRepository = examRepository;
    }

    public async Task<StandardApiResponse<CreateExamResponse?>> CreateExamAsync(CreateExamDto createExamDto, bool isTeacher, string userId)
    {
        string duration = "";
        if(!isTeacher) return new StandardApiResponse<CreateExamResponse?>(false, "You are not authorized to create an exam", null);
        if(string.IsNullOrEmpty(userId) || !int.TryParse(userId, out int parsedUserId))
        {
            return new StandardApiResponse<CreateExamResponse?>(false, "Invalid user ID", null);
        }
        int examDuration = (int)(createExamDto.EndTime - createExamDto.StartTime).TotalMinutes;
        if(examDuration <= 0) return new StandardApiResponse<CreateExamResponse?>(false, "Invalid Exam Time", null);
        
        if(examDuration >= 60)
        {
            int hours = examDuration/60;
            int minutes = examDuration%60;
            if (hours > 0)
            {
                duration += $"{hours} hour";
                if (hours > 1) duration += "s";
            }

            if(minutes > 0)
            {
                if (hours > 0) duration += " ";
                duration += $"{minutes} minute";
                if (minutes > 1) duration += "s";
            }
        }
        else
        {
            duration = $"{examDuration} minute";
            if (examDuration > 1) duration += "s";
        }

        var createdExam = new Exam
        {
            Name = createExamDto.Name,
            Year = createExamDto.Year,
            Month = createExamDto.Month,
            Duration = duration,
            NumberOfQuestions = createExamDto.NumberOfQuestions,
            StartTime = createExamDto.StartTime,
            EndTime = createExamDto.EndTime,
            CreatedBy = parsedUserId,
            UpdatedBy = parsedUserId
        };
        
        var result = await _examRepository.CreateExamAsync(createdExam);
        if (result != null)
        {
            var serializeUser = JsonSerializer.Serialize(result);
            CreateExamResponse? createExamResponse = JsonSerializer.Deserialize<CreateExamResponse>(serializeUser);
            return new StandardApiResponse<CreateExamResponse?>(true, "Exam created successfully", createExamResponse);
        }
        return new StandardApiResponse<CreateExamResponse?>(false, "Failed to create exam", null);
    }

    public async Task<StandardApiResponse<IEnumerable<GetAllExamResponse>>> GetAllExamAsync()
    {
        IEnumerable<Exam> exams = await _examRepository.GetAllExamAsync();
        if(exams != null){
            var serializeExams = JsonSerializer.Serialize(exams);
            IEnumerable<GetAllExamResponse>? allExams = JsonSerializer.Deserialize<IEnumerable<GetAllExamResponse>>(serializeExams);
            return new StandardApiResponse<IEnumerable<GetAllExamResponse>>(true, "Exam fetch successfully", allExams);
        }
        return new StandardApiResponse<IEnumerable<GetAllExamResponse>>(false, "Exam not found", []);
    }
}