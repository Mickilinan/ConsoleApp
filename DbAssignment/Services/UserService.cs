using DbAssignment.Entities;
using DbAssignment.Repositories;

namespace DbAssignment.Services;

public class UserService(UserRepository userRepository)
{

    private readonly UserRepository _userRepository = userRepository;

    public UserEntity CreateUser(string firstName, string lastName, string email)
    {
        // Check if a user with the same name already exists
        var existingUser = _userRepository.Get(x => x.FirstName == firstName && x.LastName == lastName && x.Email == email);
        if (existingUser != null)
        {
            return null;
        }

        var userEntity = new UserEntity
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email
        };

        userEntity = _userRepository.Create(userEntity);
        return userEntity;

    }

    public UserEntity GetUserById(int id)
    {
        var userEntity = _userRepository.Get(x => x.Id == id);
        return userEntity;
    }

    public UserEntity GetUserByEmail(string email)
    {
        var userEntity = _userRepository.Get(x => x.Email == email);
        return userEntity;
    }

    public IEnumerable<UserEntity> GetAllUsers()
    {
        var users = _userRepository.GetAll();
        return users;
    }

    public UserEntity UpdateUser(UserEntity userEntity)
    {
        var updatedUser = _userRepository.Update(x => x.Id == userEntity.Id, userEntity);
        return updatedUser;
    }


    public void DeleteUser(int Id)
    {
        _userRepository.Delete(x => x.Id == Id);

    }
}
