using DbAssignment.Entities;
using DbAssignment.Repositories;

namespace DbAssignment.Services;

public class UserService(UserRepository userRepository)
{

    private readonly UserRepository _userRepository = userRepository;

    public UserEntity CreateUser(string firstName, string lastName, string email)
    {
        var userEntity = _userRepository.Get(x => x.FirstName == firstName && x.LastName == lastName && x.Email == email);
        userEntity ??= _userRepository.Create(new UserEntity
        { 
            FirstName = firstName, 
            LastName = lastName, 
            Email = email 
        });

        return userEntity;

    }

    public UserEntity GetUserById(int id)
    {
        var userEntity = _userRepository.Get(x => x.Id == id);
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
