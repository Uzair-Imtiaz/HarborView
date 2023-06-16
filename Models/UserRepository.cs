using HarborView_Inn.Models;
using Microsoft.EntityFrameworkCore;

namespace HarborView_Inn.Models
{
    public class UserRepository
    {
        public int [] authenticateUser(User temp) 
        {
            int[] ret = new int[2];
            WebProjectAuthenticateUserContext context=new WebProjectAuthenticateUserContext();
            User user = context.Users.Find(temp.Email);
            if (user == null)
            {
                ret[0] = 0;
                ret[1] = 0;
                return ret;
            }
            if (user.Password == temp.Password)
            {
                ret[0]=1;
                ret[1] = user.Id;
                return ret;
               
            }
            ret[0] = 0;
            ret[1] = user.Id;
            return ret;

        }
        public int [] addUser(User temp)
        {
            int[] ret = new int[2];
            WebProjectAuthenticateUserContext context = new WebProjectAuthenticateUserContext();
            User users = context.Users.Find(temp.Email);
            bool isTableEmpty = !context.Users.Any();
            if(isTableEmpty)
            {
                temp.Id = 1;
            }
            else
            {
                User last = context.Users.OrderByDescending(r => r.Id).FirstOrDefault();
                temp.Id = last.Id + 1;

            }
           
            if (users==null)
            {
                context.Users.Add(temp);
                context.SaveChanges();
                ret[0] = 1;
                ret[1] = temp.Id;
                return ret;
            }
            ret[0] = 0;
            ret[1] = temp.Id;
            return ret;
        
           
            
        }
    }
}
