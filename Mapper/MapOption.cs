using QuizApp.Dtos;
using QuizApp.Models;

namespace QuizApp.Mapper;

public static class OptionMapper
{
    public static OptionResponse MapOption(Option option, string roleId)
    {
        var response = new OptionResponse
        {
            Id = option.Id,
            Option1 = option.Option1,
            Option2 = option.Option2,
            Option3 = option.Option3,
            Option4 = option.Option4,
        };
        
        if (int.TryParse(roleId, out int parsedRoleId) && (parsedRoleId == (int)Roles.Teacher || parsedRoleId == (int)Roles.SuperAdmin))
        {
            response.CorrectAnswer = option.CorrectAnswer;
            response.CreatedByUser = UserMapper.MapUser(option.CreatedByUser);
            response.UpdatedByUser = UserMapper.MapUser(option.UpdatedByUser);
        }
        
        return response;
    }
}
