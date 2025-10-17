using ChambaPro.Platform.API.IAM.Domain.Model.Aggregates;
using ChambaPro.Platform.API.IAM.Interfaces.Rest.Resources;

namespace ChambaPro.Platform.API.IAM.Interfaces.Rest.Transforms;

public static class UserResourceTransformer
{
    public static UserResource ToResourceFromEntity(User entity)
    {
        return new UserResource(
            entity.Id,
            entity.Email,
            entity.Name,
            entity.LastName,
            entity.Phone,
            entity.Avatar,
            entity.Type.ToString(),
            entity.CreatedAt,
            entity.Speciality,
            entity.Description,
            entity.Experience,
            entity.Rating,
            entity.ReviewsCount,
            entity.HourlyRate,
            entity.IsAvailable
        );
    }
}